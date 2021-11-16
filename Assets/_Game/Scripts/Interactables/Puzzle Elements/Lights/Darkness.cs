using System;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class Darkness : MonoBehaviour
    {
        [SerializeField] private Transform dragSpot;
        private BoxCollider triggerZone;


        private void Awake()
        {
            triggerZone = GetComponent<BoxCollider>();
        }


        private void ToggleDarkness()
        {
            triggerZone.enabled = !triggerZone.enabled;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
         
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
     
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
       
        }


        private void KillPlayer()
        {
            
        }
    }
}