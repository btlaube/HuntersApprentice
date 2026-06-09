using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateManager : MonoBehaviour
{

    private PlayerInputHandler input;
    [SerializeField] private PlayerComponents playerComponents;

    public PlayerState CurrentState {get; private set;}

    void Awake()
    {
        input = MasterInputHandler.Instance.playerInput;

        // Initial state
        SetState(new IdleState(playerComponents));
    }

    void FixedUpdate()
    {
        HandleStateTransitions();
    }

    private void HandleStateTransitions()
    {
        if (CurrentState is IdleState)
        {
            // Transition from Idle to Running
            if (Mathf.Abs(input.horizontalInput.x) > 0.1f)
            {
                SwitchState(new RunningState(playerComponents));
            }
            else if (playerComponents.playerInput.isJumping && playerComponents.playerJump.ShouldJump())// && playerComponents.playerJump.currentJumps < playerComponents.playerJump.maxJumps)
            {
                SwitchState(new JumpingState(playerComponents));
            }
            else if (!playerComponents.playerCollision.isGrounded)
            {
                SwitchState(new FallingState(playerComponents));
            }
        }
        else if (CurrentState is RunningState)
        {
            // Transition from Running to Idle
            if (Mathf.Abs(input.horizontalInput.x) < 0.1f)
            {
                SwitchState(new IdleState(playerComponents));
            }
            else if (!playerComponents.playerCollision.isGrounded)
            {
                SwitchState(new FallingState(playerComponents));
            }
            // // Transition from Running to Jumping
            // else if (shouldJump && currentJumps < maxJumps)
            // {
            //     SwitchState(new JumpingState(this));
            // }
        }
        else if (CurrentState is FallingState)
        {
            // Transition from Falling
            if (playerComponents.playerCollision.isGrounded)
            {
                SwitchState(new IdleState(playerComponents));
            }
            // else if (shouldJump && currentJumps < maxJumps && hasDoubleJump)
            // {
            //     SwitchState(new JumpingState(playerComponents));
            // }
            // else if (PlayerIsOnWall() && hasWallCling)
            // {
            //     SwitchState(new WallClingingState(playerComponents));
            // }
        }
        // else if (CurrentState is WallClingingState)
        // {
        //     // Transition from WallCling
        //     if (PlayerIsOnGround())
        //     {
        //         SwitchState(new IdleState(this));
        //     }
        //     // Transition from WallCling to WallJumping
        //     else if (shouldJump && currentJumps < maxJumps)
        //     {
        //         SwitchState(new WallJumpingState(this));
        //     }
        //     else if (!PlayerIsOnWall())
        //     {
        //         SwitchState(new FallingState(this));
        //     }
        // }
        else if (CurrentState is JumpingState)
        {
            // Transition from Jumping
            if (playerComponents.playerJump.JumpEnded())
            {
                SwitchState(new FallingState(playerComponents));
            }
            // if (PlayerIsOnWall() && hasWallCling)
            // {
            //     SwitchState(new WallClingingState(this));
            // }
            if (playerComponents.playerCollision.hitCeiling)
            {
                SwitchState(new FallingState(playerComponents));
            }
        }
        // else if (CurrentState is WallJumpingState)
        // {
        //     // Transition from Wall Jumping
        //     if (jumpDuration > jumpDurationThreshold || (AllKeysUp("Move Up") && AllKeysUp("Jump")))
        //     {
        //         SwitchState(new FallingState(this));
        //     }
        //     if (PlayerIsOnWall() && hasWallCling)
        //     {
        //         SwitchState(new WallClingingState(this));
        //     }
        // }
    }

    public void SetState(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;
        CurrentState.Enter();
        Debug.Log($"Current State: {CurrentState}");
    }

    public void SwitchState(PlayerState newState)
    {
        SetState(newState);
    }

}
