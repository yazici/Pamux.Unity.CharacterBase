using UnityEngine;

namespace Pamux.Lib.Unity.CharacterBase.Locomotion.Zones.Water
{
    [RequireComponent(typeof(WaterLocomotionZoneParameters))]
    public class WaterLocomotionZoneAnimationEventHandler : MonoBehaviour
    {
        private WaterLocomotionZoneParameters parameters;
        private PamuxCharacterController pcc;

        private void Awake()
        {
            this.pcc = GetComponent<PamuxCharacterController>();
            this.parameters = GetComponent<WaterLocomotionZoneParameters>();
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