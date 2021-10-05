using BNDT.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BNDT.Gameplay
{
    public class PoringScript : SyncScript
    {
        public Prefab CoinGetEffect { get; set; }
        public Prefab CoinSpawnModel { get; set; }

        public Trigger Trigger { get; set; }
        private EventReceiver<bool> triggeredEvent;

        private bool activated = false;
        private float animationTime = 0;

        public override void Update()
        {
            bool triggered;
            if (!activated && (triggeredEvent?.TryReceive(out triggered) ?? false))
            {
                CollisionStarted();
            }

            UpdateAnimation();
        }

        protected void UpdateAnimation()
        {
            if (!activated)
                return;


            animationTime += (float)Game.UpdateTime.Elapsed.TotalSeconds * 4;

            //Top.Transform.Rotation.X = (float)MathUtil.Lerp(0, -0.25, 4 * (float) Utils.Smoothstep(0, 0.7, 2 * (double) Math.Sin(animationTime)));

            //Entity.Transform.RotationEulerXYZ = new Vector3((float)MathUtil.Lerp(0, -0.25, 4 * (float)Utils.Smoothstep(0, 0.7, 2 * (double)Math.Sin(animationTime))), 0, 0);



            var coinHeight = Math.Max(0, Math.Sin(animationTime));
            Entity.Transform.Position.Y = (float)coinHeight;

            var uniformScale = (float)Math.Max(0, Math.Min(1, (2 * Math.PI - animationTime) / Math.PI));
            Entity.Transform.Scale = new Vector3(uniformScale);

        }

        public override void Start()
        {
            base.Start();

            triggeredEvent = (Trigger != null) ? new EventReceiver<bool>(Trigger.TriggerEvent) : null;

            //top = Entity.GetChild(0);
        }

        protected void CollisionStarted()
        {
            activated = true;

            var effectMatrix = Matrix.Translation(Entity.Transform.WorldMatrix.TranslationVector);
            this.SpawnPrefabInstance(CoinGetEffect, null, 1.8f, effectMatrix);

            Func<Task> cleanupTask = async () =>
            {
                await Game.WaitTime(TimeSpan.FromMilliseconds(4000));

                Game.RemoveEntity(Entity);
            };

            Script.AddTask(cleanupTask);

            Random rand = new Random();
            var numCoins = 3 + rand.Next(4);
            for (int i = 0; i < numCoins; i++)
            {
                var offsetVector = new Vector3((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble(), (float)rand.NextDouble() - 0.5f);
                effectMatrix = Matrix.Scaling(0.7f + (float)rand.NextDouble() * 0.3f) * Matrix.Translation(Entity.Transform.WorldMatrix.TranslationVector + offsetVector * 2f);
                this.SpawnPrefabModel(CoinSpawnModel, null, effectMatrix, offsetVector * (3f + (float)rand.NextDouble() * 3f));
            }
        }
    }
}
