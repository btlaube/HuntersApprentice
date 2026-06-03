using UnityEngine;

public abstract class PlayerState
{

    protected PlayerState()
    {
        
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : PlayerState
{
    public IdleState() : base() {}

    public override void Enter()
    {
        // Debug.Log("Enter Idle");
    }

    public override void Update()
    {
        // Debug.Log("Idle");
    }

    public override void Exit()
    {
        // Debug.Log("Exit Idle");
    }
}