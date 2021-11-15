using System;
using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class SpiritOrb : MonoBehaviour
    {
        [SerializeField] private BoolReference isGhostForm;
        [SerializeField] private IntReference playerOrbCount;

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