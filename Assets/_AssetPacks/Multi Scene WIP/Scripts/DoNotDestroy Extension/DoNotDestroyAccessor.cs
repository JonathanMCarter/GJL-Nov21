// ----------------------------------------------------------------------------
// DoNotDestoryAccessor.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 07/10/2021
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace JTools.MultiScene.DNDOL
{
    public class DoNotDestroyAccessor : MonoBehaviour
    {
        public static List<T> GetComponentsInDoNotDestroy<T>()
        {
            return DoNotDestroySpy.Instance.GetComponentsInDoNotDestroy<T>();
        }
        
        public static T GetComponentInDoNotDestroy<T>()
        {
            return DoNotDestroySpy.Instance.GetComponentInDoNotDestroy<T>();
        }
    }
}