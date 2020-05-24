using UnityEngine;
using System.Collections;
using Zenject;
using rts.input;
using rts.selection.model;
using UniRx;
using rts.tasks.model;

namespace rts.tasks
{
    public class WalkTaskController : MonoBehaviour
    {
        [Inject] IInputService _input;
        [Inject] ISelectionService _selection;

        void Start()
        {
            _input.MainInputAction
                .TakeUntilDestroy(this).Subscribe(pos =>
                {
                    var walkTarget = Camera.main.ScreenToWorldPoint(pos);
                    Debug.Log("wakj");
                    var task  = new WalkTask(walkTarget);
                    _selection.CurrentSelection.Value.ScheduleTask(task);

                });

        }
              
    }
}

