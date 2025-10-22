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
    public void changeState(IState newstate)
    {
        currentState.ExitState();
        currentState = newstate;
        currentState.EnterState();
    }
}
public class EnterLightState : IState
{
    private StateMachineController controller;

    public EnterLightState(StateMachineController stateMachineController)
    {
        this.controller = stateMachineController;
    }
    public void EnterState()
    {
        Debug.Log("Entering Enter light State");
        
    }
    public void UpdateState()
    {
        controller.needsRecovery = true;
        //Entering Light effects
    }
    public void ExitState()
    {
        Debug.Log("Exiting Enter light State");
    }
}
public class ExitLightState: IState
{
    private StateMachineController controller;

    public ExitLightState(StateMachineController stateMachineController)
    {
        this.controller = stateMachineController;
    }

    public void EnterState()
    {
        Debug.Log("Entering exit light State");
    
    }
    public void UpdateState()
    {   if (controller.needsRecovery)
        { //recovery effects
        controller.needsRecovery = false;
        Debug.Log("Recovering");
        }
    }
    public void ExitState()
    {
        Debug.Log("Exiting exit light State");
    }
}
public class QuarterInState : IState
{
    private StateMachineController stateMachineController;

    public QuarterInState(StateMachineController stateMachineController)
    {
        this.stateMachineController = stateMachineController;
    }
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
        Debug.Log("Exiting 25% light State");
    }
}
public class HalfInState : IState
{
    private StateMachineController stateMachineController;

    public HalfInState(StateMachineController stateMachineController)
    {
        this.stateMachineController = stateMachineController;
    }
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
    private StateMachineController stateMachineController;

    public ThreeQuarterInState(StateMachineController stateMachineController)
    {
        this.stateMachineController = stateMachineController;
    }
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
    private StateMachineController stateMachineController;

    public AllInState(StateMachineController stateMachineController)
    {
        this.stateMachineController = stateMachineController;
    }
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
