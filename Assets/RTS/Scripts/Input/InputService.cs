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
            var mainInputUp = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonUp(0)).AsUnitObservable().Share();

            var mainInputDown = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).AsUnitObservable().Share();

            var drag = mainInputDown.Select(_ =>
            {
                var end = Observable.EveryUpdate()
                .Select(update => (Vector2)Input.mousePosition).TakeUntil(mainInputUp)
               .ToReactiveProperty(Input.mousePosition);

                return new DragEvent(Input.mousePosition, end, mainInputUp.Take(1));
            }).Share();


            Drag = drag.SelectMany(dragEvent => 
            {
                return dragEvent.End.Where( pos => pos != dragEvent.Begin).Take(1).Select(_ => dragEvent);
            });

            MainInputAction = drag
                .SelectMany(dragEvent => dragEvent.Done.Select(_ => dragEvent))
                .Where(dragEvent => dragEvent.Begin == dragEvent.End.Value).Select(_ => _.Begin);
        }



        public IObservable<Vector2> MainInputAction { get; }

        public IObservable<Vector2> SecondaryInputAction => throw new NotImplementedException();

        public IObservable<IDragEvent> Drag { get; }
      
    }
}

