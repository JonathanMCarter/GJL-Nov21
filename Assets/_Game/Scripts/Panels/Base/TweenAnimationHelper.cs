using System.Collections;
using UnityEngine;

namespace DeadTired.UI
{
    public class TweenAnimationHelper
    {
        private static readonly Hashtable ScaleFromXHash = new Hashtable
        {
            { "x", 0 },
            { "time", 1.25f },
        };

        private static readonly Hashtable ScaleFromYHash = new Hashtable
        {
            { "y", 0 },
            { "time", 1.25f },
        };
        
        private static readonly Hashtable ScaleInPanelHash = new Hashtable
        {
            { "scale", Vector3.zero },
            { "time", .3f },
            { "easetype", iTween.EaseType.easeOutBack },
            { "oncomplete", "OpenPanelLogic" },
            { "oncompletetarget", null }
        };
        
        private static readonly Hashtable ScaleOutPanelHash = new Hashtable
        {
            { "scale", Vector3.zero },
            { "time", .3f },
            { "easetype", iTween.EaseType.easeInBack },
            { "oncomplete", "ClosePanelLogic" },
            { "oncompletetarget", null }
        };


        public static void TweenFromX(GameObject target)
        {
            iTween.ScaleFrom(target, ScaleFromXHash);
        }
        
        public static void TweenFromY(GameObject target)
        {
            iTween.ScaleFrom(target, ScaleFromYHash);
        }

        public static void TweenPanelOpen(GameObject target, GameObject onCompleteTarget)
        {
            if (target.transform.localScale.x <= 0)
                target.transform.localScale = Vector3.one;
                
            ScaleInPanelHash["oncompletetarget"] = onCompleteTarget;
            iTween.ScaleFrom(target, ScaleInPanelHash);
        }
        
        public static void TweenPanelClose(GameObject target, GameObject onCompleteTarget)
        {
            ScaleOutPanelHash["oncompletetarget"] = onCompleteTarget;
            iTween.ScaleTo(target, ScaleOutPanelHash);
        }
        
        public static void RunTweenAnimation(TweenAnimationData data)
        {
            switch (data.type)
            {
                case TweenAnimationType.X:
                    TweenAnimationHelper.TweenFromX(data.target);
                    break;
                case TweenAnimationType.Y:
                    TweenAnimationHelper.TweenFromY(data.target);
                    break;
                default:
                    break;
            }
        }
    }
}