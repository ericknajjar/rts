using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace rts.input
{
    public interface IInputService
    {
        IObservable<Unit> MainInputAction { get; }
        IObservable<Unit> SecondaryInputAction { get; }

        IObservable<IDragEvent> Drag {get;}

    }
}

