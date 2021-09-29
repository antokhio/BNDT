﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace BNDT.Gameplay
{
    public class VendorIdleScript : StartupScript
    {
        // Declared public member fields and properties will show in the game studio


        public override void Start()
        {
            Entity.Get<AnimationComponent>().Play("Waving");
        }
    }
}
