using UnityEngine;

public class Eyes : MonoBehaviour
{

    EffectTransitions effectTransitions;
    public float lightLevel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectTransitions = GetComponentInChildren<EffectTransitions>();
    }

    // Update is called once per frame
    void Update()
    {
        effectTransitions.testLightLevel = lightLevel;
    }
}
