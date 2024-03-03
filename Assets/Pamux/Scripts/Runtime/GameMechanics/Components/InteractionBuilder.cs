using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UIElements;

namespace Pamux.Lib.Unity.Commons
{
    public class InteractionBuilder : MonoBehaviour
    {
        // https://ezhangdoor.com/how-to-determine-door-swing-direction/
        // https://knockety.com/door-terminology/#:~:text=Door%20Unit%3A%20An%20entire%20door%2C%20including%20frame%20jambs%2C,Dutch%20door.%20Draught%3A%20The%20gap%20at%20floor%20level.

        [Serializable]
        public class ActionTarget {
            public Transform obj;
            public string actionExpression;
        }

        // open,rotate,$x,100,$z:ajar,rotate,$x,10,$z:close,rotate,$x,$y,$z

        public Transform colliderParent;
        public float colliderRadius = 0.20f;

        public bool autoCalculateCollisionCenter;
        public List<ActionTarget> actionTargets;

        void Awake() {
            //  if (this.colliderParent != null)
            // {
            //     this.transform.SetParent(this.colliderParent, true);
            // }
            this.gameObject.layer = PamuxLayer.Ids.Interactables;


            if (this.transform.position == Vector3.zero) {
                // assume 0 rotation and scale
                var centerPos = new Vector3(0f, 0f, 0f);
                foreach (var actionTarget in this.actionTargets) {
                    centerPos += actionTarget.obj.position;
                }
                centerPos /= this.actionTargets.Count;
                this.transform.position = centerPos;
            }

            var collider = this.gameObject.AddComponent<SphereCollider>();
            collider.radius = this.colliderRadius;
            

            this.gameObject.AddComponent<Interactable>();

            Destroy(this);
        }
    }
}