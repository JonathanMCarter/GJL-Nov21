using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lever : BaseInteraction, IInteractable
    {
        public UnityEvent OnLeverPulled;
        private bool leverPulled;
        
        
        public virtual void OnPlayerInteract()
        {
            if (leverPulled) return;
            if (!IsPlayerInZone || !IsPlayerInCorrectState) return;
            OnLeverPulled?.Invoke();
            leverPulled = true;
            ConfigureUI(false);
        }


        protected override IInteractable GetInteractable() => this;


        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            if (leverPulled) return;
            base.OnPlayerEnterTriggerZone(other);
            ConfigureUI(true);
        }

        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            if (leverPulled) return;
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }
    }
}