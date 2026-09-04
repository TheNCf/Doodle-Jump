using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private ObjectShifter _shifter;

        private float _scoreMultiplier = 20.0f;
        
        public event Action<int> ScoreChanged;

        public ScoreCounter(ObjectShifter shifter)
        {
            _shifter = shifter;
        }
        
        public int Score { get; private set; } = 0;

        public void Initialize()
        {
            _shifter.TotalHeightChanged += OnHeightChanged;
        }

        public void Dispose()
        {
            _shifter.TotalHeightChanged -= OnHeightChanged;
        }

        private void OnHeightChanged(float height)
        {
            Score = (int)(height * _scoreMultiplier);
            ScoreChanged?.Invoke(Score);
        }
    }
}