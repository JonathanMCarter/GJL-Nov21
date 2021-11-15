using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lever : MonoBehaviour, IInteractable
    {
        public UnityEvent onLeverPulled;
        
        
        public void OnPlayerInteract(bool isInGhostForm)
        {
            if (!isInGhostForm) return;
            onLeverPulled?.Invoke();
        }
    }
}