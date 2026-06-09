using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region PlayerState
public abstract class PlayerState
{
    protected PlayerMovement playerMovement;
    protected Animator playerAnimator;
    protected AudioHandler playerAudio;

    protected PlayerState(PlayerComponents components)
    {
        playerMovement = components.playerMovement;
        playerAnimator = components.animator;
        playerAudio = components.audioHandler;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
#endregion

#region IdleState
public class IdleState : PlayerState
{
    public IdleState(PlayerComponents controller) : base(controller) {}

    public override void Enter()
    {
        // Debug.Log("Enter Idle");
        // playerMovement.currentJumps = 0;
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
    public RunningState(PlayerComponents controller) : base(controller) {}

    public override void Enter()
    {
        // Debug.Log("Enter Running");
    }

    public override void Update()
    {
        // Debug.Log("Running");
        // playerAnimator.SetFloat("Speed", Mathf.Abs(playerMovement.rb.velocity.x));
    }

    public override void Exit()
    {
        // Debug.Log("Exit Running");
        playerMovement.StopMovement();
    }
}
#endregion

#region FallingState
public class FallingState : PlayerState
{
    public FallingState(PlayerComponents controller) : base(controller) {}

    public override void Enter()
    {
        // playerAnimator.SetBool("IsFalling", true);
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
        // playerMovement.CancelInputOnWall();
        // // Player has crossed into actual falling
        // if (playerMovement.rb.velocity.y <= 0)
        // {
        //     playerMovement.rb.gravityScale = playerMovement.regGravityScale;
        // }
    }

    public override void Exit()
    {
        // playerAnimator.SetBool("IsFalling", false);
        // Debug.Log("Exit Falling");
    }
}
#endregion

#region WallClingState
public class WallClingingState : PlayerState
{
    public WallClingingState(PlayerComponents controller) : base(controller) {}

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
    public JumpingState(PlayerComponents controller) : base(controller) {}

    public override void Enter()
    {
        // Debug.Log("Enter Jumping");
        // playerAnimator.SetBool("IsJumping", true);
        // playerAudio.Play("Jump");
        // playerMovement.jumpParticles.Play();
        // playerMovement.Jump();
    }

    public override void Update()
    {
        // Debug.Log("Jumping");
        // playerMovement.JumpUpdate();
    }

    public override void Exit()
    {
        // Debug.Log("Exit Jumping");
        // playerAnimator.SetBool("IsJumping", false);
        // playerMovement.EndJump();
    }
}
#endregion

#region WallJumpingState
public class WallJumpingState : PlayerState
{
    public WallJumpingState(PlayerComponents controller) : base(controller) {}

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


