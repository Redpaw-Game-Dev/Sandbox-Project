using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sandbox.ObjectsManagement
{
    public class ObjectProvider<T> : MonoBehaviour where T : Object
    {
        [SerializeField] private string _id;
        [SerializeField] private T _object;

        private ObjectsManager _objectsManager;
        
        [Inject]
        private void Construct(ObjectsManager objectsManager)
        {
            _objectsManager = objectsManager;
            _objectsManager.AddObject(_id, _object);
        }

        private void OnDestroy()
        {
            _objectsManager.RemoveObject(_id);
        }
    }
}