using System;
using UnityEngine;

namespace Pamux.Lib.Unity.CharacterBase.Utils {
    public class AnimatorParameters
    {
        public readonly FloatAnimatorParameter HorizontalSpeed;
        public readonly FloatAnimatorParameter VerticalSpeed;

        public readonly BoolAnimatorParameter IsJumping;
        public readonly BoolAnimatorParameter IsFalling;

        public readonly BoolAnimatorParameter IsInWater;


        public AnimatorParameters(Animator animator)
        {
            HorizontalSpeed = new FloatAnimatorParameter(animator, "HorizontalSpeed");
            VerticalSpeed = new FloatAnimatorParameter(animator, "VerticalSpeed");

            IsJumping = new BoolAnimatorParameter(animator, "IsJumping");
            IsFalling = new BoolAnimatorParameter(animator, "IsFalling");

            IsInWater = new BoolAnimatorParameter(animator, "IsInWater");
        }
    }
}