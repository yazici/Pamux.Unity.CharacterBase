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
    public class InteractableUI : MonoBehaviour
    {
        private Globals globals;

        public Font font;
        public string prompt;

        private void OnEnable() {
            this.globals = Globals.INSTANCE;

            this.globals.interactionLabel.text = this.prompt;
            Hide();

            this.globals.interactionLabel.style.unityFont = font;
            this.globals.interactionLabel.style.fontSize = 48;

            this.globals.interactionLabel.style.color = Color.red;

            this.globals.interactionLabel.style.position = new StyleEnum<Position>(Position.Absolute);
            this.globals.interactionLabel.style.left = 100;
            this.globals.interactionLabel.style.top = 100;
        }

        public void Show() {
            this.globals.interactionLabel.visible = true;
        }

        public void Hide() {
            this.globals.interactionLabel.visible = false;
        }

        private void Update() {
            Hide();
        }
    }
}