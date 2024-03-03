using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    [Serializable]
    public class WaterLocomotionZoneParameters : LocomotionZoneParametersBase {
        public static WaterLocomotionZoneParameters Create() {
            return new WaterLocomotionZoneParameters {
                MoveSpeed = 2.0f,
                SprintSpeed = 5.5f,
                RotationSmoothTime = 0.12f,
                Acceleration = 10.0f
            };
        }
    }
}