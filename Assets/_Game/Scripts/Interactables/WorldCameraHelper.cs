using System;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.Interactables
{
    [RequireComponent(typeof(Canvas))]
    public class WorldCameraHelper : MonoBehaviour, IMultiSceneAwake
    {
        [SerializeField] private bool shouldMatchRotation;
        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        public void OnMultiSceneAwake()
        {
            canvas.worldCamera = SceneElly.GetComponentFromScene<Camera>("Player");

            if (!shouldMatchRotation) return;
            canvas.transform.rotation = canvas.worldCamera.transform.rotation;
        }
    }
}