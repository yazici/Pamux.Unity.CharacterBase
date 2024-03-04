using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pamux.Lib.Unity.CharacterBase.Locomotion.Zones.Land
{
    [RequireComponent(typeof(LandLocomotionZoneParameters))]
    public class LandLocomotionZoneAnimationEventHandler : MonoBehaviour
    {
        private LandLocomotionZoneParameters parameters;
        private PamuxCharacterController pcc;

        private void Awake()
        {
            this.pcc = GetComponent<PamuxCharacterController>();
            this.parameters = GetComponent<LandLocomotionZoneParameters>();
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight >= 0.5f)
            {
                if (this.parameters.FootstepAudioClips.Length > 0)
                {
                    var index = UnityEngine.Random.Range(0, this.parameters.FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(
                        this.parameters.FootstepAudioClips[index],
                        this.pcc.transform.TransformPoint(pcc.CharacterController.center),
                        this.parameters.FootstepAudioVolume
                    );
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight >= 0.5f)
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