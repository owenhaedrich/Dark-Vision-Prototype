using System.Collections.Generic;
using UnityEngine;

public class LightCoverageManager : MonoBehaviour
{
    LightRaycast[] lightSources;

    void Start()
    {
        lightSources = FindObjectsByType<LightRaycast>(FindObjectsSortMode.None);
        foreach (LightRaycast lightRaycast in lightSources)
        {
            Debug.Log("Found light source : " + lightRaycast.gameObject.name);
        }
        
    }
    
    void Update()
    {
        
    }
}
