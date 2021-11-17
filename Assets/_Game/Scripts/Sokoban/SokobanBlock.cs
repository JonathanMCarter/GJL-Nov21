using System;
using DeadTired.Interactables;
using DependencyLibrary;
using JTools;
using UnityEngine;
using IInteractable = DeadTired.Interactables.IInteractable;
using SceneElly = MultiScene.Core.SceneElly;

namespace DeadTired.Sokoban
{
    public class SokobanBlock : BaseInteraction, IInteractable
    {
        [SerializeField] private Vector3Reference playerDirection;
        [SerializeField] private string blockID;

        private SokobanManager sokobanManager;
        private SokobanTile tileOn;


        protected override void Awake()
        {
            base.Awake();
            sokobanManager = SceneElly.GetComponentFromScene<SokobanManager>("Level2-Sokoban-1");
        }

        public string BlockID => blockID;


        protected override IInteractable GetInteractable() => this;

        public void OnPlayerInteract()
        {
            var _newTile = EvaluateDirection();
            _newTile.SetBlockToTile(this);
            
            Debug.LogError(_newTile);
            
            if (_newTile == null) return;
            var _tween = iTween.Hash
            (
                "position", GetVector.Vector3DifferentY(Vector3.zero, transform.localPosition.y), 
                "tile", 2.5f,
                "islocal", true
            );
            
            iTween.MoveTo(gameObject, _tween);
            tileOn = sokobanManager.GetTileOn(BlockID);
        }


        private SokobanTile EvaluateDirection()
        {
            if (tileOn == null)
                tileOn = sokobanManager.GetTileOn(BlockID);

            // Direction A - 
            if (playerDirection.Value.x > .75f && playerDirection.Value.z > .75f)
                return tileOn.DirectionalData.posX;
            
            // Direction B - 
            if (playerDirection.Value.x < -.75f && playerDirection.Value.z > .75f)
                return tileOn.DirectionalData.posZ;
            
            // Direction C - 
            if (playerDirection.Value.x > .75f && playerDirection.Value.z < -.75f)
                return tileOn.DirectionalData.negZ;
            
            // Direction D - 
            if (playerDirection.Value.x < -.75f && playerDirection.Value.z < -.75f)
                return tileOn.DirectionalData.negX;

            return null;
        }
    }
}