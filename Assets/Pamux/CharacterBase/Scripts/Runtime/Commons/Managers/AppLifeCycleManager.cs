using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    [RequireComponent(typeof(MouseCursorManager))]
    public class AppLifecycleManager : MonoBehaviour
    {
        private bool hasFocus = false;
        public bool HasFocus => hasFocus;

        private bool isPaused = false;
        public bool IsPaused => isPaused;

        public GameObject objectToActiveOnPause;

        private MouseCursorManager mouseCursorManager;

        private void Awake() {
            this.mouseCursorManager = GetComponent<MouseCursorManager>();

            Singletons.AppLifecycleManager = this;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            this.hasFocus = hasFocus;
        }

        private void OnApplicationPause(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        private void OnApplicationQuit()
        {
        }

        public void Pause()
        {
            this.isPaused = true;

            this.mouseCursorManager.ReleaseCursor();

            if (this.objectToActiveOnPause != null) {
                this.objectToActiveOnPause.SetActive(true);
            }
        }

        public void Resume()
        {
            if (this.objectToActiveOnPause != null) {
                this.objectToActiveOnPause.SetActive(false);
            }

            this.mouseCursorManager.LockCursor();

            this.isPaused = false;
        }

    }
}