using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildBirth.Rendering.Defaults
{
    class DefaultMaterial : Material
    {
        public DefaultMaterial()
        {
            this.Shader = new DefaultShader();
        }
    }
}
