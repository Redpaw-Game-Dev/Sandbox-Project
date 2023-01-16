using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scripts.Loading
{
    public class LoadingManager : IInitializable
    {
        private bool _isBaseScenesLoading;
        private bool _isBaseScenesLoaded;
        private BaseScenesConfig _baseScenesConfig;
        
        private LoadingManager(BaseScenesConfig baseScenesConfig)
        {
            _baseScenesConfig = baseScenesConfig;
        }
        
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