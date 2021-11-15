using System;
using DependencyLibrary;
using JTools;
using UnityEngine;

namespace DeadTired
{
    public class PlayerStateGetter : MonoBehaviour
    {
        [SerializeField] private BoolReference isPlayerGhost;
        private PlayerBehaviour playerBehaviour;

        private void Awake()
        {
            playerBehaviour = GetComponentInChildren<PlayerBehaviour>();
        }

        private void Update()
        {
            if (!FrameLimiter.LimitEveryXFrames(10)) return;
            isPlayerGhost.SetValue(playerBehaviour.currentState == PlayerBehaviour.State.ghost);
        }
    }
}