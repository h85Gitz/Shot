using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using static UI.EffectType;

namespace UI
{
    public enum EffectType
    {
        Icon,
        Message
    }
    public delegate void UIEffectDelegates();
    public class UIEffect : MonoSingleton<UIEffect>
    {
        //todo ÊÂ¼þ
        public static event UIEffectDelegates OnUIEffected;
    

        private const float Time = 2f;
        private float _timeElapsed;
        private float _value;

        
        private RectTransform _rectIcon;
        private RectTransform _rectDual;
   


        public void Move(EffectType effectType, RectTransform rectTransform, RectTransform rectSwitchWeapon,Slider hp, Slider mp)
        {
            var sequence = DOTween.Sequence();
            switch (effectType)
            {
                case Icon:
                    sequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - 200f, 0.5f, true));
                    sequence.Append(hp.DOValue(1, 0.8f)).AppendInterval(0.2f);
                    sequence.Append(mp.DOValue(1, 0.5f)).AppendInterval(0.1f);
                    break;
                case Message:
                    sequence.Append(rectSwitchWeapon.DOAnchorPosX(rectSwitchWeapon.anchoredPosition3D.x + 250f, 0.5f,
                        true));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
            }
        }

        public void OnDualMOve()
        {
            OnUIEffected?.Invoke();
            DualMove();
        }

        private void DualMove()
        {
            Debug.Log("DualMove is called");
            var sequence = DOTween.Sequence();
            //sequence.Append(_rectDual.DOAnchorPosX(_rectSwitchWeapon.anchoredPosition3D.x + 50f, 0.5f, true));
        }
        public void ArrowMove()
        {

        }

    }
}
