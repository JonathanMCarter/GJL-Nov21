using System;
using System.Collections;
using DeadTired.Scenes;
using DependencyLibrary;
using MultiScene.Core;
using MultiScene.Extensions.DoNotDestroy;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.Death
{
    public class PlayerDeathController : MonoBehaviour
    {
        [SerializeField] private BoolReference isPlayerDead;
        
        private Canvas canvas;
        private GraphicRaycaster graphicRaycaster;
        private CanvasGroup canvasGroup;
        private Coroutine deathUICo;
        private SceneGroupChangeWithFade sceneManager;
        
        public static Action OnPlayedKilled;


        private void Awake()
        {
            canvas = GetComponentInChildren<Canvas>(true);
            graphicRaycaster = GetComponentInChildren<GraphicRaycaster>(true);
            canvasGroup = GetComponent<CanvasGroup>();
            sceneManager = GetComponent<SceneGroupChangeWithFade>();
        }

        private void OnEnable() => OnPlayedKilled += ShowDeathUI;
        private void OnDisable() => OnPlayedKilled -= ShowDeathUI;
        


        private void ShowDeathUI()
        {
            canvas.enabled = true;
            graphicRaycaster.enabled = true;
            Time.timeScale = 0;
            isPlayerDead.SetValue(true);
            
            if (deathUICo != null)
            {
                StopCoroutine(deathUICo);
                deathUICo = null;
            }

            deathUICo = StartCoroutine(DeathUIFadeIn());
        }


        private IEnumerator DeathUIFadeIn()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += 2 * Time.unscaledDeltaTime;
                yield return null;
            }
        }


        public void RetryLevel()
        {
            FadeTransition.OnTransitionFadeToBlack += RunRetry;
            DoNotDestroyAccessor.GetComponentInDoNotDestroy<FadeTransition>().FadeToBlack();
        }


        private void RunRetry()
        {
            FadeTransition.OnTransitionFadeToBlack -= RunRetry;
            SceneElly.GetComponentFromScene<MultiSceneManager>("Managers").LoadScenesKeepBase();
            Time.timeScale = 1;
            isPlayerDead.SetValue(false);
        }
        
        
        public void OpenMenu()
        {
            Time.timeScale = 1;
            isPlayerDead.SetValue(false);
            sceneManager.LoadSceneGroup();
        }
    }
}