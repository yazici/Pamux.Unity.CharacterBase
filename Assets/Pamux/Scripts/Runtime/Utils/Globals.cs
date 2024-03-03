using UnityEditor.UI;
using UnityEngine.UIElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pamux.Lib.Unity.Commons
{
    public class Globals
    {
        private static Globals instance;
        public static Globals INSTANCE {
            get {
                if (instance == null) {
                    instance = new Globals();
                }
                return instance;
            }
        }

        public readonly GameObject gameObject; 
        public readonly UIDocument blankUIDocument;
        public readonly VisualElement blankUIDocumentRoot;
        public readonly Label interactionLabel;

        public Globals() {
            this.gameObject = GameObject.Find("Globals");

            foreach (var component in this.gameObject.GetComponentsInChildren<UIDocument>()) {
                if (component.gameObject.name == "BlankUIDocument") {
                    this.blankUIDocument = component;
                    break;
                }
            }

            this.interactionLabel = new Label("");

            this.blankUIDocumentRoot = this.blankUIDocument.rootVisualElement;
            this.blankUIDocumentRoot.Add(this.interactionLabel);
        }
    }
}