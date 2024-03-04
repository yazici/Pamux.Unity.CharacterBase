using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.CharacterBase.Input
{
    public class ParsedInput
    {
        public bool analogMove = true;

        public Vector2 Move {
            get {
                return this.move;
            }

            set {
                this.move = value;
            }
        }
        private Vector2 move;
        public bool HasMove => this.move != Vector2.zero;

        public float MoveMagnitude => this.HasMove
            ? (this.analogMove ? this.move.magnitude : 1.0f)
            : 0.0f;

        public Vector3 NormalizedInputDirection => this.HasMove
            ? new Vector3(this.move.x, 0.0f, this.move.y).normalized
            : Vector3.zero;

        public float TargetHorizontalSpeed => this.HasMove
            ? (this.isSprintPressed ? this.pcc.CurrentLocomotionZone.Parameters.SprintSpeed : this.pcc.CurrentLocomotionZone.Parameters.MoveSpeed)
            : 0.0f;

        public Vector2 Look {
            get {
                return this.look;
            }

            set {
                this.look = value;
            }
        }
        private Vector2 look;
        public bool HasLook => this.look != Vector2.zero;

        public bool IsJumpPressed => this.isJumpPressed;
        private bool isJumpPressed = false;

        public bool IsSprintPressed => this.isSprintPressed;
        private bool isSprintPressed = false;

        private PamuxCharacterController pcc;
        private PamuxUnityCharacterBaseInputActions pamuxUnityCharacterBaseInputActions;

        public ParsedInput(PamuxCharacterController pcc) {
            this.pcc = pcc;

            this.pamuxUnityCharacterBaseInputActions = new PamuxUnityCharacterBaseInputActions();
        }

        public void Enable() {
            this.pamuxUnityCharacterBaseInputActions.Enable();
        }

        public void Disable() {
            this.pamuxUnityCharacterBaseInputActions.Disable();
        }

        //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Components.html

        public void Initialize() {
            var cc = this.pamuxUnityCharacterBaseInputActions.CharacterControls;

            SetupInputActionCallbacks(cc.Move,                      OnMove,     OnMove, OnMove);
            SetupInputActionCallbacks(cc.Look,                      null,       null,   OnLook);
            SetupInputActionCallbacks(cc.Jump,                      OnJump,     OnJump, null);
            SetupInputActionCallbacks(cc.Sprint,                    null,       null,   OnSprint);
            SetupInputActionCallbacks(cc.SwitchTo1stPersonCamera,   null,       null,   pcc.OnSwitchTo1stPersonCamera);
            SetupInputActionCallbacks(cc.SwitchTo3rdPersonCamera,   null,       null,   pcc.OnSwitchTo3rdPersonCamera);
            SetupInputActionCallbacks(cc.OpenRuntimeConsole,        null,       null,   pcc.OnOpenRuntimeConsole);
            SetupInputActionCallbacks(cc.Interact,                  null,       null,   pcc.OnInteract);
            SetupInputActionCallbacks(cc.OpenInventory,             null,       null,   pcc.OnOpenInventory);
        }

        private void SetupInputActionCallbacks(
            InputAction inputAction,
            Action<InputAction.CallbackContext> onStarted,
            Action<InputAction.CallbackContext> onCanceled,
            Action<InputAction.CallbackContext> onPerformed) {

            if (onStarted != null) {
                inputAction.started += ctx => {
                    if (pcc.ConsumeInputs) {
                        onStarted(ctx);
                    }
                };
            }

            if (onCanceled != null) {
                inputAction.canceled += ctx => {
                    if (pcc.ConsumeInputs) {
                        onCanceled(ctx);
                    }
                };
            }

            if (onPerformed != null) {
                inputAction.performed += ctx => {
                    if (pcc.ConsumeInputs) {
                        onPerformed(ctx);
                    }
                };
            }
        }

        private void OnMove(InputAction.CallbackContext context) {
            this.move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context) {
            this.look = context.ReadValue<Vector2>();
        }

        private void OnJump(InputAction.CallbackContext context) {
            this.isJumpPressed = context.ReadValueAsButton();
        }

        private void OnSprint(InputAction.CallbackContext context) {
            this.isSprintPressed = context.ReadValueAsButton();
        }

        public void OnInteract(InputAction.CallbackContext context) {
        }

        public void OnCrouch(InputAction.CallbackContext context) {
        }

        public void OnOpenInventory(InputAction.CallbackContext context) {
        }

        public void OnOpenMap(InputAction.CallbackContext context) {
        }

        public void OnFireWeaponA(InputAction.CallbackContext context) {
        }

        public void OnFireWeaponB(InputAction.CallbackContext context) {
        }

        public void OnReload(InputAction.CallbackContext context) {
        }

        public void OnClimbUp(InputAction.CallbackContext context) {
        }

        public void OnClimbDown(InputAction.CallbackContext context) {
        }

        public void OnProne(InputAction.CallbackContext context) {
        }

        public void OnPrecisionAim(InputAction.CallbackContext context) {
        }

        public void OnPickupItem(InputAction.CallbackContext context) {
        }

        public void OnDropItem(InputAction.CallbackContext context) {
        }

        public void OnHit(InputAction.CallbackContext context) {
        }

        public void OnOpenConsole(InputAction.CallbackContext context) {
        }

        public void OnZoom(InputAction.CallbackContext context) {
        }

        public void OnPan(InputAction.CallbackContext context) {
        }

        public void OnOrbitCam(InputAction.CallbackContext context) {
        }

        public void OnUseItem(InputAction.CallbackContext context) {
        }

        public void OnCycleNext(InputAction.CallbackContext context) {
        }

        public void OnCyclePrevious(InputAction.CallbackContext context) {
        }



        //https://forum.unity.com/threads/changing-inputactions-bindings-at-runtime.842188/
        //  public void StartInteractiveRebind()
        // {
        //     Action.action.Disable(); // critical before rebind!!!
        //     rebindOperation = Action.action.PerformInteractiveRebinding()
        //         .WithControlsExcluding("Mouse")
        //         .WithCancelingThrough("<Keyboard>/escape")
        //         .OnMatchWaitForAnother(0.2f)
        //         .Start();
        // }
    }
}