using System;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class LockedDoor : BaseInteraction, IInteractable
    {
        private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
        private static readonly int CloseDoor = Animator.StringToHash("CloseDoor");
        
        public bool isUnlocked { get; set; }
        private bool isDoorOpen;
        private Animator anim;


        protected override void Awake()
        {
            base.Awake();
            anim = GetComponentInChildren<Animator>();
        }


        public void OnPlayerInteract()
        {
            if (!isUnlocked) return;

            anim.SetTrigger(isDoorOpen ? CloseDoor : OpenDoor);
            isDoorOpen = !isDoorOpen;
        }

        protected override IInteractable GetInteractable()
        {
            return this;
        }

        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            base.OnPlayerEnterTriggerZone(other);
            
            if (!isUnlocked) return;
            ConfigureUI(true);
        }


        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            base.OnPlayerExitTriggerZone(other);
            
            if (!isUnlocked) return;
            ConfigureUI(false);
        }
    }
}