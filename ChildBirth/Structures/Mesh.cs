using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using ChildBirth.Loaders;

namespace ChildBirth.Structures
{
    class Mesh : ContentObject
    {
        /// <summary>
        /// List of faces contained in the mesh
        /// </summary>
        public List<Face> FaceList = new List<Face>();

        /// <summary>
        /// Handles to the vbo objects stored on the GPU
        /// </summary>
        public uint PositionVboHandle,
                    NormalVboHandle,
                    TextureVboHandle,
                    TangentVboHandle,
                    IndexVboHandle;

        /// <summary>
        /// Storage for the position, normal and tangent data of the mesh
        /// </summary>
        public Vector3[] PositionData,
                         NormalData,
                         TangentData;

        /// <summary>
        /// Storage for the texture coordinate data of the mesh
        /// </summary>
        public Vector2[] TexCoordData;

        /// <summary>
        /// Storage for the element indices of the mesh
        /// </summary>
        private int[] ElementsData;

        public void Initialize()
        {
            InitVBO();
        }

        /// <summary>
        /// Generate and initialize the VBOs for the mesh
        /// </summary>
        private void InitVBO()
        {
            GL.GenBuffers(1, out NormalVboHandle);
            GL.GenBuffers(1, out PositionVboHandle);
            GL.GenBuffers(1, out TextureVboHandle);
            GL.GenBuffers(1, out TangentVboHandle);
            GL.GenBuffers(1, out IndexVboHandle);


            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalVboHandle);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                                   new IntPtr(NormalData.Length * Vector3.SizeInBytes),
                                   NormalData, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, PositionVboHandle);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                                   new IntPtr(PositionData.Length * Vector3.SizeInBytes),
                                   PositionData, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureVboHandle);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer,
                                   new IntPtr(TexCoordData.Length * Vector2.SizeInBytes),
                                   TexCoordData, BufferUsageHint.StaticDraw);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, TangentVboHandle);
            //GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
            //                       new IntPtr(TangentData.Length * Vector3.SizeInBytes),
            //                       TangentData, BufferUsageHint.StaticDraw);

            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexVboHandle);
            //GL.BufferData(BufferTarget.ElementArrayBuffer,
            //              new IntPtr(sizeof(uint) * ElementsData.Length),
            //              ElementsData, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }

    public class Vertex
    {
        public int Vi = 0;
        public int Ti = 0;
        public int Ni = 0;
        public int Normalihelper = -1;

        public Vertex(int vi, int ti, int ni)
        {
            Vi = vi;
            Ti = ti;
            Ni = ni;
        }

        public Vertex()
        {
        }
    }

    public class Face
    {
        public Vertex[] Vertices;
        public bool isTemp;
        public int position;

        public Face(Vertex ind1, Vertex ind2, Vertex ind3)
        {
            Vertices = new Vertex[3];
            Vertices[0] = ind1;
            Vertices[1] = ind2;
            Vertices[2] = ind3;
        }

        public Face(Vertex ind1, Vertex ind2, Vertex ind3, Vertex ind4)
        {
            Vertices = new Vertex[4];
            Vertices[0] = ind1;
            Vertices[1] = ind2;
            Vertices[2] = ind3;
            Vertices[3] = ind4;
        }
    }
}
