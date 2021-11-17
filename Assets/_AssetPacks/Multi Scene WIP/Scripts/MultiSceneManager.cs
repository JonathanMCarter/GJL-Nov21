// ----------------------------------------------------------------------------
// MultiSceneLoad.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 31/08/2021
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace JTools.MultiScene
{
    public class MultiSceneManager : MonoBehaviour
    {
        public bool loadOnAwake = true;
        public SceneGroup scenesToLoad;
        public UnityEvent beforeSceneLoaded;

        private List<IMultiSceneAwake> awakeCache;
        private List<IMultiSceneEnable> enableCache;
        private List<IMultiSceneStart> startCache;

        
        private void Awake()
        {
            // if there are no scenes to load... just do nothing xD
            if (scenesToLoad == null || !loadOnAwake) return;

            beforeSceneLoaded?.Invoke();
            LoadScenes();
        }
        
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= CallGroupLoadedListeners;
            StopAllCoroutines();
        }
        

        private void CallGroupLoadedListeners(Scene s, LoadSceneMode l)
        {
            if (!s.name.Equals(scenesToLoad.scenes[scenesToLoad.scenes.Count - 1]))
                return;

            awakeCache = SceneElly.GetComponentsFromAllScenes<IMultiSceneAwake>();
            enableCache = SceneElly.GetComponentsFromAllScenes<IMultiSceneEnable>();
            startCache = SceneElly.GetComponentsFromAllScenes<IMultiSceneStart>();
            
            StartCoroutine(CallMultiSceneAwake());
            SceneManager.sceneLoaded -= CallGroupLoadedListeners;
        }


        private IEnumerator CallMultiSceneAwake()
        {
            yield return new WaitForEndOfFrame();
            
            if (awakeCache.Count <= 0)
            {
                yield return CallMultiSceneEnable();
                yield break;
            }
            
            foreach (var _l in awakeCache)
            {
                _l.OnMultiSceneAwake();
            }
            
            yield return CallMultiSceneEnable();
        }
        
        
        private IEnumerator CallMultiSceneEnable()
        {
            yield return new WaitForEndOfFrame();
            
            if (enableCache.Count <= 0)
            {
                yield return CallMultiSceneStart();
                yield break;
            }
            
            foreach (var _l in enableCache)
            {
                _l.OnMultiSceneEnable();
            }
            
            yield return CallMultiSceneStart();
        }
        
        
        private IEnumerator CallMultiSceneStart()
        {
            yield return new WaitForEndOfFrame();
            
            if (startCache.Count <= 0) yield break;
            
            foreach (var _l in startCache)
            {
                _l.OnMultiSceneStart();
            }
        }


        public void UnloadAllActiveScenes()
        {
            Debug.Log($"Count: {SceneManager.sceneCount}");
            var _scenes = new List<string>();

            for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
                _scenes.Add(SceneManager.GetSceneAt(i).name);

            foreach (var _s in _scenes)
            {
                Debug.Log(_s);
                SceneManager.UnloadSceneAsync(_s);
            }
        }


        public void LoadScenes()
        {
            var _scenes = new List<string>();

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                _scenes.Add(SceneManager.GetSceneAt(i).name);
            }

            for (var i = 0; i < scenesToLoad.scenes.Count; i++)
            {
                var _s = scenesToLoad.scenes[i];

                if (i.Equals(scenesToLoad.scenes.Count - 1))
                    SceneManager.sceneLoaded += CallGroupLoadedListeners;
                    
                if (_scenes.Contains(_s)) continue;
                
                if (i.Equals(0))
                    SceneManager.LoadSceneAsync(_s, LoadSceneMode.Single);
                else
                    SceneManager.LoadSceneAsync(_s, LoadSceneMode.Additive);
            }
        }


        public void LoadScenes(SceneGroup groupToLoad, bool unloadCurrent)
        {
            var _scenes = new List<string>();

            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                _scenes.Add(SceneManager.GetSceneAt(i).name);
            }

            UnloadAllActiveScenes();


            for (var i = 0; i < groupToLoad.scenes.Count; i++)
            {
                var _s = groupToLoad.scenes[i];

                if (i.Equals(groupToLoad.scenes.Count - 1))
                    SceneManager.sceneLoaded += CallGroupLoadedListeners;

                if (_scenes.Contains(_s)) continue;

                if (i.Equals(0))
                    SceneManager.LoadSceneAsync(_s, LoadSceneMode.Single);
                else
                    SceneManager.LoadSceneAsync(_s, LoadSceneMode.Additive);
            }
        }
        
        

        public bool IsSceneActive(string s)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name.Equals(s)) return true;
            }

            return false;
        }
    }
}