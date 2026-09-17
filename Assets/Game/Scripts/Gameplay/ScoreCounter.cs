using System;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private readonly float _scoreMultiplier = 50.0f;
        private readonly ObjectShifter _shifter;

        public ScoreCounter(ObjectShifter shifter)
        {
            _shifter = shifter;
        }

        public int Score { get; private set; }

        public void Dispose()
        {
            _shifter.TotalHeightChanged -= OnHeightChanged;
        }

        public void Initialize()
        {
            _shifter.TotalHeightChanged += OnHeightChanged;
        }

        public event Action<int> ScoreChanged;

        private void OnHeightChanged(float height)
        {
            Score = (int)(height * _scoreMultiplier);
            ScoreChanged?.Invoke(Score);
        }
    }
}