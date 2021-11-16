using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class Lantern : BaseInteraction, IInteractable
    {
        [SerializeField] private IntReference playerOrbCount;
        [SerializeField] private MeshRenderer lampLightMeshRenderer;
        
        [SerializeField] private bool orbInLamp;
        private Light light;


        public UnityEvent OnLanternLit;
        public UnityEvent OnLanternExtinguished;


        /// <summary>
        /// Gets whether or not the lamp is on or not....
        /// </summary>
        public bool IsLampLit => light.enabled;


        protected override void Awake()
        {
            base.Awake();
            light = GetComponentInChildren<Light>();

            if (orbInLamp)
                TurnLightOn();
            else
                TurnLightOff();
        }


        public void OnPlayerInteract()
        {
            if (!IsPlayerInZone || !IsPlayerInCorrectState) return;
            ToggleLamp();
        }
        
        
        protected override IInteractable GetInteractable() => this;


        protected virtual void ToggleLamp()
        {
            if (!orbInLamp)
            {
                TurnLightOn();
                return;
            }

            TurnLightOff();
        }


        protected virtual void TurnLightOn()
        {
            if (playerOrbCount.Value <= 0) return;
            playerOrbCount.variable.IncrementValue(-1);
            PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
            orbInLamp = true;
            light.enabled = true;
            lampLightMeshRenderer.material.EnableKeyword("_EMISSION");
            OnLanternLit?.Invoke();
        }
        
        
        protected virtual void TurnLightOff()
        {
            playerOrbCount.variable.IncrementValue(1);
            PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
            orbInLamp = false;
            light.enabled = false;
            lampLightMeshRenderer.material.DisableKeyword("_EMISSION");
            OnLanternExtinguished?.Invoke();
        }


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