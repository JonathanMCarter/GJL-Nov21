using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lever : BaseInteraction, IInteractable
    {
        private static readonly int UseLever = Animator.StringToHash("UseLever");
        
        private Animator anim;
        public UnityEvent OnLeverPulled;
        protected bool leverPulled;
        protected bool oneShot;
    


        protected override void Awake()
        {
            base.Awake();
            anim = GetComponentInChildren<Animator>();
        }


        public virtual void OnPlayerInteract()
        {
            if (leverPulled && !oneShot) return;
            if (!IsPlayerInZone || !IsPlayerInCorrectState) return;
            OnLeverPulled?.Invoke();
            leverPulled = true;
            anim.SetTrigger(UseLever);
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