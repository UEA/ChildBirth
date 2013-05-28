using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBirth.Rendering
{
    class Shader
    {
        private String name;
        public String Name
        {
            set { this.name = value; }
            get { return this.name;  }
        }

        private int handle;
        public int Handle
        {
            set { this.handle = value; }
            get { return this.handle;  }
        }
    }
}
