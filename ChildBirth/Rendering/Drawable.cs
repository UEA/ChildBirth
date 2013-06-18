using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using ChildBirth.Simulation;
using ChildBirth.Structures;

namespace ChildBirth.Rendering
{
    /// <summary>
    /// A class to represent renderable objects, which inherts from the base class to
    /// facilitate simulation structure independance
    /// </summary>
    abstract class Drawable : SimObject
    {
        /// <summary>
        /// Protected list of MeshComponent objects to be used by the subclasses
        /// </summary>
        protected List<MeshComponent> components = new List<MeshComponent>();
        
        /// <summary>
        /// Private field and public property to store the material assigned to the mesh
        /// </summary>
        private Material material;
        public Material Material
        {
            get { return this.material;  }
            set { this.material = value; }
        }

        /// <summary>
        /// Boolean fields indicating if the mesh receives and casts shadows respectively
        /// </summary>
        protected bool CastShadows = false;
        protected bool ReceiveShadows = false;

        /// <summary>
        /// Initialize the mesh VBO's for rendering
        /// </summary>
        /// <param name="curMesh">The mesh to be rendered</param>
        /// <param name="shader">The shader with wich the rendering is to be performed</param>
        public virtual void SetVBOs(Mesh curMesh, Shader shader)
        {
            int shaderHandle = shader.Handle;

            int normalIndex   = GL.GetAttribLocation(shaderHandle, "in_normal");
            int positionIndex = GL.GetAttribLocation(shaderHandle, "in_position");
            int tangentIndex  = GL.GetAttribLocation(shaderHandle, "in_tangent");
            int textureIndex  = GL.GetAttribLocation(shaderHandle, "in_texture");


            if (normalIndex != -1)
            {
                GL.EnableVertexAttribArray(normalIndex);
                GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.NormalVboHandle);
                GL.VertexAttribPointer(normalIndex, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            }

            if (positionIndex != -1)
            {
                GL.EnableVertexAttribArray(positionIndex);
                GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.PositionVboHandle);
                GL.VertexAttribPointer(positionIndex, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            }

            if (tangentIndex != -1)
            {
                GL.EnableVertexAttribArray(tangentIndex);
                GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.TangentVboHandle);
                GL.VertexAttribPointer(tangentIndex, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            }

            if (textureIndex != -1)
            {
                GL.EnableVertexAttribArray(textureIndex);
                GL.BindBuffer(BufferTarget.ArrayBuffer, curMesh.TextureVboHandle);
                GL.VertexAttribPointer(textureIndex, 2, VertexAttribPointerType.Float, true, Vector2.SizeInBytes, 0);
            }


            GL.BindBuffer(BufferTarget.ElementArrayBuffer, curMesh.IndexVboHandle);
        }

        /// <summary>
        /// Initialize the material for rendering
        /// </summary>
        public virtual void SetUpMaterial()
        {
            material.Shader.Bind();

            material.SetupUniforms();
            material.SetupTextures();

            if (ReceiveShadows)
                SetUpShadowReceiveMaterial();
        }


        public virtual void SetUpShadowReceiveMaterial()
        {

            //Matrix4 lightView = SceneManager.Light.View;
            //Matrix4 lightProj = SceneManager.Light.Projection;

            //material.Shader.insertUniform(Shader.UniformType.light_view, ref lightView);
            //material.Shader.insertUniform(Shader.UniformType.light_proj, ref lightProj);

            //// Moving from unit cube [-1,1] to [0,1]  
            //Matrix4 bias = new Matrix4(
            //                           0.5f, 0.0f, 0.0f, 0.0f,
            //                           0.0f, 0.5f, 0.0f, 0.0f,
            //                           0.0f, 0.0f, 0.5f, 0.0f,
            //                           0.5f, 0.5f, 0.5f, 1.0f
            //                           );

            //bias = lightView * lightProj * bias;

            //material.Shader.insertUniform(Shader.UniformType.shadow_bias, ref bias);

            //float lightFar = SceneManager.Light.Far;
            //material.Shader.insertUniform(Shader.UniformType.in_far, ref lightFar);

        }

        public virtual void SetUpShadowCastMaterial()
        {
            //Shader shader = SceneManager.ShadowPassShader;
            //GL.UseProgram(shader.handle);

            //Matrix4 lightView = SceneManager.Light.View;
            //Matrix4 lightProj = SceneManager.Light.Projection;

            //shader.insertUniform(Shader.Uniform.light_view, ref lightView);
            //shader.insertUniform(Shader.Uniform.light_proj, ref lightProj);

            //float lightFar = SceneManager.Light.Far;
            //shader.insertUniform(Shader.Uniform.in_far, ref lightFar);

        }

        protected void AddComponent(MeshComponent meshComponent)
        {
            this.components.Add(meshComponent);
        }
    }
}

