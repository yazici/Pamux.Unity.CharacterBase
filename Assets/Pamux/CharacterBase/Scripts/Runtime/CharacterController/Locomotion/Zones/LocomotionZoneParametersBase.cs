using System;
using UnityEngine;

namespace Pamux.Lib.Unity.CharacterBase.Locomotion.Zones {
    [Serializable]
    public class LocomotionZoneParametersBase : MonoBehaviour {

        // Default values are setup for Land locomotion

        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.5f;

        [Tooltip("Maximum interaction distance")]
        public float MaxInteractionDistance = 2.0f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float Acceleration = 10f;

        public AudioClip LandingAudioClip;
        [Range(0, 1)] public float LandingAudioVolume = 0.5f;


        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        protected PamuxCharacterController pcc;

        public float TimeScaledAcceleration => Time.deltaTime * Acceleration;

        public void SetPamuxCharacterController(PamuxCharacterController pcc)
        {
            this.pcc = pcc;
        }
    }
}