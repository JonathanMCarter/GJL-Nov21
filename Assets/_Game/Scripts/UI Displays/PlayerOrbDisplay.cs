using System;
using System.Collections;
using DependencyLibrary;
using JTools;
using TMPro;
using UnityEngine;

namespace DeadTired.UI
{
    public class PlayerOrbDisplay : MonoBehaviour
    {
        [SerializeField] private IntReference playerOrbCount;
        [SerializeField] private FloatReference activeTime;

        private CanvasGroup canvasGroup;
        private TMP_Text displayText;
        private WaitForSeconds wait;
        private Coroutine visualCo;


        public static Action OnOrbCountChanged;

        
        private void Awake()
        {
            wait = new WaitForSeconds(activeTime.Value);
            canvasGroup = GetComponent<CanvasGroup>();
            displayText = GetComponentInChildren<TMP_Text>();
        }


        private void OnEnable() => OnOrbCountChanged += UpdateTextDisplay;
        
        private void OnDisable()
        { 
            OnOrbCountChanged -= UpdateTextDisplay;
            
            if (visualCo != null)
                StopCoroutine(visualCo);
        }


        private void UpdateTextDisplay()
        {
            displayText.text = MoneyFormat.Format(playerOrbCount);
            
            if (visualCo != null)
                StopCoroutine(visualCo);

            visualCo = StartCoroutine(FadeInOut());
        }

        
        private IEnumerator FadeInOut()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += 2 * Time.deltaTime;
                yield return null;
            }

            yield return wait;
            
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= 2 * Time.deltaTime;
                yield return null;
            }
        }
    }
}