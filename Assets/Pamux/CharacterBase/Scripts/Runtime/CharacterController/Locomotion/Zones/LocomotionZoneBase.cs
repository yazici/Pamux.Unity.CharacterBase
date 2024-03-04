using UnityEngine;

using Pamux.Lib.Unity.CharacterBase.Input;
using Pamux.Lib.Unity.CharacterBase.Utils;

namespace Pamux.Lib.Unity.CharacterBase.Locomotion.Zones
{
    public abstract class LocomotionConstants {
        public const float GroundedGravity = -.05f;
        public const float EarthGravity = -9.81f;
        public const float MaxFallSpeed = -40.0f;
    }

    public abstract class LocomotionZoneBase
    {
        public LocomotionZoneParametersBase Parameters => this.parameters;

        protected LocomotionStateBase state;

        protected readonly PamuxCharacterController pcc;

        protected readonly CharacterController characterController;
        protected readonly ParsedInput parsedInput;
        protected readonly LocomotionZoneParametersBase parameters;

        public LocomotionZoneBase(PamuxCharacterController pcc, LocomotionZoneParametersBase parameters)
        {
            this.pcc = pcc;

            this.characterController = this.pcc.CharacterController;
            this.parsedInput = this.pcc.ParsedInput;

            this.parameters = parameters;

             this.parameters.SetPamuxCharacterController(pcc);
        }

        public virtual void Start() {}
        public virtual void Update() {}
        public virtual void LateUpdate() {}


        protected void HandleHorizontalMove() {
            var currentHorizontalSpeed = new Vector3(
                this.characterController.velocity.x,
                0.0f, 
                this.characterController.velocity.z
            ).magnitude;

            float speedOffset = 0.1f;

            var targetHorizontalSpeed = this.parsedInput.TargetHorizontalSpeed;

            if (currentHorizontalSpeed < targetHorizontalSpeed - speedOffset ||
                currentHorizontalSpeed > targetHorizontalSpeed + speedOffset)
            {
                this.state.HorizontalSpeed = Mathf.Lerp(
                    currentHorizontalSpeed,
                    targetHorizontalSpeed * this.parsedInput.MoveMagnitude,
                    this.Parameters.TimeScaledAcceleration
                );

                this.state.HorizontalSpeed = Mathf.Round(this.state.HorizontalSpeed * 1000f) / 1000f;
            }
            else
            {
                this.state.HorizontalSpeed = targetHorizontalSpeed;
            }

            if (this.parsedInput.HasMove)
            {
                var normalizedInputDirection = this.parsedInput.NormalizedInputDirection;

                this.state.targetRotation = Mathf.Atan2(normalizedInputDirection.x, normalizedInputDirection.z) * Mathf.Rad2Deg + pcc.CameraRotationInWorldSpace.y;

                var rotation = Mathf.SmoothDampAngle(
                    pcc.transform.eulerAngles.y,
                    this.state.targetRotation,
                    ref this.state.rotationSpeed,
                    this.Parameters.RotationSmoothTime
                );

                pcc.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            this.characterController.Move(
                (this.state.TargetDirection.normalized * this.state.HorizontalSpeed + new Vector3(0.0f, this.state.VerticalSpeed, 0.0f)) * Time.deltaTime
            );
        }

        public float currentWaterHeight;
        public bool isSwimming;
        [Tooltip("Diameter of player, for physics purposes. The larger this value, the more filtered/smooth" +
            "the wave response will be.")]
        public float playerWidth = 2f;

        protected void DetectWaterZone()
        {
#if USE_CREST_WATER
            // Get updated water height
            this.currentWaterHeight = UnderCrestWaterDetector.Master.GetCurrentWaterHeight(pcc.transform, playerWidth);

            // Calculate water - player height difference
            var waterPlayerHeightDifference = pcc.transform.position.y - currentWaterHeight;

            // Check if we are over or under water
            isSwimming = waterPlayerHeightDifference < UnderCrestWaterDetector.Master.maxHeightAboveWater;

            pcc.AnimatorParameters.IsInWater.Value = isSwimming;

            
            if (isSwimming) {
                pcc.CurrentLocomotionZone = pcc.WaterLocomotion;
            } else {
                pcc.CurrentLocomotionZone = pcc.LandLocomotion;
            }
#endif
        }
    }

    public abstract class LocomotionStateBase {
        protected readonly PamuxCharacterController pcc;
        protected readonly AnimatorParameters animatorParameters;

        protected const float TerminalVelocity = -53.0f;

        protected float gravity = LocomotionConstants.EarthGravity;

        public float targetRotation;

        public float rotationSpeed = 10.0f;

        public void StopResidualSpeedWhenGrounded() {
            if (this.verticalSpeed < 0.0f)
            {
                this.verticalSpeed = LocomotionConstants.GroundedGravity;
            }
        }

        private float horizontalSpeed = 0.0f;
        public float HorizontalSpeed { 
            get { 
                return this.horizontalSpeed; 
            }
            set {
                if (value == this.horizontalSpeed) {
                    return;
                }

                //Debug.Log($"HorizontalSpeed {value} was {this.horizontalSpeed}");
                this.animatorParameters.HorizontalSpeed.Value = this.horizontalSpeed = value;
            }
        }

        private float verticalSpeed = 0.0f;
        public float VerticalSpeed { 
            get { 
                return this.verticalSpeed; 
            }

            set {
                if (value < TerminalVelocity) {
                    value = TerminalVelocity;
                }

                if (value == this.verticalSpeed) {
                    return;
                }

                this.animatorParameters.VerticalSpeed.Value = this.verticalSpeed = value;
            }
        }

        public void ApplyGravity(float deltaTime) {
            this.VerticalSpeed += this.gravity * deltaTime;
        }

        protected LocomotionStateBase(PamuxCharacterController pcc) {
            this.pcc = pcc;
            this.animatorParameters = pcc.AnimatorParameters;
        }

        public Vector3 TargetDirection => Quaternion.Euler(0.0f, this.targetRotation, 0.0f) * Vector3.forward;
    }
}