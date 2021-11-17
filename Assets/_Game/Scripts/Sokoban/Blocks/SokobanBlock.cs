using System;
using System.Linq;
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
        private BoxCollider triggerVolume;


        protected override void Awake()
        {
            base.Awake();
            sokobanManager = SceneElly.GetComponentFromScene<SokobanManager>("Level2-Sokoban-1");
            triggerVolume = GetComponentsInChildren<BoxCollider>().FirstOrDefault(t => t.isTrigger);
        }

        public string BlockID => blockID;


        protected override IInteractable GetInteractable() => this;

        public virtual void OnPlayerInteract()
        {
            var _newTile = EvaluateDirection();

            if (_newTile == null) return;
            _newTile.SetBlockToTile(this);
            triggerVolume.enabled = false;
            
            var _tween = iTween.Hash
            (
                "name", blockID,
                "position", GetVector.Vector3DifferentY(Vector3.zero, transform.localPosition.y), 
                "tile", 2f,
                "islocal", true,
                "oncomplete", "OnBlockMoveComplete"
            );
            
            iTween.MoveTo(gameObject, _tween);
            tileOn = sokobanManager.GetTileOn(BlockID);
        }


        public void OnBlockMoveComplete()
        {
            triggerVolume.enabled = true;
        }
        

        private SokobanTile EvaluateDirection()
        {
            if (tileOn == null)
                tileOn = sokobanManager.GetTileOn(BlockID);

            // Direction A - 
            if (playerDirection.Value.x > .1f && playerDirection.Value.z > .1f)
                return tileOn.DirectionalData.posX;
            
            // Direction B - 
            if (playerDirection.Value.x < -.1f && playerDirection.Value.z > .1f)
                return tileOn.DirectionalData.posZ;
            
            // Direction C - 
            if (playerDirection.Value.x > .1f && playerDirection.Value.z < -.1f)
                return tileOn.DirectionalData.negZ;
            
            // Direction D - 
            if (playerDirection.Value.x < -.1f && playerDirection.Value.z < -.1f)
                return tileOn.DirectionalData.negX;

            return null;
        }
    }
}