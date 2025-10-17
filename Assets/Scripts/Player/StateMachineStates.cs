using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}

public class StateMachineStates : MonoBehaviour
{
    IState currentState;

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
    public void ChangeState(IState newstate)
    {
        currentState.ExitState();
        currentState = newstate;
        currentState.EnterState();
    }
}
public class EnterLightState : IState
{
    public void EnterState()
    {
        Debug.Log("Entering Enter light State");
        
    }
    public void UpdateState()
    {
        //needsRecovery = true;
        //Entering Light effects
    }
    public void ExitState()
    {
        Debug.Log("Exiting Enter light State");
    }
}
public class ExitLightState: IState
{
    public void EnterState()
    {
        Debug.Log("Entering exit light State");
    
    }
    public void UpdateState()
    {    //if (needsRecovery)
        //{ recovery effects
        //needsRecovery = false;}
    }
    public void ExitState()
    {
        Debug.Log("Exiting exit light State");
    }
}
public class QuarterInState : IState
{
    public void EnterState()
    {
        Debug.Log("Entering 25% light State");
        
    }
    public void UpdateState()
    {
        //25% effects
    }
    public void ExitState()
    {
        Debug.Log("Exiting 255 light State");
    }
}
public class HalfInState : IState
{
    public void EnterState()
    {
        Debug.Log("Entering 50% light State");

    }
    public void UpdateState()
    {
        //50% effects
    }
    public void ExitState()
    {
        Debug.Log("Exiting 50% light State");
    }
}
public class ThreeQuarterInState : IState
{
    public void EnterState()
    {
        Debug.Log("Entering 75% light State");

    }
    public void UpdateState()
    {
        //75% effects
    }
    public void ExitState()
    {
        Debug.Log("Exiting 75% light State");
    }
}
public class AllInState : IState
{
    public void EnterState()
    {
        Debug.Log("Entering 100% light State");

    }
    public void UpdateState()
    {
        //100% effects
    }
    public void ExitState()
    {
        Debug.Log("Exiting 100% light State");
    }
}
