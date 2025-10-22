using UnityEngine;

public class Eyes : MonoBehaviour
{
    StateMachineController stateMachineController;
    LightCoverageManager lightCoverageManager;
    EffectTransitions effectTransitions;
    public float lightLevel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectTransitions = GetComponentInChildren<EffectTransitions>();
        stateMachineController = FindObjectsByType<StateMachineController>(FindObjectsSortMode.None)[0];
        lightCoverageManager = stateMachineController.GetComponent<LightCoverageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightCoverageManager != null)
        {
            lightLevel = lightCoverageManager.playerLightLevel/20;
        }

        effectTransitions.testLightLevel = lightLevel;

    }
}
