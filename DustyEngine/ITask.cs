using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustyEngine
{
    public interface ITask
    {
        UInt32 Interval { get; set; }
        UInt32 UpdatesSinceLastExecution { get; set; }
        UInt32 NumExecutions { get; set; }
        UInt32 LifetimeMS { get; set; }
        UInt32 LifetimeExecutions { get; set; }
        bool Paused { get; }
        Driver Driver { get; set; }
        ITask Parent { get; }

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
