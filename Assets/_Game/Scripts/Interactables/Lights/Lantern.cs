using System;
using System.Collections;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class Lantern : MonoBehaviour, IInteractable
    {
        [SerializeField] private BoolReference isPlayerGhost;
        private int orbsInLamp;
        private Light light;
        private bool isInZone;

        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Coroutine fadeCo;

        public bool isLightActive => light.enabled;


        private void Awake()
        {
            canvas = GetComponentInChildren<Canvas>();
            canvasGroup = GetComponentInChildren<CanvasGroup>();
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

        private void ConfigureUI(bool enable)
        {
            if (fadeCo != null)
                StopCoroutine(fadeCo);
            
            fadeCo = StartCoroutine(enable ? FadeIn() : FadeOut());
        }


        private IEnumerator FadeIn()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += 3 * Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator FadeOut()
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= 3 * Time.deltaTime;
                yield return null;
            }
        }
    }
}