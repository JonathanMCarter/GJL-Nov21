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

            AkSoundEngine.PostEvent("Open_Door", gameObject);
            
            isDoorOpen = !isDoorOpen;
        }

        protected override IInteractable GetInteractable() => this;

        public virtual void UnlockDoor()
        {

            IsUnlocked = true;
            AkSoundEngine.PostEvent("Unlocked", gameObject);
            OnDoorUnlocked?.Invoke();
        }

        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            base.OnPlayerEnterTriggerZone(other);

            promptImage.sprite = IsUnlocked ? normalSprite : lockedSprite;
            
            if (promptImage.sprite == lockedSprite)
            {
                AkSoundEngine.PostEvent("Closed_Door", gameObject);
            }
            
            ConfigureUI(true);
        }


        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }
    }
}