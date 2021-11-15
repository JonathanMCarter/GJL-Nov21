using System.Collections.Generic;
using JTools;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class OrbPool : MonoBehaviour
    {
        [SerializeField] private GameObject orbPrefab;
        private List<GameObject> orbs;

        
        private void Awake()
        {
            PoolHelper.SetupObjectPool(orbPrefab, transform, 10, false, out var _pool);
            orbs = _pool;
        }
    }
}