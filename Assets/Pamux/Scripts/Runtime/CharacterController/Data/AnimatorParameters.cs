using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameters
{
    public abstract class AnimatorParameter<TParameterType>
    {
        protected Animator animator;
        protected string name;
        protected int hash;

        protected AnimatorParameter(Animator animator, string name)
        {
            this.animator = animator;
            this.name = name;

            if (this.animator != null)
            {
                this.hash = Animator.StringToHash(name);
            }
        }

        public TParameterType Value
        {
            get 
            {
                return animator == null ? GetImpl() : default(TParameterType);
            }

            set
            {
                if (animator != null)
                {
                    SetImpl(value);
                }
            }
        }

        protected abstract TParameterType GetImpl();
        protected abstract void SetImpl(TParameterType value);
    }

    public class BoolAnimatorParameter : AnimatorParameter<Boolean>
    {
        public BoolAnimatorParameter(Animator animator, string name) 
            : base(animator, name) 
        {
        }

        protected override Boolean GetImpl()
        {
            return animator.GetBool(this.hash);
        }

        protected override void SetImpl(Boolean value)
        {
            animator.SetBool(this.hash, value);
        }
    }

    public class FloatAnimatorParameter : AnimatorParameter<Single>
    {
        public FloatAnimatorParameter(Animator animator, string name) 
            : base(animator, name) 
        {
        }

        protected override Single GetImpl()
        {
            return animator.GetFloat(this.hash);
        }

        protected override void SetImpl(Single value)
        {
            animator.SetFloat(this.hash, value);
        }
    }

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
