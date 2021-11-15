using System;
using DependencyLibrary;
using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lever : BaseControlsPrompt, IInteractable
    {
        [SerializeField] private bool shouldBeGhost;
        [SerializeField] private BoolReference isPlayerGhost;
        private bool isPlayerInRange;

        public UnityEvent onLeverPulled;
        
        
        public void OnPlayerInteract(bool isInGhostForm)
        {
            if (!isPlayerInRange) return;
            if (shouldBeGhost != isInGhostForm) return;
            onLeverPulled?.Invoke();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (isPlayerInRange) return;
            if (shouldBeGhost != isPlayerGhost) return;
            if (!other.CompareTag("Player")) return;
            isPlayerInRange = true;
            ConfigureUI(true);
        }


        private void OnTriggerExit(Collider other)
        {
            if (!isPlayerInRange) return;
            if (shouldBeGhost != isPlayerGhost) return;
            if (!other.CompareTag("Player")) return;
            isPlayerInRange = false;
            ConfigureUI(false);
        }
    }
}