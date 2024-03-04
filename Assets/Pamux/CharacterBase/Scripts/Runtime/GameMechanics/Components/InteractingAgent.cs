using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Pamux.Lib.Unity.CharacterBase;
using Pamux.Lib.Unity.CharacterBase.Authoring;

namespace Pamux.Lib.Unity.Commons
{
    public class InteractingAgent : MonoBehaviour, IInteractingAgent
    {
        private Transform raycastOrigin;
        private PamuxCharacterController pcc;
        private PamuxCharacterDescription pcd;


        private void Awake() {
            this.pcc = GetComponent<PamuxCharacterController>();
            this.pcd = GetComponent<PamuxCharacterDescription>();
            this.raycastOrigin = this.pcd.Eyes;
        }

        private List<Interactable> GetReachableInteractables() {
            var result = new List<Interactable>();

            Debug.DrawRay(this.raycastOrigin.position, this.raycastOrigin.forward * this.pcc.CurrentLocomotionZone.Parameters.MaxInteractionDistance, Color.red);

            if (!Physics.Raycast(
                this.raycastOrigin.position,
                this.raycastOrigin.forward,
                out RaycastHit hit,
                this.pcc.CurrentLocomotionZone.Parameters.MaxInteractionDistance,
                PamuxLayer.Masks.Interactables,
                QueryTriggerInteraction.Collide)) {
                return result;
            }

            var interactables = hit.collider.gameObject.GetComponentsInParent<Interactable>();
            if (interactables == null) {
                return result;
            }

            foreach (var interactable in interactables) {
                result.Add(interactable);
            }

            return result;
        }


        private void Update() {
            var interactables = GetReachableInteractables();
            foreach (var interactable in interactables) {
                interactable.ui.Show();
            }
        }

        public void OnInteract()
        {
            var interactables = GetReachableInteractables();
            foreach (var interactable in interactables) {
                interactable.Interact(this);
            }
        }
    }
}