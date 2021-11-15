using System;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class GhostOrb : MonoBehaviour
    {
        [SerializeField] private BoolReference isPLayerGhost;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!isPLayerGhost.Value) return;
            if (!other.CompareTag("Player")) return;
            
            // TODO - Give player an orb...
            
            gameObject.SetActive(false);
        }
    }
}