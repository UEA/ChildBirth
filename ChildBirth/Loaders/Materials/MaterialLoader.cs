using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using ChildBirth.Rendering;
using ChildBirth.Loaders.Textures;
using ChildBirth.Loaders.Shaders;

namespace ChildBirth.Loaders.Materials
{
    class MaterialLoader
    {
        #region Singleton declaration
        /// <summary>
        /// Singleton private static class instance
        /// </summary>
        private static MaterialLoader instance = null;

        public static MaterialLoader GetInstance()
        {
            if (instance == null)
                instance = new MaterialLoader();
            return instance;
        } 
        #endregion

        protected override Material Load(String name)
        {
            XmlReader reader = XmlReader.Create(ContentDir + name);

            Material material = new Material();
            material.Name = name;

            String uri = name;

            if (!uri.EndsWith(".mat"))
            {
                uri += ".mat";
            }

            // Parsing shader data
            if (reader.Name == "material" && reader.HasAttributes)
            {
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name == "shader")
                        material.shader = ShaderLoader.GetShader(reader.Value);

                }
                reader.MoveToElement();
            }

            ///// Parsing textures
            if (reader.Name == "textures" && reader.HasAttributes)
            {
                while (reader.MoveToNextAttribute())
                {
                    Texture tmpTex = TextureLoader.GetTexture(reader.Value);

                    if (reader.Name == "base")
                        material.SetTexture(Material.TexType.baseTexture, tmpTex);

                    if (reader.Name == "base2")
                        material.SetTexture(Material.TexType.base2Texture, tmpTex);

                    if (reader.Name == "envmap")
                        material.SetTexture(Material.TexType.envMapTexture, tmpTex);

                    if (reader.Name == "lightMap")
                        material.SetTexture(Material.TexType.lightTexture, tmpTex);
                }
                reader.MoveToElement();
            }

            Shader shader = Shaders.ShaderLoader.GetInstance().GetShader(shaderName);

            return new Material();
        }
    }
}
