using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.Interactables
{
    public class LockedDoor : BaseInteraction, IInteractable
    {
        private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
        private static readonly int CloseDoor = Animator.StringToHash("CloseDoor");

        [SerializeField] private Sprite lockedSprite;
        private Sprite normalSprite;
        private Image promptImage;
        
        [field: SerializeField] public bool isUnlocked { get; set; }
        private bool isDoorOpen;
        private Animator anim;


        protected override void Awake()
        {
            base.Awake();
            anim = GetComponentInChildren<Animator>();
            promptImage = GetComponentInChildren<Image>();
            normalSprite = promptImage.sprite;
        }


        public void OnPlayerInteract()
        {
            if (!isUnlocked) return;

            anim.SetTrigger(isDoorOpen ? CloseDoor : OpenDoor);
            isDoorOpen = !isDoorOpen;
        }

        protected override IInteractable GetInteractable() => this;

        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            base.OnPlayerEnterTriggerZone(other);

            promptImage.sprite = isUnlocked ? normalSprite : lockedSprite;
            ConfigureUI(true);
        }


        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }
    }
}