using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sandbox.ObjectsManagement
{
    public class ObjectsManager : IInitializable
    {
        [SerializeField] 
        private Dictionary<string, Object> _objects = new Dictionary<string, Object>();
        [SerializeField]
        private Dictionary<string, List<Action<Object>>> _callbacks = new Dictionary<string, List<Action<Object>>>();

        public void Initialize()
        {
            
        }
        
        public void AddObject(string id, Object obj)
        {
            if (_objects.ContainsKey(id))
            {
                Debug.LogError($"The object \"{obj.name}\" has not been added. The id \"{id}\" is already occupied.");
            }
            else
            {
                _objects.Add(id, obj);
                if (_callbacks.ContainsKey(id))
                {
                    foreach (var callback in _callbacks[id])
                    {
                        callback.Invoke(obj);
                    }
                }
            }
        }
        
        public void RemoveObject(string id)
        {
            if (!_objects.ContainsKey(id))
            {
                Debug.LogError($"The object with id \"{id}\" has not been added");
            }
            else
            {
                _objects.Remove(id);
            }
        }
        
        public T GetObject<T>(string id, Action<Object> callback = null) where T : Object
        {
            if(!_callbacks.ContainsKey(id)) _callbacks.Add(id, new List<Action<Object>>());
            _callbacks[id].Add(callback);
            if (_objects.ContainsKey(id))
            {
                return (T)_objects[id];
            }
            return null;
        }
    }
}