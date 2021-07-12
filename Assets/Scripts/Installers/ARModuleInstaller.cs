using AR;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ARModuleInstaller : MonoInstaller
    {
        [SerializeField] private ARContentSceneID _arContent;
        [SerializeField] private ARContentPlacement _contentPlacement;
        public override void InstallBindings()
        {
            Container.BindInstance(_arContent);
            Container.BindInstance(_contentPlacement);

            Container.BindInterfacesTo<ARContentLoader>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<ARContentPresenter>()
                .AsSingle()
                .NonLazy();
            
        }
    }
}
