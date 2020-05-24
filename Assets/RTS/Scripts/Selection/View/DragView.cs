using UnityEngine;
using System.Collections;
using Zenject;
using System;
using UniRx;

namespace rts.selection.view
{
    public class DragView : MonoInstaller, IDragView
    {

        void Start()
        {

        }

        public override void InstallBindings()
        {
            Container.Bind<IDragView>().FromInstance(this);
        }

        public IDragArea CreateDragArea(Vector2 begin, IObservable<Vector2> end)
        {
            return new DragArea(begin,end.TakeUntilDestroy(this));
        }

        class DragArea : IDragArea
        {
            public DragArea(Vector2 begin, IObservable<Vector2> end)
            {
                end.Subscribe(_ => Debug.Log(_));
            }
        

            public void Kill()
            {
                Debug.Log("morri");
            }
        }
    }


}
