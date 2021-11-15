using System;
using System.Collections;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class Lantern : BaseControlsPrompt, IInteractable
    {
        [SerializeField] private BoolReference isPlayerGhost;
        private int orbsInLamp;
        private Light light;
        private bool isInZone;
        

        public bool isLightActive => light.enabled;


        protected override void Awake()
        {
            ConfigureUI(false);
        }


        public void OnPlayerInteract(bool isInGhostForm)
        {
            if (!isInGhostForm) return;
        }


        private void FillLamp()
        {
            orbsInLamp++;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!isPlayerGhost.Value) return;
            if (!other.CompareTag("Player")) return;
            if (isInZone) return;
            ConfigureUI(true);
            isInZone = true;
        }

        
        private void OnTriggerExit(Collider other)
        {
            if (!isPlayerGhost.Value) return;
            if (!other.CompareTag("Player")) return;
            if (!isInZone) return;
            ConfigureUI(false);
            isInZone = false;
        }
    }
}