using System.Collections;
using DependencyLibrary;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class BaseInteraction : MonoBehaviour, IMultiSceneEnable
    {
        [SerializeField] protected BoolReference isPlayerGhost;
        [SerializeField] protected bool shouldPlayerBeGhost;
        
        protected bool IsPlayerInZone { get; private set; }
        protected bool IsPlayerInCorrectState => shouldPlayerBeGhost.Equals(isPlayerGhost);
        
        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Coroutine fadeCo;
        protected InteractionsManager interactionsManager;


        protected virtual void Awake()
        {
            canvas = GetComponentInChildren<Canvas>();
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }


        public void OnMultiSceneEnable()
        {
            interactionsManager = SceneElly.GetComponentFromAllScenes<InteractionsManager>();

            if (canvas == null)
            {
                canvas = GetComponentInChildren<Canvas>();
                canvasGroup = GetComponentInChildren<CanvasGroup>();    
            }
            
            canvas.worldCamera = SceneElly.GetComponentFromScene<Camera>("Player");
            canvas.transform.rotation = canvas.worldCamera.transform.rotation;
            ConfigureUI(false);
        }


        protected virtual IInteractable GetInteractable()
        {
            return null;
        }
        

        protected virtual void ConfigureUI(bool enable)
        {
            if (!gameObject.activeInHierarchy) return;
            
            if (fadeCo != null)
                StopCoroutine(fadeCo);
            
            fadeCo = StartCoroutine(enable ? FadeIn() : FadeOut());
        }


        private void OnTriggerEnter(Collider other)
        {
            OnOtherEnterTriggerZone(other);
            if (!shouldPlayerBeGhost.Equals(isPlayerGhost)) return;
            if (!other.CompareTag("Player")) return;
            IsPlayerInZone = true;
            OnPlayerEnterTriggerZone(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnOtherExitTriggerZone(other);
            if (!other.CompareTag("Player")) return;
            IsPlayerInZone = false;
            OnPlayerExitTriggerZone(other);
        }


        protected virtual void OnPlayerEnterTriggerZone(Collider other)
        {
            interactionsManager.AddInteraction(GetInteractable());
        }
        
        
        protected virtual void OnPlayerExitTriggerZone(Collider other)
        {
            interactionsManager.RemoveInteraction(GetInteractable());
        }


        protected virtual void OnOtherEnterTriggerZone(Collider other)
        {
        }
        
        
        protected virtual void OnOtherExitTriggerZone(Collider other)
        {
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