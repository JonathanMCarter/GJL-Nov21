using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeadTired.UI
{
    public class CreditsPanelUI : BasePanel
    {
        [SerializeField] private ScrollRect scrollRect;


        protected override void Awake()
        {
            base.Awake();
            
            if (scrollRect == null)
                scrollRect = GetComponentInChildren<ScrollRect>();

            if (!scrollRect.enabled) return;
            scrollRect.enabled = false;
        }


        public override void OpenPanelLogic()
        {
            base.OpenPanelLogic();
            if (scrollRect == null) return;
            scrollRect.enabled = true;
        }


        public override void ClosePanelLogic()
        {
            base.ClosePanelLogic();
            if (scrollRect == null) return;
            scrollRect.enabled = false;
        }
    }
}