using System.Collections.Generic;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class InteractionsManager : MonoBehaviour
    {
        private List<IInteractable> validInteractions;

        private void Awake()
        {
            validInteractions = new List<IInteractable>();
        }

        public void AddInteraction(IInteractable interactable)
        {
            if (validInteractions.Contains(interactable)) return;
            validInteractions.Add(interactable);
        }

        public void RemoveInteraction(IInteractable interactable)
        {
            if (!validInteractions.Contains(interactable)) return;
            validInteractions.Remove(interactable);
        }

        public void TryInteract()
        {
            if (validInteractions.Count <= 0) return;

            foreach (var interaction in validInteractions)
                interaction.OnPlayerInteract();
        }
    }
}