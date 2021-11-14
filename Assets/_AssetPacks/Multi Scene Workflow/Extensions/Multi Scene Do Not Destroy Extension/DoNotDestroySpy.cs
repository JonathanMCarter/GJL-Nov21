/*
 * 
 *  Multi-Scene Workflow
 *      Do Not Destroy Extension
 *  
 *	Do Not Destroy Spy
 *      Created in the game to allow access to the do not destroy scene.
 *			
 *  Written by:
 *      Jonathan Carter
 *		
 *	Last Updated: 05/11/2021 (d/m/y)							
 * 
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MultiScene.Extensions.DoNotDestroy
{
    /// <summary>
    /// Used to get types in the 
    /// </summary>
    public class DoNotDestroySpy : MonoBehaviour
    {
        private static DoNotDestroySpy instance;
        
        /// <summary>
        /// Gets the instance of the do not destroy spy...
        /// </summary>
        public static DoNotDestroySpy Instance => instance;
        
        private void Awake()
        {
            if (instance != null) Destroy(this.gameObject);
            SetupSpy();
        }

        /// <summary>
        /// Sets up the spy object as the instance...
        /// </summary>
        private void SetupSpy()
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Gets all the objects of the type entered within the do not destroy scene only...
        /// </summary>
        /// <typeparam name="T">The type to find</typeparam>
        /// <returns>A list of all the found objects of the type in the do not destroy scene</returns>
        public List<T> GetComponentsInDoNotDestroy<T>()
        {
            var _scene = gameObject.scene.GetRootGameObjects();
            var _find = new List<T>();
            
            foreach (var _obj in _scene)
                _find.AddRange(_obj.GetComponentsInChildren<T>());

            return _find;
        }

        /// <summary>
        /// Gets the first object of the type entered within the do not destroy scene only...
        /// </summary>
        /// <typeparam name="T">The type to find</typeparam>
        /// <returns>The first found object of the type in the do not destroy scene</returns>
        public T GetComponentInDoNotDestroy<T>()
        {
            var _get = GetComponentsInDoNotDestroy<T>();
            return _get.Count <= 0 
                ? default 
                : _get[0];
        }
        
        /// <summary>
        /// Gets all the root gameObjects in the scene for use...
        /// </summary>
        /// <returns>A list of all the valid root gameObjects the spy can find.</returns>
        public List<GameObject> GetRootGameObjects()
        {
            return gameObject.scene.GetRootGameObjects().ToList();
        }
    }
}