using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sandbox.Loading
{
    public class LoadingManager : IInitializable
    {
        private bool _isBaseScenesLoading;
        private bool _isBaseScenesLoaded;
        [Inject] private BaseScenesConfig _baseScenesConfig;
        
        public void Initialize()
        {
            LoadBaseScenes();
        }

        private async void LoadBaseScenes()
        {
            if(_baseScenesConfig == null || _isBaseScenesLoaded || _isBaseScenesLoading) return;
            
            _isBaseScenesLoading = true;
            AssetReference[] baseScenes = _baseScenesConfig.BaseScenes;
            foreach (var scene in baseScenes)
            {
                await scene.LoadSceneAsync(LoadSceneMode.Additive);
            }
            _isBaseScenesLoading = false;
            _isBaseScenesLoaded = true;
        }
    }
}