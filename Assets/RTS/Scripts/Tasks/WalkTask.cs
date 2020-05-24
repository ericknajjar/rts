using UnityEngine;
using System.Collections;

namespace rts.tasks.model
{

    public interface IWalkTask: ITask
    {
        Vector2 Target { get;  }
    }

    public class WalkTask : IWalkTask
    {
        public WalkTask(Vector2 target)
        {
            Target = target;
        }

        public Vector2 Target { get; }

        public bool Visit(ITaskVisitor visitor)
        {
            return visitor.WalkTask(this);
        }
    }

}
