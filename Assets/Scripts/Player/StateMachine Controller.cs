using UnityEngine;
using UnityEngine.XR;

public class StateMachineController : MonoBehaviour
{
    LightCoverageManager lightLevel;
    StateMachineStates lightStates;
    IState currentState;
    public bool needsRecovery = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets information from the folling scripts 
        lightLevel = GetComponent<LightCoverageManager>();
        lightStates = GetComponent<StateMachineStates>();


        //starts the game in a default state
        changeState(new ExitLightState(this));
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
            currentState.ExitState();
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
            }
            else if (lightPercent >= 5)
            {
            changeState(new EnterLightState(this));
            }
            else if (lightPercent >= 25)
            {
            changeState(new QuarterInState(this));
            }
            else if (lightPercent >= 50)
            {
                changeState(new HalfInState(this));
            }
            else if (lightPercent >= 75)
            {
                changeState(new ThreeQuarterInState(this));
            }
            else if (lightPercent >= 100)
            {
                changeState(new AllInState(this));
            }
        }
    }
}
