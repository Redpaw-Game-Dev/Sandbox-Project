using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Zenject;

namespace Sandbox.Abstraction
{
    public abstract class InfoProcessor : SerializedMonoBehaviour
    {
        [OdinSerialize] protected Dictionary<IInfo, Task> _infoTasks = new Dictionary<IInfo, Task>();

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            foreach (var infoTaskPair in _infoTasks)
            {
                diContainer.Inject(infoTaskPair.Value);
            }
        }
        
        protected void Process(IInfo info)
        {
            if (info != null && _infoTasks.ContainsKey(info))
            {
                _infoTasks[info].Do(info);
            }
        }
    }
}