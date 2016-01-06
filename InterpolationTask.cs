using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustyEngine
{
    public class InterpolationTask : BasicTask
    {
        public float Start { get; set; }
        public float End { get; set; }
        public float Value { get; set; }
        public float NumIntervals { get; set; }
        public float CurrentInterval { get; set; }

        public override void OnUpdate()
        {
            base.OnUpdate();
            CurrentInterval++;

            if (CurrentInterval > NumIntervals)
                CurrentInterval = NumIntervals;

            Value = Start + ((CurrentInterval / (NumIntervals == 0 ? 1 : NumIntervals)) * (Start - End));
        }
    }
}
