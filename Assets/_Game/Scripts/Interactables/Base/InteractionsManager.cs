using System.Collections.Generic;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class InteractionsManager : MonoBehaviour, IMultiSceneAwake
    {
        private List<IInteractable> validInteractions;

        public void OnMultiSceneAwake()
        {
            validInteractions = new List<IInteractable>();
        }

        private void Awake()
        {
            validInteractions = new List<IInteractable>();
        }

        public void AddInteraction(IInteractable interactable)
        {
            if (interactable == null) return;
            if (validInteractions.Contains(interactable)) return;
            validInteractions.Add(interactable);
        }

        public void RemoveInteraction(IInteractable interactable)
        {
            if (interactable == null) return;
            if (!validInteractions.Contains(interactable)) return;
            validInteractions.Remove(interactable);
        }

        public void TryInteract()
        {
            if (validInteractions.Count <= 0) return;

            foreach (var interaction in validInteractions)
            {
                if (interaction == null) continue;
                interaction.OnPlayerInteract();
            }
        }


        public bool HasInteraction(IInteractable toCheck)
        {
            return validInteractions.Contains(toCheck);
        }
    }
}