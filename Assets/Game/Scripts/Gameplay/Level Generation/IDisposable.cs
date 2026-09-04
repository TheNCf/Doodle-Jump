namespace Game.Scripts.Gameplay.LevelGeneration
{
    public interface IDisposable
    {
        public float SpawnHeight { get; }
        public float DistanceFromCenter { get; }
    }
}