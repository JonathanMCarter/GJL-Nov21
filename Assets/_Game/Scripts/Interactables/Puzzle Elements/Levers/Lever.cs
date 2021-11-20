using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lever : BaseInteraction, IInteractable
    {
        public UnityEvent OnLeverPulled;
        protected bool leverPulled;
        protected bool oneShot;


        public virtual void OnPlayerInteract()
        {
            if (leverPulled && !oneShot) return;
            if (!IsPlayerInZone || !IsPlayerInCorrectState) return;
            OnLeverPulled?.Invoke();
            leverPulled = true;
            ConfigureUI(false);
        }


        protected override IInteractable GetInteractable() => this;


        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            if (leverPulled && !oneShot) return;
            base.OnPlayerEnterTriggerZone(other);
            ConfigureUI(true);
        }

        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            if (leverPulled && !oneShot) return;
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }
    }
}