using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DustyEngine.MonoGame
{
    public class Vector3AffectorTask : BasicTask
    {
        public Vector3 Vector { get; set; }
        public Vector3 StepVector { get; set; }
        public VectorAffectorOperation Operation = VectorAffectorOperation.Add;

        public Vector3AffectorTask()
        {
            Vector = new Vector3();
            StepVector = new Vector3();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            switch(Operation)
            {
                case VectorAffectorOperation.Add:
                    Vector += StepVector;
                    break;
                case VectorAffectorOperation.Multiply:
                    Vector *= StepVector;
                    break;
                case VectorAffectorOperation.Divide:
                    Vector /= StepVector;
                    break;
            }
        }
    }
}
