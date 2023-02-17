﻿using UnityEngine;
using Zenject;

namespace Sandbox.ObjectsManagement
{
    public class ObjectsManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectsManager>().AsSingle();
        }
    }
}