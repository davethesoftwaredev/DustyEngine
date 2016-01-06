using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustyEngine
{
    public class CounterTask : BasicTask
    {
        public int Start { get; set; }
        public int CounterInterval { get; set; }
        public int Value { get; set; }

        public CounterTask()
        {
            CounterInterval = 1;       
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Value += CounterInterval;
        }

        public void Reset()
        {
            Value = Start;
        }
    }
}
