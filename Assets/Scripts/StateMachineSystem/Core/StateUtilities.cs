using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.StateMachineSystem
{
    public static class StateUtilities
    {
        public static IEnumerable<Type> GetStateTypes()
        {
            var stateBaseType = typeof(IState);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => stateBaseType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);
        }
    }
}