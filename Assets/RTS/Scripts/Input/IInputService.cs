using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace rts.input
{
    public interface IInputService
    {
        IObservable<Vector2> MainInputAction { get; }
        IObservable<Vector2> SecondaryInputAction { get; }

        IObservable<IDragEvent> Drag {get;}

    }
}

