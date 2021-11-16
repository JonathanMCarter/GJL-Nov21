using System;
using System.Collections;
using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class SpiritOrb : MonoBehaviour
    {
        [SerializeField] private BoolReference isGhostForm;
        [SerializeField] private IntReference playerOrbCount;

        private Hashtable spiritOrbIdleTween;


        private void Awake()
        {
            spiritOrbIdleTween = new Hashtable
            {
                { "y", transform.localPosition.y + .5f },
                { "time", 2f },
                { "looptype", iTween.LoopType.pingPong },
                { "islocal", true }
            };
            
            iTween.MoveTo(gameObject, spiritOrbIdleTween);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isGhostForm.Value) return;
            if (!other.CompareTag("Player")) return;
            playerOrbCount.variable.IncrementValue(1);
            PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
            gameObject.SetActive(false);
        }
    }
}