using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.StateMachineSystem
{
    public static class StateUtilities
    {
        public static IEnumerable<Type> GetStateTypes()
        {
            var type = typeof(IState);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && type != p);
        }
    }
}