namespace Game.Scripts.Gameplay.LevelGeneration
{
    public interface IDisposableObject
    {
        public float SpawnHeight { get; }
        public float DistanceFromCenter { get; }
    }
}