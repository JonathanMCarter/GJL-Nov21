/*
 * 
 *  Multi-Scene Workflow
 *      URP Camera Extension
 *  
 *	Multi-Scene Base Camera
 *      Handles the base camera setup
 *			
 *  Written by:
 *      Jonathan Carter
 *		
 *	Last Updated: 05/11/2021 (d/m/y)
 * 
 */


using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MultiScene.Extensions.URP
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
            _cameraData.cameraStack.Add(overlayCam);
        }
    }
}