using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DeadTired.Interactables;
using DependencyLibrary;
using JTools;
using UnityEngine;

namespace DeadTired
{
    public class GroundRevealLamp : Lantern
    {
        [SerializeField] private FloatReference lampRadius;
        [SerializeField] private FloatReference lampRadiusSpeed;
        private SphereCollider trigger;
        [SerializeField] private List<LightFloorTile> validTiles;


        protected override void Awake()
        {
            base.Awake();
            trigger = GetComponentsInChildren<SphereCollider>().FirstOrDefault(t => t.isTrigger);
            validTiles = new List<LightFloorTile>();
        }


        protected override void OnOtherEnterTriggerZone(Collider other)
        {
            if (!TryGet.ComponentInChildren(other.gameObject, out LightFloorTile tile)) return;
            if (validTiles.Contains(tile)) return;
            validTiles.Add(tile);
            tile.AssignLamp(this);
        }


        protected override void OnOtherExitTriggerZone(Collider other)
        {
            if (!TryGet.ComponentInChildren(other.gameObject, out LightFloorTile tile)) return;
            if (!validTiles.Contains(tile)) return;
            validTiles.Remove(tile);
            tile.RemoveLamp();
        }

        public void OnLampLitGround()
        {
            trigger.radius = lampRadius;
            
            foreach (var tile in validTiles)
                tile.AssignLamp(this);
        }
        
        public void OnLampExtinguishedGround()
        {
            if (trigger != null)
                trigger.radius = 2f;
            
            if (validTiles == null) return;
            if (validTiles.Count <= 0) return;
            foreach (var tile in validTiles)
                tile.RemoveLamp();
        }
    }
}