using System;
using TMPro;
using UnityEngine;

namespace DeadTired.Dialogue
{
    public class DialogueDisplay : MonoBehaviour
    {
        private TMP_Text dialogueText;

        private void Awake()
        {
            dialogueText = GetComponent<TMP_Text>();
        }

        public void UpdateText(string text)
        {
            dialogueText.text = text;
        }
    }
}