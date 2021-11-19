using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DeadTired.Interactables
{
    public class LeverLine : MonoBehaviour
    {
        [SerializeField] private float waitTime = .1f;
        [SerializeField] private Material onMaterial;
        private MeshRenderer[] lineObjects;
        private WaitForSeconds wait;

        public UnityEvent OnLineComplete;

        
        private void Awake()
        {
            lineObjects = new MeshRenderer[transform.childCount];
            wait = new WaitForSeconds(waitTime);

            for (var i = 0; i < transform.childCount; i++)
                lineObjects[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        }


        public void EnableLine()
        {
            
            StartCoroutine(TurnOnLineCo());
            
        }


        private IEnumerator TurnOnLineCo()
        {
            AkSoundEngine.PostEvent("LightSwitch", gameObject);
            var _pos = 0;

            while (_pos < lineObjects.Length)
            {
                lineObjects[_pos].material = onMaterial;
               if(lineObjects[_pos].material == onMaterial){
                    
               }
                _pos++;
                yield return wait;
                
            }
            
            OnLineComplete?.Invoke();
            
        }
    }
}