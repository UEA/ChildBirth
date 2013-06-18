using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using ChildBirth.Structures;
using ChildBirth.Loaders.Models;

namespace ChildBirth.Rendering
{
    class Model : Drawable
    {
        private Model()
        {
        }

        public Model(String meshName)
        {
            this.Material = new Defaults.DefaultMaterial();
            Mesh mesh = MeshLoader.GetInstance().GetMesh(meshName);
            base.AddComponent(new MeshComponent(this, mesh));
        }

        public Model(String meshName, String materialName)
        {
            this.Material = Loaders.Materials.MaterialLoader.GetInstance().GetMaterial(materialName);
            Mesh mesh = MeshLoader.GetInstance().GetMesh(meshName);
            base.AddComponent(new MeshComponent(this, mesh));
        }

        public override void Render(RenderPass pass)
        {
                Render();
        }

        public override void Render()
        {
            SetUpMaterial();
            Render(Material.Shader);
        }

        public override void Render(Shader shader)
        {
            Mesh tmpMesh = null;

            foreach (MeshComponent comp in components)
            {
                if (tmpMesh != comp.Mesh)
                    SetVBOs(comp.Mesh, shader);

                shader.insertUniform(Shader.UniformType.mesh_matrix, comp.Transform);
                shader.insertUniform(Shader.UniformType.rotation_matrix, comp.GlobalOrientationMatrix);
                shader.insertUniform(Shader.UniformType.model_matrix, this.transform);

                GL.DrawArrays(BeginMode.Triangles, 0, comp.Mesh.PositionData.Length / 3);

                tmpMesh = comp.Mesh;
            }
        }

        public override void Update(double time)
        {
            base.Update(time);
        }
    }
}
