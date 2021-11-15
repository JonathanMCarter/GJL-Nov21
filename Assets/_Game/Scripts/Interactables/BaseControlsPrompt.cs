using System.Collections;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class BaseControlsPrompt : MonoBehaviour
    {
        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Coroutine fadeCo;
        
        
        protected virtual void Awake()
        {
            canvas = GetComponentInChildren<Canvas>();
            canvasGroup = GetComponentInChildren<CanvasGroup>();
            ConfigureUI(false);
        }
        
        protected virtual void ConfigureUI(bool enable)
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