using System.Collections.Generic;

namespace Game.Scripts.Gameplay
{
    public class ShiftRegistry
    {
        private readonly List<IShiftable> _shiftables = new();
        
        public IReadOnlyList<IShiftable> Shiftables => _shiftables;

        public void Register(IShiftable shiftable) => _shiftables.Add(shiftable);
        
        public void Unregister(IShiftable shiftable) => _shiftables.Remove(shiftable);
    }
}