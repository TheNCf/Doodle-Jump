using System;
using MVVM;
using UnityEditor;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace SampleGame
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        [SerializeField] private BindingMode viewBinding;

        [SerializeField] private Object view;

        [SerializeField] private MonoScript viewType;

        [SerializeField] private string viewId;

        [Space(8)] [SerializeField] private BindingMode viewModelBinding;

        [SerializeField] private Object viewModel;

        [SerializeField] private MonoScript viewModelType;

        [SerializeField] private string viewModelId;

        private IBinder _binder;

        [Inject] private DiContainer diContainer;

        private void Awake()
        {
            _binder = CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            var view = viewBinding switch
            {
                BindingMode.FromInstance => this.view,
                BindingMode.FromResolve => diContainer.Resolve(viewType.GetClass()),
                BindingMode.FromResolveId => diContainer.ResolveId(viewType.GetClass(), viewId),
                _ => throw new Exception($"Binding type of view {viewBinding} is not found!")
            };

            var model = viewModelBinding switch
            {
                BindingMode.FromInstance => viewModel,
                BindingMode.FromResolve => diContainer.Resolve(viewModelType.GetClass()),
                BindingMode.FromResolveId =>
                    diContainer.ResolveId(viewModelType.GetClass(), viewModelId),
                _ => throw new Exception($"Binding type of view {viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }

        private enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }
    }
}