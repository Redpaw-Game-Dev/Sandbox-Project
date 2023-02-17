using System;

namespace Sandbox.Abstraction
{
    [Serializable]
    public abstract class Task
    {
        public abstract void Do(IInfo info = null);
    }
}