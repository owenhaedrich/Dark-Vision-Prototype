using UnityEngine;
using UnityEngine.Rendering;

public class EffectTransitions : MonoBehaviour
{
    RectTransform topEyeLid;
    RectTransform bottomEyeLid;

    float currentClosure = 0f;
    float maxEyeLidTravel = 500f; // Distance each eyelid moves (top: 500 to 0, bottom: -500 to 0)
    bool isBlinking = false;
    float blinkProgress = 0f;
    float blinkDuration = 0.15f;
    float blinkCooldown = 0.2f;
    float timeSinceLastBlink = 0f;
    float squintClosure = 0f;
    float timeInBrightLight = 0f;
    float squintDelay = 1f;
    float brightThreshold = 0.5f;
    float maxSquintAmount = 1f; // Full squint = fully closed (y = 0)
    public float squintSpeed = 5f; // Speed to move to squint (units per second)
    Volume darkVolume;
    Volume lightVolume;

    // White noise variables
    AudioSource noiseSource;
    float noiseVolume = 0f;
    float maxNoiseVolume = 1f; // Max volume for AudioSource
    float noiseFadeSpeed = 5f; // Speed to lerp volume

    public float testLightLevel = 0f;
    public AudioClip whiteNoiseClip; // Assign in Inspector or via Resources.Load

    void Start()
    {
        topEyeLid = transform.GetChild(0).Find("TopEyeLid").GetComponent<RectTransform>();
        bottomEyeLid = transform.GetChild(0).Find("BottomEyeLid").GetComponent<RectTransform>();

        // Set initial open positions
        topEyeLid.anchoredPosition = new Vector2(topEyeLid.anchoredPosition.x, 500f);
        bottomEyeLid.anchoredPosition = new Vector2(bottomEyeLid.anchoredPosition.x, -500f);

        Volume[] vols = GetComponents<Volume>();
        if (vols.Length >= 2)
        {
            darkVolume = vols[0];
            lightVolume = vols[1];
            darkVolume.weight = 1;
            lightVolume.weight = 0;
        }

        // Find the AudioSource in the scene (attached to Player)
        noiseSource = FindObjectOfType<AudioSource>();
        if (noiseSource == null)
        {
            Debug.LogError("No AudioSource found in the scene. Please attach an AudioSource to the Player.");
        }
        else
        {
            // Configure AudioSource with clip
            if (whiteNoiseClip != null)
            {
                noiseSource.clip = whiteNoiseClip;
            }
            else if (noiseSource.clip == null)
            {
                Debug.LogWarning("No white noise clip assigned to AudioSource or EffectTransitions. Please assign a clip.");
            }
            noiseSource.loop = true;
            noiseSource.Play();
            noiseSource.volume = 0f; // Start silent
        }

        squintClosure = 0f;
        currentClosure = 0f;
        UpdateEyeLidPositions(0f);
    }

    void Update()
    {
        float lightLevel = GetLightLevel();

        if (darkVolume && lightVolume)
        {
            darkVolume.weight = 1f - lightLevel;
            lightVolume.weight = lightLevel;
        }

        if (!isBlinking)
        {
            UpdateSquint(lightLevel);
            currentClosure = squintClosure;
        }

        // Update noise volume after squint (shares timeInBrightLight)
        UpdateNoiseVolume(lightLevel);

        timeSinceLastBlink += Time.deltaTime;

        if (!isBlinking && timeSinceLastBlink > blinkCooldown && lightLevel > brightThreshold)
        {
            if (Random.value < 0.01f)
            {
                StartBlink();
            }
        }

        if (isBlinking)
        {
            UpdateBlink();
        }

        UpdateEyeLidPositions(currentClosure);
    }

    float GetLightLevel() => testLightLevel;

    void UpdateSquint(float lightLevel)
    {
        bool isBright = lightLevel >= brightThreshold;
        Debug.Log($"LightLevel: {lightLevel}, IsBright: {isBright}, TimeInBrightLight: {timeInBrightLight}, SquintClosure: {squintClosure}");

        if (isBright)
        {
            timeInBrightLight += Time.deltaTime;
        }
        else
        {
            timeInBrightLight -= Time.deltaTime * 2f;
        }

        timeInBrightLight = Mathf.Clamp(timeInBrightLight, 0f, squintDelay);

        // Determine target squint: 0 until delay is reached, then 1
        float targetSquint = (timeInBrightLight >= squintDelay && isBright) ? maxSquintAmount : 0f;

        // Move squintClosure toward target at squintSpeed
        if (squintClosure < targetSquint)
        {
            squintClosure += squintSpeed * Time.deltaTime;
            squintClosure = Mathf.Min(squintClosure, targetSquint);
        }
        else if (squintClosure > targetSquint)
        {
            squintClosure -= squintSpeed * Time.deltaTime;
            squintClosure = Mathf.Max(squintClosure, targetSquint);
        }
    }

    void UpdateNoiseVolume(float lightLevel)
    {
        // Target volume scales with lightLevel and exposure time
        float exposureRatio = timeInBrightLight / squintDelay;
        float targetVolume = lightLevel * exposureRatio * maxNoiseVolume;
        targetVolume = Mathf.Clamp(targetVolume, 0f, maxNoiseVolume);

        // Smoothly lerp to target
        noiseVolume = Mathf.Lerp(noiseVolume, targetVolume, Time.deltaTime * noiseFadeSpeed);

        // Apply to AudioSource if present
        if (noiseSource != null)
        {
            noiseSource.volume = noiseVolume;
        }

        Debug.Log($"NoiseVolume: {noiseVolume}, TargetVolume: {targetVolume}");
    }

    void StartBlink()
    {
        isBlinking = true;
        blinkProgress = 0f;
        timeSinceLastBlink = 0f;
    }

    void UpdateBlink()
    {
        blinkProgress += Time.deltaTime / blinkDuration;

        if (blinkProgress >= 1f)
        {
            isBlinking = false;
            blinkProgress = 0f;
            currentClosure = squintClosure;
        }
        else
        {
            float blinkCurve = Mathf.Sin(blinkProgress * Mathf.PI);
            currentClosure = Mathf.Lerp(squintClosure, 1.2f, blinkCurve); // Blink to 1.2x squint
        }
    }

    void UpdateEyeLidPositions(float closure)
    {
        Debug.Log($"CurrentClosure: {closure}, TopEyeLid Y: {topEyeLid.anchoredPosition.y}, BottomEyeLid Y: {bottomEyeLid.anchoredPosition.y}");

        // closure: 0 = fully open (top: 500, bottom: -500), 1 = fully closed (top: 0, bottom: 0)
        // closure > 1 (e.g., 1.2) means top goes below 0, bottom goes above 0
        float topY = Mathf.Lerp(500f, 0f, closure);
        float bottomY = Mathf.Lerp(-500f, 0f, closure);

        topEyeLid.anchoredPosition = new Vector2(topEyeLid.anchoredPosition.x, topY);
        bottomEyeLid.anchoredPosition = new Vector2(bottomEyeLid.anchoredPosition.x, bottomY);
    }
}