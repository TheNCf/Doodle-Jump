using System;
using MVVM;
using TMPro;
using UniRx;

namespace Game.Scripts.UI.Binders
{
    public class TextBinder : IBinder, IObserver<string>
    {
        private readonly TextMeshProUGUI _view;
        private readonly IReadOnlyReactiveProperty<string> _property;
        private IDisposable _handle;

        public TextBinder(TextMeshProUGUI view, IReadOnlyReactiveProperty<string> property)
        {
            _view = view;
            _property = property;
        }
        
        public void Bind()
        {
            OnNext(_property.Value);
            _handle = _property.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(string value)
        {
            _view.text = value;
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }
    }
}