using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Controls.html#control-paths
// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionBindings.html#Composite%20Bindings
// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionType.html

// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Processors.html
//action.ApplyBindingOverride(new InputBinding { overrideProcessors = "StickDeadzone(min=0.25,max=0.95)" });
//action.ApplyBindingOverride(new InputBinding {overrideProcessors = "invertVector2(invertX=true,invertY=true)" });

namespace Pamux.Lib.Unity.CharacterBase.Input
{
    public class InputActionBuilder {
        private readonly PamuxCharacterController pcc;
        private readonly InputActionMap inputActionMap;

        private string name;
        private InputActionType inputActionType;

        private string key;
        private Dictionary<string, string> keys;
        private string controlType;
        private string bindingPath;
        private string bindingOverrideProcessors;

        private Action<InputAction.CallbackContext> onStarted;
        private Action<InputAction.CallbackContext> onCanceled;
        private Action<InputAction.CallbackContext> onPerformed;

        public InputActionBuilder(PamuxCharacterController pcc, InputActionMap inputActionMap) {
            this.pcc = pcc;
            this.inputActionMap = inputActionMap;
        }

        public InputActionBuilder WithName(string name) {
            this.name = name;
            return this;
        }

        public InputActionBuilder WithType(InputActionType inputActionType) {
            this.inputActionType = inputActionType;
            return this;
        }

        public InputActionBuilder ForKey(string value) {
            this.key = value;
            return this;
        }

        public InputActionBuilder ForKeys(Dictionary<string, string> value) {
            this.keys = value;
            return this;
        }

        public InputActionBuilder ForControlType(string value) {
            this.controlType = value;
            return this;
        }

        public InputActionBuilder WithBindingPath(string value) {
            this.bindingPath = value;
            return this;
        }

        public InputActionBuilder WithBindingOverrideProcessors(string value) {
            this.bindingOverrideProcessors = value;
            return this;
        }

        public InputActionBuilder OnStarted(Action<InputAction.CallbackContext> value) {
            this.onStarted = value;
            return this;
        }
        public InputActionBuilder OnCanceled(Action<InputAction.CallbackContext> value) {
            this.onCanceled = value;
            return this;
        }
        public InputActionBuilder OnPerformed(Action<InputAction.CallbackContext> value) {
            this.onPerformed = value;
            return this;
        }

        private bool TryAddSingleKeyBinding(InputAction inputAction) {
            if (this.key == null) {
                return false;
            }
            var binding = inputAction.AddBinding(path: $"<Keyboard>/{key}");
            if (this.bindingOverrideProcessors != null) {
                binding.WithProcessors(this.bindingOverrideProcessors);
            }
            return true;
        }

        private bool TryAddCompositeKeyBinding(InputAction inputAction) {
            if (this.keys == null) {
                return false;
            }
            var binding = inputAction.AddCompositeBinding(controlType);
            // if (this.bindingOverrideProcessors != null) {
            //     binding.WithProcessors(this.bindingOverrideProcessors);
            // }

            if (this.keys != null) {
                foreach (var part in this.keys) {
                    binding.With(part.Value, $"<Keyboard>/{part.Key}");
                }
            }
            return true;
        }

        private bool TryAddDeltaPointerBinding(InputAction inputAction) {
            if (!this.bindingPath.Equals("<Pointer>/delta")) {
                return false;
            }
            var binding = inputAction.AddBinding("<Pointer>/delta");
            if (this.bindingOverrideProcessors != null) {
                binding.WithProcessors(this.bindingOverrideProcessors);
            }
            return true;
        }

        private bool AddBinding(InputAction inputAction) {
            return TryAddSingleKeyBinding(inputAction) ||
                TryAddCompositeKeyBinding(inputAction) ||
                TryAddDeltaPointerBinding(inputAction);
        }

        public InputAction build() {
            var result = 
                this.inputActionMap.AddAction(
                    this.name, 
                    this.inputActionType);

            if (!AddBinding(result)) {
                throw new NotImplementedException("Unknown binding type for the input device");
            }

            if (this.onStarted != null) {
                result.started += ctx => {
                    if (pcc.ConsumeInputs) {
                        this.onStarted(ctx);
                    }
                };
            }

            if (this.onCanceled != null) {
                result.canceled += ctx => {
                    if (pcc.ConsumeInputs) {
                        this.onCanceled(ctx);
                    }
                };
            }

            if (this.onPerformed != null) {
                result.performed += ctx => {
                    if (pcc.ConsumeInputs) {
                        this.onPerformed(ctx);
                    }
                };
            }

            // Resetting the object for the next build.
            this.name = null;
            this.inputActionType = InputActionType.Value;
            this.key = null;
            this.keys = null;
            this.controlType = null;
            this.bindingPath = null;
            this.bindingOverrideProcessors = null;
            this.onStarted = null;
            this.onCanceled = null;
            this.onPerformed = null;

            return result;
        }
    }
}