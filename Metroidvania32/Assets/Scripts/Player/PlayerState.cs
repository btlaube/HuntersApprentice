using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region PlayerState
public abstract class PlayerState
{
    protected PlayerComponents components;

    protected PlayerState(PlayerComponents playerComponents)
    {
        components = playerComponents;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
#endregion

#region IdleState
public class IdleState : PlayerState
{
    public IdleState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        // Debug.Log("Enter Idle");
        // playerMovement.currentJumps = 0;
        components.animator.SetFloat("Speed", 0.0f);
        components.playerMovement.StopMovement();
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
#endregion

#region RunningState
public class RunningState : PlayerState
{
    public RunningState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        // Debug.Log("Enter Running");
        // components.playerMovement.StartMovement();
    }

    public override void Update()
    {
        Debug.Log("Running");
        components.animator.SetFloat("Speed", Mathf.Abs(components.playerVelocity.GetMovementVelocity().x));
        Debug.Log($"Set animator speed to {Mathf.Abs(components.playerVelocity.GetMovementVelocity().x)}");
        components.playerMovement.MovementUpdate();
    }

    public override void Exit()
    {
        // Debug.Log("Exit Running");
        components.playerMovement.StopMovement();
    }
}
#endregion

#region FallingState
public class FallingState : PlayerState
{
    public FallingState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        components.animator.SetBool("IsFalling", true);
        // Debug.Log("Enter Falling");
        // playerMovement.rb.gravityScale = playerMovement.regGravityScale;

        // if (playerMovement.rb.velocity.y > 0)
        // {
        //     // Player released jump while rising
        //     playerMovement.rb.gravityScale = playerMovement.shortHopGravityScale;
        // }
        // else
        // {
        //     // Normal falling
        //     playerMovement.rb.gravityScale = playerMovement.regGravityScale;
        // }
    }

    public override void Update()
    {
        // Debug.Log("Falling");
        components.playerMovement.MovementUpdate();
        // components.playerJump.UpdateJumpInput();
        // playerMovement.CancelInputOnWall();
        // // Player has crossed into actual falling
        // if (playerMovement.rb.velocity.y <= 0)
        // {
        //     playerMovement.rb.gravityScale = playerMovement.regGravityScale;
        // }
    }

    public override void Exit()
    {
        components.animator.SetBool("IsFalling", false);
        // Debug.Log("Exit Falling");
        // components.playerMovement.StopMovement();
    }
}
#endregion

#region WallClingState
public class WallClingingState : PlayerState
{
    public WallClingingState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        // Debug.Log("Enter WallClinging");
        // playerAnimator.SetBool("IsOnWall", true);
        // playerMovement.rb.gravityScale = playerMovement.wallClingGravityScale;
        // playerMovement.SetYVelocity(0.0f);
        // playerMovement.currentJumps = 0;
    }

    public override void Update()
    {
        // Debug.Log("WallClinging");

        // Cancel horixontal input for on wall
        // playerMovement.CancelInputOnWall();

        // flip sprite on wall
        // int wallDirection = playerMovement.GetWallDirection();
        // if (wallDirection == -1)
        // {
        //     playerMovement.FlipSprite(false);
        // }
        // else if (wallDirection == 1)
        // {
        //     playerMovement.FlipSprite(true);
        // }
    }

    public override void Exit()
    {
        // Debug.Log("Exit WallClinging");
        // playerAnimator.SetBool("IsOnWall", false);
    }
}
#endregion

#region JumpingState
public class JumpingState : PlayerState
{
    public JumpingState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        // Debug.Log("Enter Jumping");
        components.animator.SetBool("IsJumping", true);
        // playerAudio.Play("Jump");
        // playerMovement.jumpParticles.Play();
        // playerMovement.Jump();
        components.playerJump.StartJump();
    }

    public override void Update()
    {
        // Debug.Log("Jumping");
        // playerMovement.JumpUpdate();
        components.playerJump.JumpUpdate();
        components.playerMovement.MovementUpdate();
    }

    public override void Exit()
    {
        // Debug.Log("Exit Jumping");
        components.animator.SetBool("IsJumping", false);
        components.playerJump.EndJump();
        // components.playerMovement.StopMovement();
    }
}
#endregion

#region WallJumpingState
public class WallJumpingState : PlayerState
{
    public WallJumpingState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        // Debug.Log("Enter WallJumping");
        // playerAnimator.SetBool("IsJumping", true);
        // playerAudio.Play("Jump");
        // playerMovement.wallJumpParticles.Play();
        // playerMovement.WallJump();
    }

    public override void Update()
    {
        // Debug.Log("WallJumping");
        // playerMovement.WallJumpUpdate();
    }

    public override void Exit()
    {
        // Debug.Log("Exit WallJumping");
        // playerAnimator.SetBool("IsJumping", false);
        // playerMovement.EndJump();
    }
}
#endregion

#region DashState
public class DashingState : PlayerState
{
    public DashingState(PlayerComponents components) : base(components) {}

    public override void Enter()
    {
        components.playerDash.StartDash();
        components.animator.SetBool("IsDashing", true);
        // movement.DisableFacing();
    }

    public override void Update()
    {
        components.playerDash.DashUpdate();
    }

    public override void Exit()
    {
        components.playerDash.EndDash();
        components.animator.SetBool("IsDashing", false);
        // movement.EnableFacing();
    }
}

#endregion


