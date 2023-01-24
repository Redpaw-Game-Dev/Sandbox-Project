using UnityEngine;

namespace Scripts.Utilities
{
    public class ApplicationTweaks : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;
        [SerializeField, Range(0, 5)] private int _vSyncCount;
        
        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
            QualitySettings.vSyncCount = _vSyncCount;
        }
    }
}