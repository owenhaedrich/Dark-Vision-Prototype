using UnityEngine;
using UnityEngine.Rendering;

public class EffectTransitions : MonoBehaviour
{
    RectTransform topEyeLid;
    RectTransform bottomEyeLid;
    float eyeLidOpenAmount = 1.0f; // 1.0 = fully open, 0.0 = fully closed
    float blinkMoveDistance = 1000.0f; // Distance the eyelids move when closing
    bool blinking = false;
    float blinkProgress = 0.0f;
    float blinkTime = 1.0f;
    Volume darkVolume;
    Volume lightVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topEyeLid = transform.GetChild(0).Find("TopEyeLid").GetComponent<RectTransform>();
        bottomEyeLid = transform.GetChild(0).Find("BottomEyeLid").GetComponent<RectTransform>();
        Volume[] volumeComponents = GetComponents<Volume>();
        darkVolume = volumeComponents[0];
        lightVolume = volumeComponents[1];
        darkVolume.weight = 1;
        lightVolume.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float lightLevel = GetLightLevel();
        darkVolume.weight = 1 - lightLevel;
        lightVolume.weight = lightLevel;

        if (Random.value < 0.01f && lightLevel > 0.5f)
        {
            blinking = true;
        }

        if (blinking)
        {
            Blink();
        }
    }

    public float testLightLevel = 0;
    float GetLightLevel()
    {
        return testLightLevel;
    }

    void MoveEyeLids()
    {
        float topLidY = Mathf.Lerp(blinkMoveDistance, 0, eyeLidOpenAmount);
        float bottomLidY = Mathf.Lerp(-blinkMoveDistance, 0, eyeLidOpenAmount);
        topEyeLid.anchoredPosition = new Vector2(topEyeLid.anchoredPosition.x, topLidY);
        bottomEyeLid.anchoredPosition = new Vector2(bottomEyeLid.anchoredPosition.x, bottomLidY);
    }

    void Blink()
    {
        blinkProgress += Time.deltaTime / blinkTime;
        if (blinkProgress >= 1.0f)
        {
            blinking = false;
            blinkProgress = 0.0f;
            eyeLidOpenAmount = 1.0f;
            MoveEyeLids();
        }
        else
        {
            float blinkAmount = Mathf.Sin(blinkProgress * Mathf.PI);
            eyeLidOpenAmount = Mathf.Clamp01(eyeLidOpenAmount - blinkAmount);
            MoveEyeLids();
        }
    }

}
