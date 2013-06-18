using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using ChildBirth.Loaders;

namespace ChildBirth.Rendering
{
    /// <summary>
    /// Class representing a GLSL shader pair
    /// Inherits from the ContentObject to facilitate our content pipeline
    /// </summary>
    class Shader : ContentObject
    {
        /// <summary>
        /// Priavte field and public property to sotre and access the handle to the shader in the GPU memory
        /// </summary>
        private int handle;
        /// <summary>
        /// Shader handle
        /// </summary>
        public int Handle
        {
            set { this.handle = value; }
            get { return this.handle;  }
        }

        /// <summary>
        /// Private field to store the vertex shader source code
        /// </summary>
        private string vShader;

        /// <summary>
        /// Private field to store the vertex shader source code
        /// </summary>
        private string fShader;

        /// <summary>
        /// Vertex shader source
        /// </summary>
        public String VShader
        {
            get { return this.vShader;  }
            set { this.vShader = value; }
        }

        /// <summary>
        /// Fragment shader source
        /// </summary>
        public String FShader
        {
            get { return this.fShader;  }
            set { this.fShader = value; }
        }

        /// <summary>
        /// Bind the shader for rendering
        /// </summary>
        public void Bind()
        {
            GL.UseProgram(handle);
        }

        /// <summary>
        /// Unbind the shader for rendering using fixed pipeline
        /// </summary>
        public void Unbind()
        {
            GL.UseProgram(0);
        }

        /// <summary>
        /// A public enum to enumerate all the required uniforms used in the shaders of the program
        /// NOTE: The elements of the enum represent the actual UniformType names in the shader so that
        /// the locations are determined automatically
        /// </summary>
        public enum UniformType
        {
            /// <summary>
            /// The view matrix of the current camera
            /// </summary>
            modelview_matrix,
            /// <summary>
            /// The projection matrix of the current camera
            /// </summary>
            projection_matrix,
            /// <summary>
            /// rotation matrix representing the combined rotation of the mesh component and the model
            /// </summary>
            rotation_matrix,
            /// <summary>
            /// matrix representing transformation of the whole model
            /// </summary>
            model_matrix,
            /// <summary>
            /// mesh_matrix is the matrix representing the local transformations of the mesh component
            /// </summary>
            mesh_matrix,
            /// <summary>
            /// matrix representing the view trasformation of the current light used for shadow mapping
            /// </summary>
            light_view,
            /// <summary>
            /// matrix representing the projection of the current light used for shadow mapping
            /// </summary>
            light_proj,
            light_matrix,
            /// <summary>
            /// the shadow bias value used for shadow mapping
            /// </summary>
            shadow_bias,

            /// <summary>
            /// the vector representing the camera eye
            /// </summary>
            in_eyepos,
            /// <summary>
            /// float representing current simulation time
            /// </summary>
            in_time,
            /// <summary>
            /// //TODO//: dont remember exactly what this does
            /// </summary>
            in_light,

            /// <summary>
            /// vector representing the ambient color and intensity
            /// </summary>
            ambient_light
        }


        /// <summary>
        /// Method for setting a float uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
        public void insertUniform(UniformType uni, float value)
		{
			int location = uniformLocations[(int)uni];
			if (location != -1)
				GL.Uniform1(location, 1, ref value);
		}

        /// <summary>
        /// Method for setting a integer uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
		internal void insertUniform(UniformType uni, int value)
		{
			int location = uniformLocations[(int)uni];
			if (location != -1)
				GL.Uniform1(location, 1, ref value);
		}

        /// <summary>
        /// Method for setting a 4D vector to uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
		internal void insertUniform(UniformType uni, Vector4 value)
		{
			int location = uniformLocations[(int)uni];
			if (location != -1)
				GL.Uniform4(location, ref value);
		}

        /// <summary>
        /// Method for setting a float 3D vector to a uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
		public void insertUniform(UniformType uni, Vector3 value)
		{
			int location = uniformLocations[(int)uni];
			if (location != -1)
				GL.Uniform3(location, ref value);
		}

        /// <summary>
        /// Method for setting a 2D vector to a uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
		internal void insertUniform(UniformType uni, Vector2 value)
		{
			int location = uniformLocations[(int)uni];
			if (location != -1)
				GL.Uniform2(location, ref value);
		}

        /// <summary>
        /// Method for setting a matrix uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
		public void insertUniform(UniformType uni, Matrix4 value)
		{
            Matrix4 refValue = value;
			int location = uniformLocations[(int)uni];
			if (location != -1)
                GL.UniformMatrix4(location, false, ref refValue);
		}



        /// <summary>
        /// Method for setting a matrix uniform value
        /// </summary>
        /// <param name="uni">The uniform to be set</param>
        /// <param name="value">The value to be assigned to the uniform</param>
        public void insertUniform(UniformType uni, ref Matrix4 value)
        {
            int location = uniformLocations[(int)uni];
            if (location != -1)
                GL.UniformMatrix4(location, false, ref value);
        }

        public void SetTextureUniform(String texName, int texUnit)
        {
            GL.Uniform1(GetUniformLocation(texName), texUnit);
        }

        public int GetUniformLocation(String name)
        {
            return GL.GetUniformLocation(handle, name);
        }

        /// <summary>
        /// storage for uniform locations
        /// </summary>
        private int[] uniformLocations;

        /// <summary>
        /// Method to retreive uniform locations from the linked shader based on the ShaderType enum values
        /// NOTE: The uniform names and the values in the ShaderType enum must match
        /// </summary>
        private void generateLocations()
		{
			string[] names = Enum.GetNames(typeof(UniformType));
			
			int handlesCount = names.Length;
			uniformLocations = new int[handlesCount];
			
			for (int i = 0; i < handlesCount; i++)
			{
                uniformLocations[i] = GL.GetUniformLocation(handle, names[i]);
			}
		}

        /// <summary>
        /// Method to initialize the shader by compiling and linking
        /// Additionally the uniform locations are calculated
        /// </summary>
        public void Initialize()
        {
            int vertexShaderHandle,
            fragmentShaderHandle;

            vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShaderHandle, vShader);
            GL.ShaderSource(fragmentShaderHandle, fShader);

            GL.CompileShader(vertexShaderHandle);
            GL.CompileShader(fragmentShaderHandle);

            String errorV = GL.GetShaderInfoLog(vertexShaderHandle);
            String errorF = GL.GetShaderInfoLog(fragmentShaderHandle);

            Console.WriteLine(errorV);
            Console.WriteLine(errorF);

            // Create program
            handle = GL.CreateProgram();

            GL.AttachShader(handle, vertexShaderHandle);
            GL.AttachShader(handle, fragmentShaderHandle);

            GL.LinkProgram(handle);

            Console.WriteLine(GL.GetProgramInfoLog(handle));

            generateLocations();
        }
    }
}
