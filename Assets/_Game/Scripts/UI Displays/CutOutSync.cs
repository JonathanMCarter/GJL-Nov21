using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class CutOutSync : MonoBehaviour
    {

        public static int posID = Shader.PropertyToID("_Positon");
        public static int sizeID = Shader.PropertyToID("_Size");

        public Material wallMaterial;
        public Camera camera;
        public LayerMask mask;

        void Start()
        {
            if(camera == null)
                camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {

            //check if behind or infront of a wall
            var dir = camera.transform.position - transform.position;
            var ray = new Ray(transform.position, dir.normalized);

            if(Physics.Raycast(ray, 3000, mask))
            {
                wallMaterial.SetFloat(sizeID, 1);
            }
            else
            {
                wallMaterial.SetFloat(sizeID, 0);
            }

            var view = camera.WorldToViewportPoint(transform.position);
            wallMaterial.SetVector(posID, view);
        }
    }
}
