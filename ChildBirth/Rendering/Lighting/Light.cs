using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;

namespace ChildBirth.Rendering.Lighting
{
    class Light : Cameras.Camera
    {
        public override Vector3 Position
        {
            get
            {
                return Eye;
            }
            set
            {
                Eye = value;
                base.Position = value;
            }
        }
    }
}
