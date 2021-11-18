using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class LightableFloorTile : MonoBehaviour
    {
        public bool testThis;
        public Transform player;
        
        private void Update()
        {
            // var _materails = GetComponent<Renderer>().materials;
            //
            // foreach (var mat in _materails)
            // {
            //     mat.SetVector("_Pos", transform.position);
            // }

            if (testThis)
            {
                var renderer = GetComponent<Renderer>();
                
                // Pass the player location to the shader
                renderer.sharedMaterial.SetVector("_PlayerPosition", player.position);
            }
        }
    }
}
