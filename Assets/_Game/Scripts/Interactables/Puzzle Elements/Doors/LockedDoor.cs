using UnityEngine;
using UnityEngine.Events;
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
        
        [field: SerializeField] public bool IsUnlocked { get; protected set; }
        private bool isDoorOpen;
        private Animator anim;

        public UnityEvent OnDoorUnlocked;


        protected override void Awake()
        {
            base.Awake();
            anim = GetComponentInChildren<Animator>();
            promptImage = GetComponentInChildren<Image>();
            normalSprite = promptImage.sprite;
        }


        public virtual void OnPlayerInteract()
        {
            if (!IsUnlocked) return;

            anim.SetTrigger(isDoorOpen ? CloseDoor : OpenDoor);
            isDoorOpen = !isDoorOpen;
        }

        protected override IInteractable GetInteractable() => this;

        public virtual void UnlockDoor()
        {
            IsUnlocked = true;
            OnDoorUnlocked?.Invoke();
        }

        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            base.OnPlayerEnterTriggerZone(other);

            promptImage.sprite = IsUnlocked ? normalSprite : lockedSprite;
            ConfigureUI(true);
        }


        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }
    }
}