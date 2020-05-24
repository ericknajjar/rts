using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace rts.input
{
    public class SelectionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }
    }

}
