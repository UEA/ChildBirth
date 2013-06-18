using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK.Graphics.OpenGL;
using ChildBirth.Loaders;

namespace ChildBirth.Rendering
{
    class Texture : ContentObject
    {
        /// <summary>
        /// Private storage for texture id
        /// </summary>
        private int id;
        /// <summary>
        /// The texture id
        /// </summary>
        public int Id
        {
            get { return this.id;  }
            set { this.id = value; }
        }

        /// <summary>
        /// Method to activate the desired texture unit using OpenGL
        /// </summary>
        /// <param name="texUnit">Texture unit index to be activated</param>
        public void Activate(int texUnit)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
        }

        /// <summary>
        /// Method to bind a texture using OpenGL
        /// </summary>
        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, this.id);
        }

        /// <summary>
        /// Property to access the type of the texture
        /// </summary>
        public TextureType Type
        {
            get { return this.type;  }
            set { this.type = value; }
        }
        private TextureType type;

        /// <summary>
        /// The set of texture types
        /// </summary>
        public enum TextureType
        {
            base1,
            base2,
            base3,
            envMap,
            normal,
            emit,
            reflection,
            emitMap,
            specMap,
            env,
            definfo,
            light
        }
    }
}
