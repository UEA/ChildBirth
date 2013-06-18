using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using ChildBirth.Structures;

namespace ChildBirth.Loaders.Models
{
    class MeshLoader : Loader
    {
        #region Singleton declaration
        private static MeshLoader instance = null;
        public static MeshLoader GetInstance()
        {
            if (instance == null)
                instance = new MeshLoader();
            return instance;
        }

        private MeshLoader() { }
        #endregion

        /// <summary>
        /// Temporary data storage used for VBO construction
        /// </summary>
        private List<Vector3> PositionDataList = new List<Vector3>(),
                             NormalDataList = new List<Vector3>();
        private List<Vector2> TexCoordDataList = new List<Vector2>();

        private List<int> IndexDataList = new List<int>();

        public Mesh GetMesh(String name)
        {
            return (Mesh) base.GetObject(name);
        }

        protected override ContentObject Load(string name)
        {
            Mesh mesh = LoadObj(ConstructURIAndRefineName(ref name));

            mesh.Initialize();
            library.Add(mesh);

            return mesh;
        }

        private Mesh LoadObj(String uri)
        {
            Mesh mesh = new Mesh();

            // Read the file and display it line by line.
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(uri);

            while ((line = file.ReadLine()) != null)
            {
                string[] sline = line.Split(new string[] { " " }, 10, StringSplitOptions.None);

                if (sline[0] == "v")
                {
                    float X = float.Parse(sline[1]);
                    float Y = float.Parse(sline[2]);
                    float Z = float.Parse(sline[3]);
                    PositionDataList.Add(new Vector3(X, Y, Z));
                }

                if (sline[0] == "vn")
                {
                    float X = float.Parse(sline[1]);
                    float Y = float.Parse(sline[2]);
                    float Z = float.Parse(sline[3]);
                    NormalDataList.Add(new Vector3(X, Y, Z));

                }

                if (sline[0] == "vt")
                {
                    float X = float.Parse(sline[1]);
                    float Y = 1 - float.Parse(sline[2]);
                    TexCoordDataList.Add(new Vector2(X, Y));

                }

                if (sline[0] == "f")
                {
                    string[] segment = sline[1].Split(new string[] { "/" }, 10, StringSplitOptions.None);
                    if (segment.Length == 3)
                    {
                        Vertex fp1 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        segment = sline[2].Split(new string[] { "/" }, 10, StringSplitOptions.None);
                        Vertex fp2 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        segment = sline[3].Split(new string[] { "/" }, 10, StringSplitOptions.None);
                        Vertex fp3 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        mesh.FaceList.Add(new Face(fp1, fp2, fp3));
                    }
                    else if (segment.Length == 3)
                    {
                        Vertex fp1 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        segment = sline[2].Split(new string[] { "/" }, 10, StringSplitOptions.None);
                        Vertex fp2 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        segment = sline[3].Split(new string[] { "/" }, 10, StringSplitOptions.None);
                        Vertex fp3 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        segment = sline[4].Split(new string[] { "/" }, 10, StringSplitOptions.None);
                        Vertex fp4 = new Vertex(int.Parse(segment[0]) - 1, int.Parse(segment[1]) - 1, int.Parse(segment[2]) - 1);

                        mesh.FaceList.Add(new Face(fp1, fp2, fp3, fp4));
                    }
                }
            }

            file.Close();

            prepareFaceData(ref mesh);

            return mesh;
        }

        private void prepareFaceData(ref Mesh mesh)
        {
            List<VertexDataSet> vertexDataList = new List<VertexDataSet>();

            foreach(Face face in mesh.FaceList)
            {
                foreach(Vertex vertex in face.Vertices)
                {
                    VertexDataSet vertexData = new VertexDataSet();
                    vertexData.position = PositionDataList[vertex.Vi];
                    vertexData.normal = NormalDataList[vertex.Ni];
                    vertexData.texture = TexCoordDataList[vertex.Ti];

                    vertexDataList.Add(vertexData);
                }
            }

            int vertNum = vertexDataList.Count;

            mesh.PositionData = new Vector3[vertNum];
            mesh.NormalData = new Vector3[vertNum];
            mesh.TexCoordData = new Vector2[vertNum];

            for (int i = 0; i < vertexDataList.Count; i++)
            {
                mesh.PositionData[i] = vertexDataList[i].position;
                mesh.NormalData[i] = vertexDataList[i].normal;
                mesh.TexCoordData[i] = vertexDataList[i].texture;
            }
        }

        protected override string ContentSubDirectory { get { return "Models/"; } }
        protected override string FileExtension { get { return ".obj"; } }

        private struct VertexDataSet
        {
            public Vector3 position;
            public Vector3 normal;
            public Vector3 tangent;
            //public float[] boneWeight = null;
            //public int[] boneId;
            public Vector2 texture;

            public float threshold;

            public bool Equals(VertexDataSet vert)
            {
                threshold = 0.0001f;
                if ((this.position - vert.position).LengthFast < threshold &&
                    (this.texture - vert.texture).LengthFast < threshold &&
                    (this.normal - vert.normal).LengthFast < threshold)
                    return true;
                else
                    return false;
            }
        }
    }
}
