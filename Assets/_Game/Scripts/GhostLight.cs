using UnityEngine;

namespace DeadTired.GhostMode
{
    public class GhostLight : MonoBehaviour
    {
        [SerializeField] private GameObject ghostEffects;


        private void OnEnable() => PlayerBehaviour.OnPlayerStateChanged += OnPlayerGhost;
        private void OnDisable() => PlayerBehaviour.OnPlayerStateChanged -= OnPlayerGhost;


        private void OnPlayerGhost(bool isGhost)
        {
            if (isGhost)
                EnableGhostEffects();
            else
                DisableGhostEffects();
        }
        

        private void EnableGhostEffects()
        {
            ghostEffects.SetActive(true);
        }

        private void DisableGhostEffects()
        {
            ghostEffects.SetActive(false);
        }
    }
}