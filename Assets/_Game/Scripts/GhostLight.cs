using System;
using DependencyLibrary;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.GhostMode
{
    public class GhostLight : MonoBehaviour
    {
        [SerializeField] private BoolReference ghostState;
        private GameObject ghostEffects;
        

        private void Awake()
        {
            if (transform.childCount <= 0) return;
            ghostEffects = transform.GetChild(0).gameObject;
        }
    }
}