using rts.input;
using UnityEngine;
using Zenject;
using UniRx;
using System;

namespace rts.selection.view
{
    public interface IDragArea
    {
        void Kill();
    }

    public interface IDragView
    {
        IDragArea CreateDragArea(Vector2 begin, IObservable<Vector2> end);
    }

    public class SelectionViewController: MonoBehaviour
    {
        [Inject] IInputService _inputService;
        [Inject] IDragView _dragView;

        void Start() 
        {
            _inputService.Drag.TakeUntilDestroy(this).SelectMany( ev =>
            {

                var dragArea = _dragView.CreateDragArea(ev.Begin, ev.End);

                return ev.Done.Select(_ => dragArea);

            }).Subscribe(dragArea => 
            {
                dragArea.Kill();
            });
        }

    }
}