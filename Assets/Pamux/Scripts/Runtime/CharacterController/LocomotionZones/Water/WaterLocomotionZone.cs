using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    public class WaterLocomotion : LocomotionZoneBase
    {
        private new WaterLocomotionZoneParameters Parameters => this.parameters as WaterLocomotionZoneParameters;
        private LocomotionState State => this.state as LocomotionState;

        public class LocomotionState : LocomotionStateBase {
            private readonly WaterLocomotion locomotion;
            private float buoyancy = -.7f * LocomotionConstants.EarthGravity;

            public LocomotionState(PamuxCharacterController pcc)
                : base(pcc) {

                this.locomotion = pcc.WaterLocomotion;
            }

            public void ApplyBuoyancy(float deltaTime) {
                this.VerticalSpeed += this.buoyancy * deltaTime;
            }

        }

        public WaterLocomotion(PamuxCharacterController pcc)
            : base(pcc, pcc.WaterLocomotionZoneParameters)
        {
            Parameters.SetPamuxCharacterController(pcc);

            this.state = new LocomotionState(pcc);
        }

#region Unity API
        public override void Update()
        {
            HandleHorizontalMove();

            var targetY1 = -1.45f;
            var targetY2 = -1.40f;


            if (this.pcc.transform.position.y < targetY1) {
                var diff = Mathf.Abs(this.pcc.transform.position.y - targetY1) / 10f;
                this.State.ApplyGravity(-Time.deltaTime * diff);
            } else if (this.pcc.transform.position.y > targetY2) {
                var diff = Mathf.Abs(this.pcc.transform.position.y - targetY2) / 10f;
                this.State.ApplyGravity(Time.deltaTime * diff);
            } else {
                this.state.VerticalSpeed = 0.0f;
            }


            DetectWaterZone();
        }
#endregion Unity API

        public override void Start()
        {

        }

        public override void LateUpdate()
        {

        }
    }
}