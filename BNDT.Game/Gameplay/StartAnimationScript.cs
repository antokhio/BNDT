using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Core;

namespace BNDT.Gameplay
{
    public class StartAnimationScript : StartupScript
    {
        // Declared public member fields and properties will show in the game studio

        [Display("Animation Key")]
        public string key;

        [Display("Animation Delay Seconds")]
        public double delayStart = 0;

        public override void Start()
        {
            if (delayStart > 0)
                Task.Run(async () =>
                {
                    await Task.Delay((int)(delayStart * 1000));
                    Entity.Get<AnimationComponent>().Play(key);
                });
            else
                Entity.Get<AnimationComponent>().Play(key);
        }
    }
}
