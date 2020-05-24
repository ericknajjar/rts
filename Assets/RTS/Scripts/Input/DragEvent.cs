using System;
using UniRx;
using UnityEngine;

namespace rts.input
{
    public class DragEvent : IDragEvent
    {
 
        public Vector2 Begin { get; }

        public IReadOnlyReactiveProperty<Vector2> End { get; }

        public DragEvent(Vector2 begin, IReadOnlyReactiveProperty<Vector2> end, IObservable<Unit> done)
        {
            Begin = begin;
            End = end;
            Done = done;
        }

        public IObservable<Unit> Done { get;  }


    }

    public interface IDragEvent
    {
        Vector2 Begin { get; }
        IReadOnlyReactiveProperty<Vector2> End { get; }
        IObservable<Unit> Done { get; }
    }
}

