using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    public class LandLocomotion : LocomotionZoneBase
    {
        public class LocomotionState : LocomotionStateBase {
            private readonly LandLocomotion locomotion;

            public float initialJumpVelocity; // TODO: Discrete only?

            public float jumpCoolOffDurationDelta;
            public float fallStateGracePeriodDelta;

            private bool isJumping = false;
            public bool IsJumping { 
                get { 
                    return this.isJumping; 
                }
                set {
                    if (value == this.isJumping) {
                        return;
                    }

                    this.animatorParameters.IsJumping.Value = this.isJumping = value;
                }
            }

            public bool isFalling = false;
            public bool IsFalling { 
                get { 
                    return this.isFalling; 
                }
                set {
                    if (value == this.isFalling) {
                        return;
                    }

                    this.animatorParameters.IsFalling.Value = this.isFalling = value;
                }
            }

            public LocomotionState(LandLocomotion locomotion)
                : base(locomotion.pcc) {
                
                this.locomotion = locomotion;

                this.jumpCoolOffDurationDelta = this.locomotion.Parameters.JumpCoolOffDuration;
                this.fallStateGracePeriodDelta = this.locomotion.Parameters.FallStateGracePeriod;
            }

            public void EnterFallingState(float deltaTime) {
                if (this.fallStateGracePeriodDelta >= 0.0f)
                {
                    this.fallStateGracePeriodDelta -= deltaTime;
                }
                else
                {
                    this.IsFalling = true;
                }
            }
        }

        public PamuxPlayerInputActions playerInputActions;
        private new LandLocomotionZoneParameters Parameters => this.parameters as LandLocomotionZoneParameters;
        private LocomotionState State => this.state as LocomotionState;


        public LandLocomotion(PamuxCharacterController pcc)
            : base(pcc, pcc.LandLocomotionZoneParameters)
        {
            this.Parameters.SetPamuxCharacterController(pcc);

            this.state = new LocomotionState(this);
        }

#region Unity API
        public override void Update()
        {
            HandleHorizontalMove();

            this.State.ApplyGravity(Time.deltaTime);

            if (this.characterController.isGrounded) {
                HandleVerticalMove_Grounded();
            } else {
                HandleVerticalMove_NotGrounded();
            }

            DetectWaterZone();
        }
#endregion Unity API

#region Analog Input handling implementation
    private float GetRequiredVelocityToReach(float height) {
        return Mathf.Sqrt(height * this.Parameters.Gravity * -2f);
    }
    
    private void HandleVerticalMove_Grounded() {
        this.State.fallStateGracePeriodDelta = this.Parameters.FallStateGracePeriod;

        this.State.IsJumping = false;
        this.State.IsFalling = false;

        this.State.StopResidualSpeedWhenGrounded();

        if (this.State.jumpCoolOffDurationDelta > 0.0f)
        {
            this.State.jumpCoolOffDurationDelta -= Time.deltaTime;
        }
        else
        {
            if (pcc.ParsedInput.IsJumpPressed)
            {
                this.State.VerticalSpeed = GetRequiredVelocityToReach(this.Parameters.JumpHeight);
                this.State.IsJumping = true;
            }
        }
    }

    private void HandleVerticalMove_NotGrounded() {
        this.State.jumpCoolOffDurationDelta = this.Parameters.JumpCoolOffDuration;
        this.State.EnterFallingState(Time.deltaTime);
    }


#endregion
    }
}