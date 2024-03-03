using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pamux.Lib.Unity.Commons
{
    public class WaterLocomotionZoneAnimationEventHandler : MonoBehaviour
    {
        private WaterLocomotionZoneParameters parameters;
        private PamuxCharacterController pcc;

        private void Awake()
        {
            this.pcc = GetComponent<PamuxCharacterController>();
            this.parameters = pcc.WaterLocomotionZoneParameters;
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(
                    this.parameters.LandingAudioClip, 
                    this.pcc.transform.TransformPoint(pcc.CharacterController.center),
                    this.parameters.LandingAudioVolume
                );
            }
        }
    }
}