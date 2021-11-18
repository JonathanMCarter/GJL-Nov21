using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class CutoutObject : MonoBehaviour
    {
        [SerializeField] private Transform targetObject;
        [SerializeField] private LayerMask wallMask;

        private Camera camera;
        
        private static readonly int CutoutPos = Shader.PropertyToID("_CutoutPos");
        private static readonly int CutoutSize = Shader.PropertyToID("_CutoutSize");
        private static readonly int FalloutSize = Shader.PropertyToID("_FalloutSize");

        private void Awake()
        {
            camera = GetComponent<Camera>();
            CalcOnce();
        }

        private void Update()
        {
            var cutoutPos = camera.WorldToViewportPoint(targetObject.position);
            cutoutPos.y /= (Screen.width / Screen.height);

            var _offset = targetObject.position - transform.position;
            var _hit = Physics.RaycastAll(transform.position, _offset, _offset.magnitude, wallMask);

            foreach (var hit in _hit)
            {
                var _materials = hit.transform.GetComponent<Renderer>().materials;

                foreach (var mat in _materials)
                {
                    mat.SetVector(CutoutPos, cutoutPos);
                    mat.SetFloat(CutoutSize, .25f);
                    mat.SetFloat(FalloutSize, .1f);
                }
            }
        }


        private void CalcOnce()
        {
            var cutoutPos = camera.WorldToViewportPoint(targetObject.position);
            cutoutPos.y /= (Screen.width / Screen.height);

            var _offset = targetObject.position - transform.position;
            var _hit = Physics.RaycastAll(transform.position, _offset, _offset.magnitude, wallMask);

            foreach (var hit in _hit)
            {
                var _materials = hit.transform.GetComponent<Renderer>().materials;

                foreach (var mat in _materials)
                {
                    mat.SetVector(CutoutPos, cutoutPos);
                    mat.SetFloat(CutoutSize, .25f);
                    mat.SetFloat(FalloutSize, .1f);
                }
            }
        }
    }
}
