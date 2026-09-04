using System;
using MVVM;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI.Binders
{
    public class TextBinder : IBinder, IObserver<string>
    {
        private readonly TMP_Text _view;
        private readonly IReadOnlyReactiveProperty<string> _property;
        private IDisposable _handle;

        public TextBinder(TMP_Text view, IReadOnlyReactiveProperty<string> property)
        {
            _view = view;
            _property = property;
            Debug.Log("Binder created");
        }
        
        public void Bind()
        {
            OnNext(_property.Value);
            _handle = _property.Subscribe(this);
            Debug.Log("Binded");
            Debug.Log(_property.Value);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(string value)
        {
            _view.text = value;
            Debug.Log("Changed");
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }
    }
}