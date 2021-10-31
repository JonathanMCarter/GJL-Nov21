// ----------------------------------------------------------------------------
// DoNotDestroySpy.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 07/10/2021
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace JTools.MultiScene.DNDOL
{
    public class DoNotDestroySpy : MonoBehaviour
    {
        private static DoNotDestroySpy instance;
        public static DoNotDestroySpy Instance => instance;

        private void Awake()
        {
            if (instance != null) 
                Destroy(gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }


        public List<T> GetComponentsInDoNotDestroy<T>()
        {
            var _scene = gameObject.scene.GetRootGameObjects();
            var _find = new List<T>();
            
            foreach (var _obj in _scene)
                _find.AddRange(_obj.GetComponentsInChildren<T>(true));

            return _find;
        }


        public T GetComponentInDoNotDestroy<T>()
        {
            var _get = GetComponentsInDoNotDestroy<T>();
            return _get.Count <= 0 
                ? default 
                : _get[0];
        }
    }
}