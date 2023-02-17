using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sandbox.Abstraction
{
    public class DisableAfterTimerTask : Task
    {
        [SerializeField] private GameObject _objectToDisable;
        [SerializeField] private float _timer = 5f; 
        
        public override async void Do(IInfo info = null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_timer));
            _objectToDisable.SetActive(false);
        }
    }
}