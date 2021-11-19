using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired.Menu
{
    public class PageManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup startGroup;
        [SerializeField] private CanvasGroup current;
        [SerializeField] private CanvasGroup selected;

        [SerializeField] private List<CanvasGroup> groups;
        private Coroutine changePageCo;
        private int pos;
        private bool canMove;

        private Action OnPageOpened;

        private void Awake()
        {
            startGroup.alpha = 1;
            current = startGroup;
            pos = 0;
            canMove = true;
            OnPageOpened += PageOpened;
        }


        private void OnDisable()
        {
            OnPageOpened -= PageOpened;
        }


        private void Update()
        {
            MoveControls();
        }
        

        private void MoveControls()
        {
            if (!(Input.GetAxisRaw("Horizontal") > .1f) && !(Input.GetAxisRaw("Horizontal") < -.1f)) return;
            MoveInDirection();
        }


        private void PageOpened()
        {
            canMove = true;
        }

        
        private void MoveInDirection()
        {
            if (!canMove) return;
            if (Input.GetAxisRaw("Horizontal") > .1f)
                pos++;
            else
                pos--;

            if (pos < 0)
                pos = groups.Count - 1;

            if (pos >= groups.Count)
                pos = 0;
            
            ChangePage(groups[pos]);
            canMove = false;
        }


        public void ChangePage(CanvasGroup toShow)
        {
            if (selected != null)
                current = selected;

            selected = toShow;
            
            if (changePageCo != null)
                StopCoroutine(changePageCo);

            changePageCo = StartCoroutine(ShowNewPageCo());
        }
        

        private IEnumerator ShowNewPageCo()
        {
            if (current == null)
            {
                while (selected.alpha < 1f)
                {
                    selected.alpha += 5 * Time.deltaTime;
                    yield return null;
                }

                OnPageOpened?.Invoke();
                yield break;
            }
            
            while (current.alpha > 0 && selected.alpha < 1f)
            {
                current.alpha -= 5 * Time.deltaTime;
                selected.alpha += 5 * Time.deltaTime;
                yield return null;
            }

            current = selected;
            OnPageOpened?.Invoke();
        }
    }
}