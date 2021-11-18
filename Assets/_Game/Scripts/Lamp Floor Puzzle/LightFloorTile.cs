using System;
using System.Collections;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired
{
    public class LightFloorTile : MonoBehaviour
    {
        private static readonly int PlayerPosition = Shader.PropertyToID("_PlayerPosition");
        private static readonly int VisibleDistance = Shader.PropertyToID("_VisibleDistance");

        [SerializeField] private FloatReference maxDistanceFromLight;
        [SerializeField] private FloatReference transitionSpeed;
        
        private Renderer rend;
        private float currentDistance = 0;
        [SerializeField] private GroundRevealLamp lamp;

        private Coroutine activeCo;
  


        private void Awake()
        {
            rend = GetComponent<Renderer>();
            currentDistance = 0;
        }

        public void AssignLamp(GroundRevealLamp l)
        {
            lamp = l;
            if (!l.IsLampLit) return;
            GenerateGroundVisuals();
            
            if (activeCo != null)
                StopCoroutine(activeCo);
            
            activeCo = StartCoroutine(RevealGroundSmoothCo());
        }


        public void RemoveLamp()
        {
            lamp = null;
            
            if (activeCo != null)
                StopCoroutine(activeCo);
            
            activeCo = StartCoroutine(HideGroundSmoothCo());
        }
        

        public void GenerateGroundVisuals()
        {
            // Pass the player location to the shader
            rend.material.SetVector(PlayerPosition, lamp.transform.position);
        }


        private IEnumerator RevealGroundSmoothCo()
        {
            rend.material.SetFloat(VisibleDistance, 0);
            currentDistance = rend.sharedMaterial.GetFloat(VisibleDistance);
            
            while (currentDistance < maxDistanceFromLight)
            {
                yield return null;
                rend.material.SetFloat(VisibleDistance, currentDistance += transitionSpeed * Time.deltaTime);
            }
        }


        private IEnumerator HideGroundSmoothCo()
        {
            currentDistance = rend.material.GetFloat(VisibleDistance);
            
            while (currentDistance > 0)
            {
                yield return null;
                rend.material.SetFloat(VisibleDistance, currentDistance -= transitionSpeed * Time.deltaTime);
            }
        }


        /// <summary>
        /// Draws the gizmo for the point so it can be seen in the scene easier without needed a mesh renderer
        /// </summary>
        private void OnDrawGizmos()
        {
            var _pos = transform.position;
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_pos, Vector3.one * 5);
        }
    }
}
