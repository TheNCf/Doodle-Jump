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
        private ScoreCounter _scoreCounter;
        
        public ScoreViewModel(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }
        
        [Data("Currency")] public readonly ReactiveProperty<string> Score = new();

        public void Initialize()
        {
            OnScoreChanged(_scoreCounter.Score);
            _scoreCounter.ScoreChanged += OnScoreChanged;
        }

        public void Dispose()
        {
            _scoreCounter.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            Score.Value = SpriteFontConverter.Parse(score.ToString());
        }
    }
}