using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Graphics;
using ChildBirth.Loaders;

using ChildBirth.Simulation;

namespace ChildBirth.Rendering
{
    class Material : ContentObject
    {
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
        private Color4 color = Color4.White;
        public Color4 Color
        {
            set { this.color = value; }
            get { return this.color;  }
        }

        /// <summary>
        /// The array of textues with the size of the texture type enum
        /// Note memory is allocated for all the textures in advance
        /// </summary>
        private Texture[] textures = new Texture[Enum.GetValues(typeof(Texture.TextureType)).Length];

        /// <summary>
        /// Method to add a texture to the set of textures of current material
        /// Note the texture type is used as an index into the array of textures
        /// </summary>
        /// <param name="type">The type of the texture</param>
        /// <param name="texture">The texture</param>
        public void SetTexture(Texture.TextureType type, Texture texture)
        {
            textures[(int)type] = texture;
        }

        /// <summary>
        /// Method to insert all the uniforms required for the rendering of the material
        /// </summary>
        public void SetupUniforms()
        {
            Vector3 eye = SimulationManager.Instance.CurrentSimulation.MainCamera.Eye;
            Matrix4 view = SimulationManager.Instance.CurrentSimulation.MainCamera.View;
            Matrix4 proj = SimulationManager.Instance.CurrentSimulation.MainCamera.Projection;
            Vector3 lightPos = SimulationManager.Instance.CurrentSimulation.MainCamera.Eye;

            shader.insertUniform(Shader.UniformType.in_eyepos, eye);
            shader.insertUniform(Shader.UniformType.projection_matrix, proj);
            shader.insertUniform(Shader.UniformType.modelview_matrix, view);
            shader.insertUniform(Shader.UniformType.in_light, lightPos);
        }                                      

        /// <summary>
        /// Method to bind all the textures used in the current material
        /// </summary>
        public void SetupTextures()
        {
            int texUnit = 0;
            for (int i = 0; i < textures.Length; i++)
            {
                if (textures[i] == null)
                    continue;

                textures[i].Activate(texUnit);
                textures[i].Bind();

                string texName = textures[i].Type.ToString();
                
                shader.SetTextureUniform(texName, texUnit);
                
                texUnit++;
            }
        }
    }
}
