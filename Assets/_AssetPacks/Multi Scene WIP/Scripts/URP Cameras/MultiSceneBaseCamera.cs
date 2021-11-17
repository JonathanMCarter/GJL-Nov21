// ----------------------------------------------------------------------------
// BaseCamEdit.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 31/08/2021
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JTools.MultiScene.URP
{
    [ExecuteInEditMode]
    public class MultiSceneBaseCamera : MonoBehaviour
    {
        public string camID;
        private Camera cam;

        public Camera GetCamera => cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        /// <summary>
        /// Adds a camera to this base camera stack.
        /// </summary>
        /// <param name="overlayCam">The overlay camera to add to the stack.</param>
        public void AddCamera(Camera overlayCam)
        {
            var _cameraData = cam.GetUniversalAdditionalCameraData();

            if (_cameraData.cameraStack.Contains(overlayCam)) return;
            _cameraData.cameraStack.Add(overlayCam);
        }
    }
}