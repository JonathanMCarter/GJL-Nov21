using System;
using System.Collections.Generic;
using JTools;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanTile : MonoBehaviour
    {
        [SerializeField] private List<SokobanTile> neighbours;

        public bool IsOccupied => transform.childCount > 0;
        
        public GameObject OccupyingObject => IsOccupied ? transform.GetChild(0).gameObject : null;
        public SokobanBlock OccupyingBlock { get; private set; }


        private void Awake()
        {
            GetNeighbours();
        }


        private void GetNeighbours()
        {
            neighbours = new List<SokobanTile>();

            if (Physics.Raycast(transform.position, GetVector.Vector3DifferentZ(transform.position, transform.position.z + 5), out RaycastHit hit))
            {
                if (!TryGet.ComponentInChildren(hit.collider.gameObject, out SokobanTile _tile)) return;
                neighbours.Add(_tile);
            }
        }


        public virtual void SetBlockToTile(SokobanBlock block)
        {
            if (IsOccupied) return;
            OccupyingBlock = block;
        }
    }
}