using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBirth.Rendering
{
    class Material
    {
        private String name;

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private Shader shader;

        public Shader Shader
        {
            get { return this.shader;  }
            set { this.shader = value; }
        }
    }
}
