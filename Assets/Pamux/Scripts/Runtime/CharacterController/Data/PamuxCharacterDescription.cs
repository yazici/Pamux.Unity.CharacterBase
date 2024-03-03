using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    public class PamuxCharacterDescription : MonoBehaviour
    {
        public string Name;
        
        public GameObject characterPrefab;
        private Transform eyes;

        public Transform Eyes {
            get {
                if (this.eyes == null) {
                    this.eyes = this.transform.Find("PamuxCharacterEyes");
                    if (this.eyes == null) {
                        throw new Exception("Pamux character require a child object with the exact name PamuxCharacterEyes.");
                    }
                }
                return this.eyes;
            }
        }
        // public Transform pamuxCharacterLeftHand;
        // public Transform pamuxCharacterRightHand;
        // public Transform pamuxCharacterLeftFoot;
        // public Transform pamuxCharacterRightFoot;

        private float GetTotalMeshExtents(GameObject gameObject)
        {
            float minY = float.MaxValue;
            float maxY = float.MinValue;
            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                var bounds = renderer.bounds;

                if (bounds.max.y > maxY) {
                    maxY = bounds.max.y;
                }

                if (bounds.min.y < minY) {
                    minY = bounds.min.y;
                }
            }

            return maxY - minY;
        }

        private void Awake()
        {
            

            // https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
            var character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            character.transform.SetParent(this.transform, false);

            var height = GetTotalMeshExtents(character);

            var characterController = GetComponent<CharacterController>();
            characterController.height = height;
            characterController.center = new Vector3(characterController.center.x, characterController.center.y + height/2 + characterController.radius, characterController.center.z);

            // var playerCameraTarget = this.transform.Find("PlayerCameraTarget");
            // playerCameraTarget.transform.SetParent(character.transform, false);

            var characterAnimator = character.transform.GetComponent<Animator>();

            if (characterAnimator != null) {
                var animator = GetComponent<Animator>();
                animator.avatar = characterAnimator.avatar;

                Destroy(characterAnimator);
            }
        }

        private void CopyComponentData(Component from, Component to) {
            Type type = from.GetType();
            if (type != to.GetType()) {
                return; // TODO: throw
            }

            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

            foreach (var pinfo in type.GetProperties(flags)) {
                if (pinfo.CanWrite) {
                    try {
                        pinfo.SetValue(to, pinfo.GetValue(from, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }

            foreach (var finfo in type.GetFields(flags)) {
                finfo.SetValue(to, finfo.GetValue(from));
            }
        }
    }
}