using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pamux.Lib.Unity.Commons
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Quaternion originalRotation;
        [NonSerialized] public Vector3 originalScale;

        [NonSerialized] public int actionIndex;
        [NonSerialized] public string[] actionExpressions;

        [NonSerialized] public InteractableUI ui;

        void Awake()
        {
            SaveOriginalTransform();

            this.ui = GetComponent<InteractableUI>();
        }

        public void Interact(IInteractingAgent interactingAgent)
        {
            var parts = this.actionExpressions[this.actionIndex].Split(",");

            string name = parts[0];
            string command = parts[1];

            string x = parts[2];
            string y = parts[3];
            string z = parts[4];

            if (command == "rotate") {
                // "open,rotate,$x,100,$y" : Rotate around y 100 degrees ($x: original x, $y: original y, $y: original z)

                iTween.RotateTo(this.gameObject,iTween.Hash("rotation", GetRotation(x, y, z),"time",1.0f,"delay",0.0f,"isLocal",true));
                return;
            }
            
            if (command == "move") {
                // "open,move,$x,$y,0.55" : Action:Open Move to z=0.55, ($x: original x, $y: original y, $y: original z)

                iTween.MoveTo(this.gameObject,iTween.Hash("position", GetPosition(x, y, z),"time",1.0f,"delay",0.0f,"isLocal",true));
                return;
            }
        }

        // TODO: QUaternion
        public Vector3 GetRotation(string xStr, string yStr, string zStr) {
            return new Vector3(
                GetValueOrDefault(xStr, this.originalRotation),
                GetValueOrDefault(yStr, this.originalRotation),
                GetValueOrDefault(zStr, this.originalRotation)
            );
        }

        public Vector3 GetPosition(string xStr, string yStr, string zStr) {
            return new Vector3(
                GetValueOrDefault(xStr, this.originalPosition),
                GetValueOrDefault(yStr, this.originalPosition),
                GetValueOrDefault(zStr, this.originalPosition)
            );
        }

        public Vector3 GetScale(string xStr, string yStr, string zStr) {
            return new Vector3(
                GetValueOrDefault(xStr, this.originalScale),
                GetValueOrDefault(yStr, this.originalScale),
                GetValueOrDefault(zStr, this.originalScale)
            );
        }

        private void SaveOriginalTransform() {
            this.originalPosition =  this.transform.localPosition;
            this.originalRotation = this.transform.localRotation;
            this.originalScale = this.transform.localScale;
        }

        private float GetValueOrDefault(string str, Vector3 originalValue) {
            if (str.StartsWith("$")) {
                if (str.EndsWith("x")) {
                    return originalValue.x;
                }
                if (str.EndsWith("y")) {
                    return originalValue.y;
                }
                if (str.EndsWith("z")) {
                    return originalValue.z;
                }
            }

            return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        }

        private float GetValueOrDefault(string str, Quaternion originalValue) {
            if (str.StartsWith("$")) {
                if (str.EndsWith("x")) {
                    return originalValue.x;
                }
                if (str.EndsWith("y")) {
                    return originalValue.y;
                }
                if (str.EndsWith("z")) {
                    return originalValue.z;
                }
                if (str.EndsWith("w")) {
                    return originalValue.w;
                }
            }

            return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}