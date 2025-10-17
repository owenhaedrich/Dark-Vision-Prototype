using UnityEngine;
using UnityEngine.XR;

public class StateMachineController : MonoBehaviour
{
    LightCoverageManager lightLevel;
    StateMachineStates lightStates;
    IState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets information from the folling scripts 
       lightLevel = GetComponent<LightCoverageManager>();
       lightStates = GetComponent<StateMachineStates>();
 

        //starts the game in a default state
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (currentState != null)
        {
            currentState.UpdateState();

            StateSwap();
        }
    }
    public void changeState(IState newstate)
    {
        if (currentState != null)
        {
            currentState.EnterState();
        }
        currentState = newstate;
        if (currentState != null)
        {
            currentState.EnterState();
        }
    }
    public void StateSwap()
    {
        if (lightLevel != null)
        {
            float lightPercent = lightLevel.playerLightLevel;
            if (lightPercent <= 0) 
            {
                changeState(new ExitLightState(this));
            if (lightPercent >= 5)
            {
                changeState(new EnterLightState(this));
            }
      }
    }
}
