using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LightRaycast : MonoBehaviour
{
    [SerializeField] private float lightRadius;
    [SerializeField] GameObject player;
    [SerializeField] GameObject lightPoint;
    private bool withinRange = false;
    [SerializeField] private float distancePercent;
    [SerializeField] private float lightPercent;
    LayerMask layerMask;


    void Awake()
    {
        layerMask = LayerMask.GetMask("Player");
    }
    void Update()
    {
        Vector3 dirToPlayer = (player.transform.position - lightPoint.transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(lightPoint.transform.position, dirToPlayer, out hit, lightRadius, layerMask))
        {
            Debug.DrawRay(lightPoint.transform.position, dirToPlayer * lightRadius, Color.green);
            withinRange = true;
            distancePercent = (hit.distance / lightRadius) * 100;
            lightPercent = 100 - distancePercent;


        }
        else
        {
            Debug.DrawRay(lightPoint.transform.position, dirToPlayer * lightRadius, Color.red);
            withinRange = false;
            distancePercent = 100;
            lightPercent = 0;

        }
    }
}
