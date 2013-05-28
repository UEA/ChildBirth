using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChildBirth.Loaders;

namespace ChildBirth.Rendering
{
    class Shader : ContentObject
    {
        /// <summary>
        /// Private field and public property to store and access the name of the shader
        /// </summary>
        private String name;
        public String Name
        {
            set { this.name = value; }
            get { return this.name;  }
        }

        /// <summary>
        /// Priavte field and public property to sotre and access the handle to the shader in the GPU memory
        /// </summary>
        private int handle;
        public int Handle
        {
            set { this.handle = value; }
            get { return this.handle;  }
        }

        /// <summary>
        /// Private field to store the vertex and fragment shader source code
        /// </summary>
        private string vShader;
        private string fShader;
    }
}
