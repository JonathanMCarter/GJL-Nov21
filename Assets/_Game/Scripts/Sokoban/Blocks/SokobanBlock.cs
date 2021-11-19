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

        private SokobanTile defaultTile;
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
        
        
        protected override void OnPlayerEnterTriggerZone(Collider other)
        {
            base.OnPlayerEnterTriggerZone(other);
            ConfigureUI(true);
        }

        protected override void OnPlayerExitTriggerZone(Collider other)
        {
            base.OnPlayerExitTriggerZone(other);
            ConfigureUI(false);
        }

        public virtual void OnPlayerInteract()
        {
            if (!interactionsManager.HasInteraction(GetInteractable()) || !IsPlayerInZone) return;
            
            // Should fix the block from being moved before the tween has finished xD
            if (!triggerVolume.enabled) return;
            
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
            ConfigureUI(false);
            AkSoundEngine.PostEvent("Move_Box", gameObject);
            
        }


        public void OnBlockMoveComplete()
        {
            
            triggerVolume.enabled = true;
            base.OnPlayerExitTriggerZone(triggerVolume);
        }
        

        private SokobanTile EvaluateDirection()
        {
            if (tileOn == null)
                tileOn = sokobanManager.GetTileOn(BlockID);

            if (defaultTile == null)
                defaultTile = tileOn;

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


        public void ResetToDefaultTile()
        {
            if (defaultTile == null) return;
            defaultTile.ForceSetBlockToTile(this);
            tileOn = defaultTile;
            transform.localPosition = GetVector.Vector3DifferentY(Vector3.zero, transform.localPosition.y);
        }
    }
}