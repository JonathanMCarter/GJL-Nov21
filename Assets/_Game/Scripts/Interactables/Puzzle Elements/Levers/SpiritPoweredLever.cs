using System;
using DeadTired.UI;
using DependencyLibrary;
using JTools;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.Interactables
{
    public class SpiritPoweredLever : Lever
    {
        [SerializeField] private IntReference playerOrbCount;
        [SerializeField] private Sprite needOrbSprite;
        [SerializeField] private Sprite normalSprite;
        [SerializeField, ReadOnly] private bool isPowered;
        [SerializeField, ReadOnly] private bool isPulled;

        private Image promptImage;

        public Action OnSpiritAdded;


        protected override void Awake()
        {
            base.Awake();
            promptImage = GetComponentInChildren<Image>();
            promptImage.sprite = isPowered ? normalSprite : needOrbSprite;
        }


        public override void OnPlayerInteract()
        {
            if (isPulled) return;
            if (!isPowered && playerOrbCount.Value > 0)
            {
                playerOrbCount.variable.IncrementValue(-1);
                PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
                isPowered = true;
                promptImage.sprite = isPowered ? normalSprite : needOrbSprite;
                OnSpiritAdded?.Invoke();
                return;
            }

            if (!isPowered) return;
            base.OnPlayerInteract();
            AkSoundEngine.PostEvent("Lockoff", gameObject);
            isPulled = true;
        }
    }
}