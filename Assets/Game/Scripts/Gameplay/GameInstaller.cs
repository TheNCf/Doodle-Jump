using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private PlayerCharacterView _playerCharacterViewPrefab;
        [SerializeField] private Transform _startPoint;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<CameraView>()
                .FromInstance(_cameraView)
                .AsSingle();

            PlayerCharacterView playerCharacterView = Container
                .InstantiatePrefabForComponent<PlayerCharacterView>(
                    _playerCharacterViewPrefab, 
                    _startPoint.position,
                    Quaternion.identity, 
                    null);
            
            Container
                .Bind<PlayerCharacterView>()
                .FromInstance(playerCharacterView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerCharacterMover>()
                .AsSingle()
                .NonLazy();
        }
    }
}