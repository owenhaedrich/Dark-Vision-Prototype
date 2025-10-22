using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LightCoverageManager : MonoBehaviour
{
    LightRaycast[] lightSources;
    float[] lightPercentages;
    public float playerLightLevel;

    int sources = 0;




    void Start()
    {
        lightSources = FindObjectsByType<LightRaycast>(FindObjectsSortMode.None);
        foreach (LightRaycast lightRaycast in lightSources)
        {
            Debug.Log("Found light source : " + lightRaycast.gameObject.name);
            sources = sources + 1;
        }
        lightPercentages = new float[sources];
        Debug.Log(sources + "sources");

    }

    void Update()
    {
        PlayerLightLevel();
    }

    private void PlayerLightLevel()
    {
        for (int i = 0; i < lightSources.Length; i++)
        {
            LightRaycast activeSource = lightSources[i].GetComponent<LightRaycast>();
            lightPercentages[i] = activeSource.lightPercent;
        }
        playerLightLevel = lightPercentages.Max();
        //Debug.Log("The player's light level is " + playerLightLevel);
    }
}
