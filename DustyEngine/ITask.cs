using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustyEngine
{
    public interface ITask
    {
        ulong Interval { get; set; }
        ulong UpdatesSinceLastExecution { get; set; }
        ulong NumExecutions { get; set; }
        ulong LifetimeMS { get; set; }
        ulong LifetimeExecutions { get; set; }
        ulong LastExecutionTime { get; set; }
        bool Paused { get; }
        Driver Driver { get; set; }
        ITask Parent { get; }

        TaskIntervalMethod IntervalMethod { get; set; }
        List<ITask> Children { get; }

        void OnPause();
        void OnUnpause();
        void OnUpdate();
        void OnAddChild(ITask task);
        void OnRemoveChild(ITask task);
        void OnLifetimeExpired();
        void OnChildrenPaused();
        void OnChildrenUnpaused();
        void OnAddedToParent(ITask parent);
        void OnRemovedFromParent(ITask parent);

        void AddChild(ITask task);
        void RemoveChild(ITask task);
        void ClearChildren();
        void Pause();
        void Unpause();
        void PauseChildren();
        void UnpauseChildren();
        void RemoveSelf();
    }
}
