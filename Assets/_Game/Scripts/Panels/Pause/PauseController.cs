using System.Collections;
using DeadTired.Scenes;
using DependencyLibrary;
using JTools;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.Pause
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private BoolReference isPaused;
        [SerializeField] private FloatReference transitionTime;
        [SerializeField] private BoolReference isPLayerDead;

        private Canvas canvas;
        private GraphicRaycaster graphicRaycaster;
        private CanvasGroup canvasGroup;
        private SceneGroupChangeWithFade sceneManager;
        private Coroutine pauseGameCo;

        
        private void Awake()
        {
            canvas = GetComponentInChildren<Canvas>(true);
            graphicRaycaster = GetComponentInChildren<GraphicRaycaster>(true);
            canvasGroup = GetComponent<CanvasGroup>();
            sceneManager = GetComponent<SceneGroupChangeWithFade>();
        }


        private void Update()
        {
            if (isPLayerDead.Value) return;
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            PauseGame();
        }


        public void PauseGame()
        {
            if (pauseGameCo != null)
                StopCoroutine(pauseGameCo);
            
            if (isPaused.Value)
            {
                pauseGameCo = StartCoroutine(PauseMenuVisuals(false));
                return;
            }

            Time.timeScale = 0;
            canvas.enabled = true;
            graphicRaycaster.enabled = true;
            pauseGameCo = StartCoroutine(PauseMenuVisuals(true));
        }
        


        private IEnumerator PauseMenuVisuals(bool pause)
        {
            if (pause)
            {
                while (canvasGroup.alpha < 1f)
                {
                    canvasGroup.alpha += transitionTime * Time.unscaledDeltaTime;
                    yield return null;
                }

                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                isPaused.SetValue(true);
            }
            else
            {
                while (canvasGroup.alpha > 0f)
                {
                    canvasGroup.alpha -= transitionTime * Time.unscaledDeltaTime;
                    yield return null;
                }
                
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                isPaused.SetValue(false);
                Time.timeScale = 1;
                canvas.enabled = false;
                graphicRaycaster.enabled = false;
            }
        }


        public void ReturnToMenu()
        {
            FadeTransition.OnTransitionFadeToBlack += ResumeTime;
            sceneManager.LoadSceneGroup();
        }


        private void ResumeTime()
        {
            FadeTransition.OnTransitionFadeToBlack -= ResumeTime;
            Time.timeScale = 1;
        }
    }
}