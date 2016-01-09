using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustyEngine
{
    public class BasicTask : ITask
    {
        protected ulong _interval = 0;
        public ulong Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        protected ulong _updatesSinceLastExecution = 0;
        public ulong UpdatesSinceLastExecution
        {
            get { return _updatesSinceLastExecution; }
            set { _updatesSinceLastExecution = value; }
        }

        protected ulong _numExecutions = 0;
        public ulong NumExecutions
        {
            get { return _numExecutions; }
            set { _numExecutions = value; }
        }

        protected ulong _lifetimeMS = 0;
        public ulong LifetimeMS
        {
            get { return _lifetimeMS; }
            set { _lifetimeMS = value; }
        }

        protected ulong _lifetimeExecutions = 0;
        public ulong LifetimeExecutions
        {
            get { return _lifetimeExecutions; }
            set { _lifetimeExecutions = value; }
        }

        protected bool _paused = false;
        public bool Paused
        {
            get { return _paused; }
        }

        protected Driver _driver;
        public Driver Driver
        {
            get { return _driver; }
            set { _driver = value; }
        }

        protected ITask _parent;
        public ITask Parent
        {
            get { return _parent; }
        }

        protected List<ITask> _children;
        public List<ITask> Children
        {
            get { return _children; }
        }

        protected TaskIntervalMethod _intervalMethod = TaskIntervalMethod.Time;
        public TaskIntervalMethod IntervalMethod
        {
            get { return _intervalMethod; }
            set { _intervalMethod = value; }
        }

        protected ulong _pauseTime;
        protected ulong _lastExecutionTime;
        public ulong LastExecutionTime
        {
            get
            {
                return _lastExecutionTime;
            }
            set
            {
                _lastExecutionTime = value;
            }
        }

        protected ulong _totalExecutions = 0;

        public BasicTask()
        {
            _children = new List<ITask>();
        }

        public virtual void AddChild(ITask task)
        {
            task.Driver = _driver;
            _children.Add(task);
            task.OnAddedToParent(this);
            OnAddChild(task);
        }

        public virtual void RemoveChild(ITask task)
        {
            _children.Remove(task);
            task.OnRemovedFromParent(this);
            OnRemoveChild(task);
        }

        public virtual void ClearChildren()
        {
            foreach(var t in _children)
            {
                t.OnRemovedFromParent(this);
                t.ClearChildren();
            }

            _children.Clear();
        }
        
        public virtual void Pause()
        {
            _paused = true;
            _pauseTime = (ulong)DateTime.Now.Ticks;
            OnPause();

            foreach(var t in Children)
            {
                t.Pause();
            }
        }

        public virtual void Unpause()
        {
            _paused = false;

            // push up last execution time based upon the length of time paused (to avoid stutter)
            var timePaused = (ulong)DateTime.Now.Ticks - _pauseTime;
            _lastExecutionTime += timePaused;

            OnUnpause();

            foreach (var t in Children)
            {
                t.Unpause();
            }
        }

        public virtual void PauseChildren()
        {
            foreach(var t in _children)
            {
                t.Pause();
            }
        }

        public virtual void UnpauseChildren()
        {
            foreach(var t in _children)
            {
                t.Unpause();
            }
        }

        public virtual void RemoveSelf()
        {
            if(_parent != null)
            {
                Parent.RemoveChild(this);
            }
        }

        public virtual void OnAddChild(ITask task)
        {
        }

        public virtual void OnAddedToParent(ITask parent)
        {
            _parent = parent;
        }

        public virtual void OnChildrenPaused()
        {
        }

        public virtual void OnChildrenUnpaused()
        {
        }

        public virtual void OnCreation()
        {
        }

        public virtual void OnDestruction()
        {
        }

        public virtual void OnLifetimeExpired()
        {
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnRemoveChild(ITask task)
        {
        }

        public virtual void OnRemovedFromParent(ITask parent)
        {
            _parent = null;
        }

        public virtual void OnUnpause()
        {
        }

        public virtual void OnUpdate()
        {
        }
    }
}
