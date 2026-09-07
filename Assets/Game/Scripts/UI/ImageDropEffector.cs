using System;
using DG.Tweening;
using Game.Scripts.UI.Views;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class ImageDropEffector
    {
        public event Action EffectFinished;
        
        public void Activate(ImageDropView view)
        {
            view.RectTransform.localScale = Vector3.one * view.Data.StartSize;
            
            Observable
                .Timer(TimeSpan.FromSeconds(view.Data.Delay))
                .Subscribe(_ => StartEffect(view))
                .AddTo(view);
        }

        private void StartEffect(ImageDropView view)
        {
            ImageDropData data = view.Data;
            DOTween.Sequence()
                .Append(view.RectTransform.DOScale(data.MinSize, data.ShrinkDuration).SetEase(Ease.Linear))
                .Append(view.RectTransform.DOScale(data.EndSize, data.InflateDuration).SetEase(Ease.Linear))
                .OnComplete(() => EffectFinished?.Invoke())
                .SetLink(view.gameObject)
                .Play();
        }
    }
}