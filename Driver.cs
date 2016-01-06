using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DustyEngine;

namespace DustyEngine
{
    public class Driver
    {
        private static Driver _instance = new Driver();
        public static Driver Instance
        {
            get
            {
                return _instance;
            }
        }

        protected RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();
        public RNGCryptoServiceProvider Random
        {
            get
            {
                return _random;
            }
        }

        protected ITask _rootTask = null;
        public ITask RootTask
        {
            get
            {
                return _rootTask;
            }
        }

        public Driver()
        {
            _rootTask = new BasicTask();
            _rootTask.Driver = this;
        }

        public void Update()
        {
            var removalStack = new Stack<ITask>();
            UpdateTask(_rootTask, removalStack);

            while(removalStack.Count > 0) 
            {
                var task = removalStack.Pop();
                if(task.Parent != null)
                {
                    task.Parent.RemoveChild(task);
                }
            }
        }

        protected void UpdateTask(ITask t, Stack<ITask> removalStack)
        {
            var removeTasks = new List<ITask>();

            if(!t.Paused)
            {
                t.UpdatesSinceLastExecution++;

                if (t.UpdatesSinceLastExecution >= t.Interval)
                {
                    t.OnUpdate();
                    t.UpdatesSinceLastExecution = 0;
                    t.NumExecutions++;

                    if(t.LifetimeExecutions > 0 && t.NumExecutions >= t.LifetimeExecutions)
                    {
                        removalStack.Push(t);
                    }
                }

                foreach(var c in t.Children)
                {
                    UpdateTask(c, removalStack);
                }
            }
        }
    }
}
