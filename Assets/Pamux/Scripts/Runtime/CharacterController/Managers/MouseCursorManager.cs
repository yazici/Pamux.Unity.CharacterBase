using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    public class MouseCursorManager : MonoBehaviour
    {
        private InputAction click;
        private InputAction escape;

        private void Awake()
        {
            Singletons.MouseCursorManager = this;

// https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
#if UNITY_EDITOR
            ReleaseCursor();
            Cursor.visible = true;
#else
            LockCursor();
            Cursor.visible = false;
#endif
            click = new InputAction(binding: "<Mouse>/leftButton");
            click.performed += ctx => {
                LockCursor();
            };

            click.Enable();


            escape = new InputAction(binding: "<Keyboard>/escape");
            escape.performed += ctx => {
                ReleaseCursor();
            };
            escape.Enable();
        }

        public void LockCursor() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ConfineCursor() {
            Cursor.lockState = CursorLockMode.Confined;
        }

         public void ReleaseCursor() {
            Cursor.lockState = CursorLockMode.None;
        }

        public bool IsCursorLocked => Cursor.lockState == CursorLockMode.Locked;
        public bool IsCursorConfined => Cursor.lockState == CursorLockMode.Confined;

        public bool IsOwned => Cursor.lockState != CursorLockMode.None;
    }
}