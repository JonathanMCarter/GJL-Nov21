/*
 * 
 *  Multi-Scene Workflow
 *      Do Not Destroy Extension
 *  
 *	Do Not Destroy Accessor
 *      Used to access the do not destroy on load scene.
 *			
 *  Written by:
 *      Jonathan Carter
 *		
 *	Last Updated: 05/11/2021 (d/m/y)							
 * 
 */

using System.Collections.Generic;
using UnityEngine;

namespace MultiScene.Extensions.DoNotDestroy
{
    /// <summary>
    /// Call to access a spy object and find the object of the type entered...
    /// </summary>
    public static class DoNotDestroyAccessor
    {
        /// <summary>
        /// Gets all the components of the type entered in the Do Not Destroy Persistent Scene...
        /// </summary>
        /// <typeparam name="T">The type to find</typeparam>
        /// <returns>A list of all of that type that can be found</returns>
        public static List<T> GetComponentsInDoNotDestroy<T>()
        {
            FindOrCreateSpy();
            return DoNotDestroySpy.Instance.GetComponentsInDoNotDestroy<T>();
        }
        
        /// <summary>
        /// Gets the first component of the type entered in the Do Not Destroy Persistent Scene...
        /// </summary>
        /// <typeparam name="T">The type to find</typeparam>
        /// <returns>The first found of the type entered</returns>
        public static T GetComponentInDoNotDestroy<T>()
        {
            FindOrCreateSpy();
            return DoNotDestroySpy.Instance.GetComponentInDoNotDestroy<T>();
        }

        /// <summary>
        /// Gets all the objects in the Do Not Destroy scene...
        /// </summary>
        /// <returns>A list of all the found objects</returns>
        public static List<GameObject> GetRootGameObjects()
        {
            return DoNotDestroySpy.Instance.GetRootGameObjects();
        }

        /// <summary>
        /// Checks to see if a spy exist. If not it creates one for you...
        /// </summary>
        private static void FindOrCreateSpy()
        {
            if (DoNotDestroySpy.Instance != null) return;
            var _obj = new GameObject
            {
                name = "Multi-Scene Ext. DoNotDestroySpy"
            };
            
            _obj.AddComponent<DoNotDestroySpy>();
        }
    }
}