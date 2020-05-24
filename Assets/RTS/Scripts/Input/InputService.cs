using UnityEngine;
using System.Collections;
using System;
using UniRx;

namespace rts.input
{

    public class InputService : IInputService
    {

        public InputService()
        {
            MainInputAction = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).AsUnitObservable().Share();

            var mainInputUp = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonUp(0)).AsUnitObservable().Share();

            Drag = MainInputAction.Select(_ => 
            {
                var end = Observable.EveryUpdate()
                .Select(update => (Vector2) Input.mousePosition).StartWith(Input.mousePosition)
                .Distinct().TakeUntil(mainInputUp);

                return new DragEvent(Input.mousePosition, end, mainInputUp.Take(1));
            });
        }



        public IObservable<Unit> MainInputAction { get; }

        public IObservable<Unit> SecondaryInputAction => throw new NotImplementedException();

        public IObservable<IDragEvent> Drag { get; }
      
    }
}

