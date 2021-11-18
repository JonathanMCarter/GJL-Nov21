using DeadTired.Interactables;
using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.Sokoban
{
    public class SokobanSpiritBlock : SokobanBlock
    {
        [SerializeField] private IntReference playerOrbCount;
        [SerializeField] private bool isPowered;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite needOrbSprite;
        private Image promptImage;


        protected override void Awake()
        {
            base.Awake();
            promptImage = GetComponentInChildren<Image>();
            promptImage.sprite = isPowered ? normalSprite : needOrbSprite;
        }


        public override void OnPlayerInteract()
        {
            if (isPowered)
                base.OnPlayerInteract();
            else
            {
                if (playerOrbCount.Value <= 0) return;
                playerOrbCount.variable.IncrementValue(-1);
                PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
                isPowered = true;
                promptImage.sprite = isPowered ? normalSprite : needOrbSprite;
            }
        }
    }
}