using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pamux.Lib.Unity.Commons
{
    public interface IInteractable {
        void Interact(IInteractingAgent interactingAgent);
    }

    public interface IInteractableUI {
        void Show();
    }

    public interface IInteraction {
        void Interact(IInteractingAgent interactingAgent, IInteractable interactable);
    }
}