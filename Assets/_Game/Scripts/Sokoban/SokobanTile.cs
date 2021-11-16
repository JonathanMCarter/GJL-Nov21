using System;
using System.Collections.Generic;
using JTools;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanTile : MonoBehaviour
    {
        [SerializeField] private List<SokobanTile> neighbours;
        protected SokobanManager sokobanManager;
        
        public bool IsOccupied => transform.childCount > 0;
        
        public GameObject OccupyingObject => IsOccupied ? transform.GetChild(0).gameObject : null;
        public SokobanBlock OccupyingBlock { get; private set; }

        
        private void Start()
        {
            sokobanManager = SceneElly.GetComponentFromScene<SokobanManager>("Level2-Sokoban-1");
            neighbours = sokobanManager.GetNeighbours(this);
        }


        public virtual void SetBlockToTile(SokobanBlock block)
        {
            if (IsOccupied) return;
            OccupyingBlock = block;
        }
    }
}