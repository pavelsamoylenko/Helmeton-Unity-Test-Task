using System;
using UnityEngine;
using Zenject;

namespace AR
{
    public class ARContentPresenter : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly ARContentPlacement _contentPlacement;

        public ARContentPresenter(SignalBus signalBus, ARContentPlacement arContentPlacement)
        {
            _signalBus = signalBus;
            _contentPlacement = arContentPlacement;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnARContentLoaded>(SetARContent);
        }

        private void SetARContent(OnARContentLoaded arg)
        {
            var content = arg.Content;
            _contentPlacement.SetARContent(arg.Content);
            
            Debug.Log($"Loaded ar content: {content}", content);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnARContentLoaded>(SetARContent);
        }
    }
}