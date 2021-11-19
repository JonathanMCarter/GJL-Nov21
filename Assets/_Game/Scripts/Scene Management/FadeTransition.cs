using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.Scenes
{
    public class FadeTransition : MonoBehaviour
    {
        private CanvasGroup transitionGroup;
        private Canvas canvas;
        private GraphicRaycaster graphicRaycaster;
        private Coroutine fadeCoroutine;

        
        public float speed = 1f;
        public bool startFadeIn = true;
        
        
        /// <summary>
        /// Gets called when a transition is first called...
        /// </summary>
        public static event Action OnTransitionCalled;
        
        /// <summary>
        /// Gets called when a transition is complete...
        /// </summary>
        public static event Action OnTransitionComplete;
        
        /// <summary>
        /// Gets called when a transition to black is complete...
        /// </summary>
        public static event Action OnTransitionFadeToBlack;
        
        /// <summary>
        /// Gets called when a transition to clear is complete...
        /// </summary>
        public static event Action OnTransitionFadeToClear;


        private static FadeTransition current;
        
        
        
        private void OnEnable()
        {
            DontDestroyOnLoad(this.gameObject);
            
            if (current == null)
                current = this;
            else
                Destroy(gameObject);
            
            transitionGroup = GetComponent<CanvasGroup>();
            canvas = GetComponent<Canvas>();
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            
            if (startFadeIn)
                FadeToClear();
        }


        /// <summary>
        /// Runs the fade transition to fade from clear to black...
        /// </summary>
        public void FadeToBlack()
        {
            if (canvas != null)
            {
                if (!canvas.enabled)
                    canvas.enabled = true;
            }

            if (graphicRaycaster != null)
            {
                if (!graphicRaycaster.enabled)
                    graphicRaycaster.enabled = true;
            }

            OnTransitionCalled?.Invoke();
            StopCurrentFade();
            fadeCoroutine = StartCoroutine(FadeCo(true, speed));
        }


        /// <summary>
        /// Runs the fade transition to fade from black to clear...
        /// </summary>
        public void FadeToClear()
        {
            if (canvas != null)
            {
                if (!canvas.enabled)
                    canvas.enabled = true;
            }
            
            OnTransitionCalled?.Invoke();
            StopCurrentFade();
            fadeCoroutine = StartCoroutine(FadeCo(false, speed));
        }


        /// <summary>
        /// Runs a fade in & out...
        /// </summary>
        public void FadeInOut()
        {
            if (canvas != null)
            {
                if (!canvas.enabled)
                    canvas.enabled = true;
            }
            
            OnTransitionCalled?.Invoke();
            StopCurrentFade();
            fadeCoroutine = StartCoroutine(FadeInOutCo(speed));
        }


        /// <summary>
        /// Stops the fade that is currently running...
        /// </summary>
        private void StopCurrentFade()
        {
            if (fadeCoroutine == null) return;
            StopCoroutine(fadeCoroutine);
        }


        /// <summary>
        /// Runs a fade in or out dependent on the bool entered...
        /// </summary>
        /// <param name="toBlack">Should the transition fade to black?</param>
        /// <param name="duration">How long should the fade take?</param>
        private IEnumerator FadeCo(bool toBlack, float duration = 1f)
        {
            var _currentTime = 0f;
            var _start = transitionGroup.alpha;
            var _targetValue = 0f;

            // Fade to black...
            if (toBlack)
            {
                _targetValue = 1f;

                while (_currentTime < duration)
                {
                    _currentTime += Time.unscaledDeltaTime;
                    transitionGroup.alpha = Mathf.Lerp(_start, _targetValue, _currentTime / duration);
                    yield return null;
                }

                transitionGroup.alpha = 1f;
                OnTransitionFadeToBlack?.Invoke();
                OnTransitionComplete?.Invoke();
                yield break;
            }

            // Fade to clear...
            _targetValue = 0f;

            while (_currentTime < duration)
            {
                _currentTime += Time.unscaledDeltaTime;
                transitionGroup.alpha = Mathf.Lerp(_start, _targetValue, _currentTime / duration);
                yield return null;
            }

            transitionGroup.alpha = 0f;
            OnTransitionFadeToClear?.Invoke();
            OnTransitionComplete?.Invoke();
        }


        /// <summary>
        /// Runs a fade in & out...
        /// </summary>
        /// <param name="duration">How long should the fade take?</param>
        private IEnumerator FadeInOutCo(float duration = 1f)
        {
            var _currentTime = 0f;
            var _start = transitionGroup.alpha;
            var _targetValue = 0f;

            _targetValue = 1f;

            while (_currentTime < duration)
            {
                _currentTime += Time.unscaledDeltaTime;
                transitionGroup.alpha = Mathf.Lerp(_start, _targetValue, _currentTime / duration);
                yield return null;
            }

            transitionGroup.alpha = 1f;
            OnTransitionFadeToBlack?.Invoke();
            
            // Reset for the reverse....
            _currentTime = 0f;
            _start = transitionGroup.alpha;
            _targetValue = 0f;

            while (_currentTime < duration)
            {
                _currentTime += Time.unscaledDeltaTime;
                transitionGroup.alpha = Mathf.Lerp(_start, _targetValue, _currentTime / duration);
                yield return null;
            }

            transitionGroup.alpha = 0f;
            OnTransitionFadeToClear?.Invoke();
            OnTransitionComplete?.Invoke();
        }
    }
}