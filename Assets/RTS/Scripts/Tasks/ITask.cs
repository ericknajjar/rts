using UnityEngine;
using System.Collections;

namespace rts.tasks.model
{
    public interface ITaskVisitor
    {
        bool WalkTask(IWalkTask task);
    }

    public interface ITask 
    {
        bool Visit(ITaskVisitor visitor);
    }
}
