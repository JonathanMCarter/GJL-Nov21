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
        private Renderer rend;
        private float currentDistance = 0;
        [SerializeField] private GroundRevealLamp lamp;
  


        private void Awake()
        {
            rend = GetComponent<Renderer>();
            currentDistance = 0;
            GenerateGroundVisuals();
            StartCoroutine(RevealGroundSmoothCo());
        }

        public void AssignLamp(GroundRevealLamp l)
        {
            lamp = l;
        }


        public void ClearLamps()
        {
            lamp = null;
        }


        public void GenerateGroundVisuals()
        {
            // Pass the player location to the shader
            rend.sharedMaterial.SetVector(PlayerPosition, lamp.transform.position);
        }


        private IEnumerator RevealGroundSmoothCo()
        {
            rend.sharedMaterial.SetFloat(VisibleDistance, 0);
            currentDistance = rend.sharedMaterial.GetFloat(VisibleDistance);
            
            while (currentDistance < maxDistanceFromLight)
            {
                yield return null;
                rend.sharedMaterial.SetFloat(VisibleDistance, currentDistance += 15 * Time.deltaTime);
            }
        }


        private IEnumerator HideGroundSmoothCo()
        {
            currentDistance = rend.sharedMaterial.GetFloat(VisibleDistance);
            
            while (currentDistance > 0)
            {
                yield return null;
                rend.sharedMaterial.SetFloat(VisibleDistance, currentDistance -= 15 * Time.deltaTime);
            }
        }
    }
}
