using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics;
using ChildBirth.Loaders;

namespace ChildBirth.Rendering
{
    class Material : ContentObject
    {
        /// <summary>
        /// The private field and public property for the name of the material
        /// </summary>
        private String name;
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// The private field and public property for the shader object of the material
        /// </summary>
        private Shader shader;
        public Shader Shader
        {
            get { return this.shader;  }
            set { this.shader = value; }
        }

        /// <summary>
        /// The private field and public property for the base color of the material
        /// </summary>
        private Color4 color;
        public Color4 Color
        {
            set { this.color = value; }
            get { return this.color;  }
        }
    }
}
