using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DeadTired.Scenes;
using JTools.ScriptableObjects;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.Dialogue
{
    public class DialogueManager : MonoBehaviour, IMultiSceneAwake
    {
        [SerializeField] private StringCollection dialogue;
        [SerializeField] private DialogueDisplay display;
        
        private int dialPosition;
        private bool canContinue;
        private WaitForSeconds dialWait;
        private Coroutine dialCo;
        private SceneGroupChangeWithFade fade;
        
        private bool HasFinishedFile => dialPosition >= dialogue.collection.Length;



        private void Awake()
        {
            dialPosition = 0;
            dialWait = new WaitForSeconds(.2f);
            canContinue = true;
            fade = GetComponent<SceneGroupChangeWithFade>();
        }


        public void OnMultiSceneAwake()
        {
            ShowNextDialogue();
        }


        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
                ShowNextDialogue();
        }


        private void ShowNextDialogue()
        {
            if (!canContinue) return; 
            
            if (HasFinishedFile)
            {
                fade.LoadSceneGroup();
                return;
            }

            if (dialCo == null)
            {
                dialCo = StartCoroutine(DialTextCo());
                StartCoroutine(InputDelay());
                dialPosition++;
            }
            else
            {
                StopCoroutine(dialCo);
                dialCo = null;
                display.UpdateText(dialogue.collection[dialPosition - 1]);
                StartCoroutine(InputDelay());
            }
        }


        private IEnumerator InputDelay()
        {
            canContinue = false;
            yield return dialWait;
            canContinue = true;
        }


        private IEnumerator DialTextCo()
        {
            var _pos = 0;
            var _charArray = dialogue.collection[dialPosition];

            while (_pos < _charArray.Length)
            {
                _pos++;
                display.UpdateText(_charArray.Substring(0, _pos));
                yield return new WaitForSeconds(.025f);
            }

            dialCo = null;
        }
    }
}