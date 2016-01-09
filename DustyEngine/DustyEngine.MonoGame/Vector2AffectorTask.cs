using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DustyEngine.MonoGame
{
    public class Vector2AffectorTask : BasicTask
    {
        public Vector2 Vector { get; set; }
        public Vector2 StepVector { get; set; }
        public VectorAffectorOperation Operation = VectorAffectorOperation.Add;

        public Vector2AffectorTask()
        {
            Vector = new Vector2();
            StepVector = new Vector2();
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
