using UnityEngine;

namespace Scripts.Abstraction
{
    public class DebugLogTask : Task
    {
        [SerializeField] private string _message;
        
        public override void Do(IInfo info = null)
        {
            Debug.Log(_message);
        }
    }
}