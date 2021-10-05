using BNDT.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace BNDT.Gameplay
{
    public class BubbleScript : SyncScript
    {
        public double Time { get; set; } = 1;
        public double TimeOffset { get; set; } = 0;

        private float animationTime = 0;
        

        private Vector3 scale;
        private float eY;

        public override void Update()
        {
            var dt = (float)Game.UpdateTime.Elapsed.TotalSeconds;
            animationTime += dt * (float)Time;

            var anim =(float) Utils.Smoothstep(0.0, 0.4, Math.Sin(animationTime + (float)TimeOffset));

            Entity.Transform.Position.Y = eY + anim * 0.1f ;
            Entity.Transform.Scale = scale * new Vector3(anim);
        }

        public override void Start()
        {
            base.Start();

            scale = Entity.Transform.Scale;
            eY = Entity.Transform.Position.Y;
        }

   
    }
}
