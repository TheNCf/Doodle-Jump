using MVVM;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Binders
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
        }
    }
}