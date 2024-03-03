using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    public class PamuxCharacterControllerCamera
    {
        private readonly PamuxCharacterController pcc;

        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;

        protected const float threshold = 0.01f;

        protected readonly ParsedInput parsedInput;

        private LocomotionZoneParametersBase Parameters => pcc.ActiveLocomotionZoneParameters;

        public PamuxCharacterControllerCamera(PamuxCharacterController pcc)
        {
            this.pcc = pcc;
            this.parsedInput = this.pcc.ParsedInput;
        }

        public void Start()
        {
            this.cinemachineTargetYaw = Parameters.CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        }

        public void Update()
        {
        }

        public void LateUpdate()
        {
            HandleCameraRotation();
        }
        
        private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

        private void HandleCameraRotation()
        {
            if (!this.parsedInput.HasLook) {
                return;
            }

            if (this.parsedInput.Look.sqrMagnitude >= threshold && !Parameters.LockCameraPosition)
            {
                float deltaTimeMultiplier = pcc.IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                this.cinemachineTargetYaw += this.parsedInput.Look.x * deltaTimeMultiplier;
                this.cinemachineTargetPitch += this.parsedInput.Look.y * deltaTimeMultiplier;
            }

            this.cinemachineTargetYaw = ClampAngle(this.cinemachineTargetYaw, float.MinValue, float.MaxValue);
            this.cinemachineTargetPitch = ClampAngle(this.cinemachineTargetPitch, Parameters.BottomClamp, Parameters.TopClamp);

            Parameters.CinemachineCameraTarget.transform.rotation = 
                Quaternion.Euler(this.cinemachineTargetPitch + Parameters.CameraAngleOverride, this.cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) {
                lfAngle += 360f;
            }

            if (lfAngle > 360f) {
                lfAngle -= 360f;
            }

            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}