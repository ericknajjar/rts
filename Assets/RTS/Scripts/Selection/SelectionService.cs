using UnityEngine;
using System.Collections;
using UniRx;
using rts.tasks.model;
using System.Collections.Generic;
using rts.input;
using Zenject;
using System.Linq;

namespace rts.selection.model
{
    public interface ISelection
    {
        bool ScheduleTask(ITask task);
    }

    public class EmptySelection : ISelection
    {
        public bool ScheduleTask(ITask task)
        {
            return false;
        }
    }

    public interface ISelectionService
    {
        IReadOnlyReactiveProperty<ISelection> CurrentSelection { get; }
    }

    public class SimpleSelection: ISelection, ITaskVisitor
    {
        IList<GameObject> _units;

        public SimpleSelection(IList<GameObject> units)
        {
            _units = units;
        }

        public bool ScheduleTask(ITask task)
        {

            return task.Visit(this);
        }

        public bool WalkTask(IWalkTask task)
        {
            if(_units.Count > 0)
            {
                Vector2 averagePos = Vector2.zero;

                foreach (var unit in _units)
                {
                    averagePos += (Vector2)unit.transform.position;
                }

                averagePos /= _units.Count;

                foreach (var unit in _units)
                {
                    var delta = (Vector2)unit.transform.position - averagePos;
                    var target = task.Target + delta;

                    unit.GetComponent<Walkable>().WalkTo(target);
                }
            }
       
            return true;
        }
    }


    public class SelectionService : MonoInstaller, ISelectionService
    {
        [SerializeField] List<GameObject> _units;

        [Inject] IInputService _input;

        public IReadOnlyReactiveProperty<ISelection> CurrentSelection { get; private set; }

        void Start()
        {
            CurrentSelection = _input.Drag.SelectMany(dragEvent => 
            {

                return dragEvent.Done.Select(_ => dragEvent);
            }).Select(dragEvent => {

                List<GameObject> allThatContained = new List<GameObject>();

                var rect =  ScreenUtils.PointsToRect(dragEvent.Begin, dragEvent.End.Value);
                foreach (var unit in _units)
                {
                    var screenPos =  Camera.main.WorldToScreenPoint(unit.transform.position);

                    if (rect.Contains(screenPos))
                    {
                        allThatContained.Add(unit);
                    }
                }

                return new SimpleSelection(allThatContained);

            }).ToReadOnlyReactiveProperty<ISelection>(new EmptySelection());
        }

        public override void InstallBindings()
        {
            Container.Bind<ISelectionService>().FromInstance(this);
        }

    }
}


