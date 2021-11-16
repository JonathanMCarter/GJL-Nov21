using DeadTired.Death;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class Darkness : MonoBehaviour
    {
        [SerializeField] private BoolReference isPlayerGhost;
        private BoxCollider triggerZone;
        private bool calledToKill;


        private void Awake()
        {
            if (triggerZone != null) return;
            triggerZone = GetComponent<BoxCollider>();
        }
        
        public void ToggleDarkness(bool value)
        {
            if (triggerZone == null)
                triggerZone = GetComponent<BoxCollider>();
            
            triggerZone.enabled = value;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (isPlayerGhost.Value) return;
            if (!other.CompareTag("Player")) return;
            if (calledToKill) return;
            KillPlayer();
        }

        private void OnTriggerStay(Collider other)
        {
            if (isPlayerGhost.Value) return;
            if (!other.CompareTag("Player")) return;
            if (calledToKill) return;
            KillPlayer();
        }


        private void KillPlayer()
        {
            PlayerDeathController.OnPlayedKilled?.Invoke();
        }
    }
}