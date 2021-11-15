using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class Lantern : BaseInteraction, IInteractable
    {
        [SerializeField] private IntReference playerOrbCount;
        [SerializeField] private MeshRenderer lampLightMeshRenderer;
        
        private bool orbInLamp;
        private Light light;


        /// <summary>
        /// Gets whether or not the lamp is on or not....
        /// </summary>
        public bool IsLampLit => light.enabled;


        protected override void Awake()
        {
            base.Awake();
            light = GetComponentInChildren<Light>();
        }


        public void OnPlayerInteract()
        {
            if (!IsPlayerInZone || !IsPlayerInCorrectState) return;
            ToggleLamp();
        }
        
        
        protected override IInteractable GetInteractable() => this;


        private void ToggleLamp()
        {
            if (!orbInLamp)
            {
                if (playerOrbCount.Value <= 0) return;
                playerOrbCount.variable.IncrementValue(-1);
                PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
                orbInLamp = true;
                light.enabled = true;
                lampLightMeshRenderer.material.EnableKeyword("_EMISSION");
                return;
            }
            
            playerOrbCount.variable.IncrementValue(1);
            PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
            orbInLamp = false;
            light.enabled = false;
            lampLightMeshRenderer.material.DisableKeyword("_EMISSION");
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