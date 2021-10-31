// ----------------------------------------------------------------------------
// OverlayCamEdit.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 31/08/2021
// ----------------------------------------------------------------------------

using System;
using System.Linq;
using JTools;
using UnityEngine;

namespace JTools.MultiScene.URP
{
    [ExecuteInEditMode]
    public class MultiSceneOverlayCamera : MonoBehaviour, IMultiSceneAwake
    {
        private Camera cam;
        public string camID;
        private bool setupRun;

        public Camera GetCameraInEditor => GetComponent<Camera>();

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }
        
        private void Start()
        {
            if (setupRun) return;
            SetCamera();
        }

        public void OnMultiSceneAwake()
        {
            if (setupRun) return;
            SetCamera();
        }

        private void SetCamera()
        {
            if (setupRun) return;
            var _cameras = SceneElly.GetComponentsFromAllScenes<MultiSceneBaseCamera>();

            if (_cameras.Count <= 0) return;
            foreach (var _cam in _cameras.Where(t => t.camID.Equals(camID)))
                _cam.AddCamera(cam);

            setupRun = true;
        }
    }
}