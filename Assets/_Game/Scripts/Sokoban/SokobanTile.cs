using System;
using System.Collections.Generic;
using JTools;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanTile : MonoBehaviour
    {
        [SerializeField] private List<SokobanTile> neighbours;
        [SerializeField] private SokobanDirectionalData directionalData;
        protected SokobanManager sokobanManager;


        public bool IsOccupied => transform.childCount > 0;
        
        public GameObject OccupyingObject => IsOccupied ? transform.GetChild(0).gameObject : null;
        public SokobanBlock OccupyingBlock { get; private set; }

        
        private void Start()
        {
            sokobanManager = SceneElly.GetComponentFromScene<SokobanManager>("Level2-Sokoban-1");
            neighbours = sokobanManager.GetNeighbours(this);
            GetDirectionalData();
        }


        public virtual void SetBlockToTile(SokobanBlock block)
        {
            if (IsOccupied) return;
            OccupyingBlock = block;
        }


        private void GetDirectionalData()
        {
            directionalData = new SokobanDirectionalData();
            
            foreach (var tile in neighbours)
            {
                var _tileTransform = tile.transform.position;
                var _cachedTransform = transform.position;
                
                if (_tileTransform.x > _cachedTransform.x)
                {
                    directionalData.posX = tile;
                }  
                
                if (_tileTransform.x < _cachedTransform.x)
                {
                    directionalData.negX = tile;
                }  

                if (_tileTransform.z > _cachedTransform.z)
                {
                    directionalData.posZ = tile;
                }  
                
                if (_tileTransform.z < _cachedTransform.z)
                {
                    directionalData.negZ = tile;
                }  
            }
        }
    }
}