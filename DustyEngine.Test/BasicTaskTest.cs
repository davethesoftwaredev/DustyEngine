using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DustyEngine;

namespace DustyEngine.Test
{
    [TestClass]
    public class BasicTaskTest
    {
        [TestMethod]
        public void TestAddFirstTask()
        {
            BasicTask bt = new BasicTask();
            Driver.Instance.RootTask.AddChild(bt);

            Assert.IsNotNull(Driver.Instance.RootTask);
            Assert.AreEqual(Driver.Instance, bt.Driver);
            Assert.AreEqual(1, Driver.Instance.RootTask.Children.Count);
            Assert.AreEqual(bt.Parent, Driver.Instance.RootTask);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestAddChildTask()
        {
            BasicTask bt = new BasicTask();
            BasicTask child = new BasicTask();

            bt.AddChild(child);

            Assert.AreEqual(1, bt.Children.Count);
            Assert.AreEqual(bt, child.Parent);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestRemoveChild()
        {
            BasicTask bt = new BasicTask();
            BasicTask child = new BasicTask();

            bt.AddChild(child);
            Assert.AreEqual(1, bt.Children.Count);

            bt.RemoveChild(child);
            Assert.AreEqual(0, bt.Children.Count);
            Assert.IsNull(child.Parent);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestCounter()
        {
            var ct = new CounterTask();
            Driver.Instance.RootTask.AddChild(ct);
            ct.IntervalMethod = TaskIntervalMethod.Updates;

            Assert.AreEqual(0, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);

            ct.CounterInterval = -1;

            Driver.Instance.Update();
            Assert.AreEqual(0, ct.Value);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestPause()
        {
            var ct = new CounterTask();
            Driver.Instance.RootTask.AddChild(ct);

            ct.Pause();
            Driver.Instance.Update();
            Assert.AreEqual(0, ct.Value);
            ct.Unpause();
            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestDelayedTask()
        {
            var ct = new CounterTask();
            Driver.Instance.RootTask.AddChild(ct);
            ct.IntervalMethod = TaskIntervalMethod.Updates;

            ct.Interval = 5;
            Driver.Instance.Update();
            Assert.AreEqual(0, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(0, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(0, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(0, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);

            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(1, ct.Value);
            Driver.Instance.Update();
            Assert.AreEqual(2, ct.Value);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestLifetime()
        {
            var bt = new BasicTask();
            bt.LifetimeExecutions = 3;
            bt.IntervalMethod = TaskIntervalMethod.Updates;

            Driver.Instance.RootTask.AddChild(bt);
            Assert.AreEqual(1, Driver.Instance.RootTask.Children.Count);

            Driver.Instance.Update();
            Assert.AreEqual(1, Driver.Instance.RootTask.Children.Count);
            Driver.Instance.Update();
            Assert.AreEqual(1, Driver.Instance.RootTask.Children.Count);
            Driver.Instance.Update();
            Assert.AreEqual(0, Driver.Instance.RootTask.Children.Count);
            Assert.IsNull(bt.Parent);

            Driver.Instance.RootTask.ClearChildren();
        }

        [TestMethod]
        public void TestTimeTask()
        {
            var bt = new BasicTask();
            bt.Interval = 1000;

            Driver.Instance.RootTask.AddChild(bt);
            var time = DateTime.Now.Ticks / 10000;

            while(DateTime.Now.Ticks / 10000 - time < 3500)
            {
                Driver.Instance.Update();
            }

            Assert.AreEqual(3, (int)bt.NumExecutions);
            Driver.Instance.RootTask.ClearChildren();
        }
    }
}
