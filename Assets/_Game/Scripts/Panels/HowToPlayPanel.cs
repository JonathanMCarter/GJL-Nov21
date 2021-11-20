using System;
using UnityEngine;

namespace DeadTired.UI
{
    public class HowToPlayPanel : BasePanel
    {
        private void Update()
        {
            if (!Input.GetButtonDown("Back")) return;
            ClosePanel();
        }
    }
}