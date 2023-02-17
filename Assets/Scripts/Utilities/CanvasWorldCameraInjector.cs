using UnityEngine;
using Zenject;

namespace Sandbox.Utilities
{
    public class CanvasWorldCameraInjector : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [Inject]
        private void Construct([Inject(Id = "UI Camera")] Camera _uiCamera)
        {
            _canvas.worldCamera = _uiCamera;
        }
    }
}