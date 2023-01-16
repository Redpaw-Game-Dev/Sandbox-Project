using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.Loading
{
    [CreateAssetMenu(menuName = "Game/Base Scenes Config")]
    public class BaseScenesConfig : ScriptableObject
    {
        [SerializeField] private AssetReference[] _baseScenes = new AssetReference[0];

        public AssetReference[] BaseScenes => _baseScenes;
    }
}