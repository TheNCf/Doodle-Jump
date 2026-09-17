using System;
using Game.Scripts.Core;
using Game.Scripts.Gameplay;
using MVVM;
using UniRx;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        [Data("Score")] public readonly ReactiveProperty<string> Score = new();
        private readonly ScoreCounter _scoreCounter;

        public ScoreViewModel(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public void Dispose()
        {
            _scoreCounter.ScoreChanged -= OnScoreChanged;
        }

        public void Initialize()
        {
            OnScoreChanged(_scoreCounter.Score);
            _scoreCounter.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            Score.Value = SpriteFontConverter.Parse(score.ToString());
        }
    }
}