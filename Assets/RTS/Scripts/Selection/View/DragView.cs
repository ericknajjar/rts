using UnityEngine;
using System.Collections;
using Zenject;
using System;
using UniRx;

namespace rts.selection.view
{
    public class DragView : MonoInstaller, IDragView
    {
        [SerializeField] Canvas _selectionCanvas;
        [SerializeField] RectTransform _selectionAreaPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IDragView>().FromInstance(this);
        }

        public IDragArea CreateDragArea(Vector2 begin, IObservable<Vector2> end)
        {
            RectTransform go = GameObject.Instantiate<RectTransform>(_selectionAreaPrefab, _selectionCanvas.transform);

            return new DragArea(begin,end.TakeUntilDestroy(go), go);
        }

        class DragArea : IDragArea

        {
            RectTransform _rectTransform;

            public DragArea(Vector2 begin, IObservable<Vector2> end, RectTransform rectTransform)
            {
                _rectTransform = rectTransform;

                end.Subscribe(endPosition => 
                {
             
                    var rect = ScreenUtils.PointsToRect(begin, endPosition);

                    rectTransform.anchoredPosition = rect.position;

                    rectTransform.sizeDelta = rect.size;
                });
            }
        
            public void Kill()
            {
               GameObject.Destroy(_rectTransform.gameObject);
            }
        }
    }


}
