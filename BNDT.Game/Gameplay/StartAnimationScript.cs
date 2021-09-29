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

        public override void Start()
        {
            Entity.Get<AnimationComponent>().Play(key);
        }
    }
}
