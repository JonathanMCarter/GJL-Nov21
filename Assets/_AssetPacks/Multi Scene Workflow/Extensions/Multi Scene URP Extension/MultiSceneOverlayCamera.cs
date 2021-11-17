/*
 * 
 *  Multi-Scene Workflow
 *      URP Camera Extension
 *  
 *	Multi-Scene Overlay Camera
 *      Handles the overlay camera setup to stack with the base camera setup
 *			
 *  Written by:
 *      Jonathan Carter
 *		
 *	Last Updated: 05/11/2021 (d/m/y)
 * 
 */
using System.Linq;
using UnityEngine;
using MultiScene.Core;

namespace MultiScene.Extensions.URP
{
    [ExecuteInEditMode]
    public class MultiSceneOverlayCamera : MonoBehaviour, IMultiSceneAwake
    {
        private Camera cam;
        public string camID;
        private bool setupRun;

        /// <summary>
        /// Gets the camera in the editor...
        /// </summary>
        public Camera GetCamera => GetComponent<Camera>();

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
            SetCamera();
        }

        /// <summary>
        /// Sets the camera to the base camera of the same ID when all scenes have loaded xD
        /// </summary>
        private void SetCamera()
        {
            var _cameras = SceneElly.GetComponentsFromAllScenes<MultiSceneBaseCamera>();

            if (_cameras.Count <= 0) return;
            foreach (var _cam in _cameras.Where(t => t.camID.Equals(camID)))
                _cam.AddCamera(cam);

            setupRun = true;
        }
    }
}