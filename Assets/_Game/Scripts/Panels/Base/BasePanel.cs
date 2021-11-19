using System.Collections;
using System.Collections.Generic;
using DependencyLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.UI
{
    public class BasePanel : MonoBehaviour
    {
        [SerializeField] protected string panelID;
        
        [SerializeField] protected List<TweenAnimationData> headerElements;
        [SerializeField] protected List<TweenAnimationData> optionElements;

        [SerializeField] protected FloatReference headerWait;
        [SerializeField] protected FloatReference optionsWait;
        
        protected Canvas canvas;
        protected GraphicRaycaster graphicRaycaster;
        [SerializeField] protected GameObject panel;
        protected WaitForSeconds waitHeading;
        protected WaitForSeconds waitOptions;

        protected Transform PanelObject => panel.transform;

        public string PanelID => panelID;
        
        
        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            waitHeading = new WaitForSeconds(headerWait);
            waitOptions = new WaitForSeconds(optionsWait);

            foreach (var obj in headerElements)
                obj.target.SetActive(false);
            
            foreach (var obj in optionElements)
                obj.target.SetActive(false);
        }
        

        public virtual void OpenPanel()
        {
            foreach (var _h in headerElements)
            {
                iTween.Stop(_h.target);
                _h.target.SetActive(false);
                _h.target.transform.localScale = Vector3.one;
            }
            
            foreach (var _o in optionElements)
            {
                iTween.Stop(_o.target);
                _o.target.SetActive(false);
                _o.target.transform.localScale = Vector3.one;
            }

            StartCoroutine(PanelOpenSequence());
        }


        public virtual void OpenPanelLogic(){}
        
        
        public virtual void ClosePanel()
        {
            TweenAnimationHelper.TweenPanelClose(panel.gameObject, gameObject);
        }
        
        
        public virtual void ClosePanelLogic()
        {
            if (canvas != null)
            {
                canvas.enabled = false;
                graphicRaycaster.enabled = false;
            }
            
            panel.transform.localScale = Vector3.one;

            foreach (var obj in headerElements)
            {
                obj.target.SetActive(false);
            }
            
            foreach (var obj in optionElements)
            {
                iTween.Stop(obj.target);
                obj.target.SetActive(false);
            }
        }
        
        
        protected virtual IEnumerator PanelOpenSequence()
        {
            if (canvas != null)
            {
                canvas.enabled = true;
                graphicRaycaster.enabled = true;
                TweenAnimationHelper.TweenPanelOpen(panel.gameObject, gameObject);
            }
            
            yield return ShowHeaderItems();
            yield return ShowOptions();
        }


        protected virtual IEnumerator ShowHeaderItems()
        {
            if (headerElements.Count <= 0) yield break;
            yield return waitHeading;
            
            foreach (var obj in headerElements)
            {
                RunTweenAnimation(obj);
                obj.target.SetActive(true);
            }
        }


        protected virtual IEnumerator ShowOptions()
        {
            if (optionElements.Count <= 0) yield break;
            yield return waitOptions;
            
            foreach (var obj in optionElements)
            {
                RunTweenAnimation(obj);
                obj.target.SetActive(true);
                yield return waitOptions;
            }
        }


        protected void RunTweenAnimation(TweenAnimationData data)
        {
            switch (data.type)
            {
                case TweenAnimationType.X:
                    TweenAnimationHelper.TweenFromX(data.target);
                    break;
                case TweenAnimationType.Y:
                    TweenAnimationHelper.TweenFromY(data.target);
                    break;
                default:
                    break;
            }
        }
    }
}