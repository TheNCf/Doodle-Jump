using Game.Scripts.Core.Ads;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core
{
    [CreateAssetMenu(fileName = "ProjectInstaller", menuName = "Installers/ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<AdService>()
                .AsSingle()
                .NonLazy();
        }
    }
}