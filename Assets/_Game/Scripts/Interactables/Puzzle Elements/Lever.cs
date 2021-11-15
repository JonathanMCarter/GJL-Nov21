using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lever : BaseInteraction, IInteractable
    {
        public UnityEvent OnLeverPulled;
        
        
        public void OnPlayerInteract()
        {
            if (!IsPlayerInZone || !IsPlayerInCorrectState) return;
            OnLeverPulled?.Invoke();
        }


        protected override IInteractable GetInteractable() => this;


        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            base.OnPlayerEnterTriggerZone(other);
            ConfigureUI(true);
        }

        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }
    }
}