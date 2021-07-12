using UnityEngine;
using Zenject;

namespace AR
{
    public class ARContent : MonoBehaviour, IInitializable
    {
        [Inject] private SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.Fire(new OnARContentLoaded {Content = this});
            gameObject.SetActive(false);
        }

        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}