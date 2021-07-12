using AR;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ARContentSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private ARContent _arContent;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ARContent>()
                .FromInstance(_arContent);
        }
    }
}
