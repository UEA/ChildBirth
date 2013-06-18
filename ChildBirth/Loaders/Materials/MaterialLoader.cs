using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

using ChildBirth.Rendering;
using ChildBirth.Loaders.Textures;
using ChildBirth.Loaders.Shaders;

namespace ChildBirth.Loaders.Materials
{
    class MaterialLoader : Loader
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

        private MaterialLoader()
        {}

        #endregion

        public Material GetMaterial(String name)
        {
            return (Material) base.GetObject(name);
        }

        protected override ContentObject Load(String name)
        {
            String uri = ConstructURIAndRefineName(ref name);

            XmlReader reader = XmlReader.Create(uri);
            
            Material material = new Material();
            material.Name = name;

            while (reader.Read())
            {
                /// Parsing shader data
                if (reader.Name == "material" && reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        if (reader.Name == "shader")
                            material.Shader = ShaderLoader.GetInstance().GetShader(reader.Value);

                    }
                    reader.MoveToElement();
                }

                /// Parsing textures of the material
                if (reader.Name == "textures" && reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        Texture tmpTex = TextureLoader.GetInstance().GetTexture(reader.Value);

                        if (reader.Name == "base")
                            material.SetTexture(Texture.TextureType.base1, tmpTex);

                        if (reader.Name == "base2")
                            material.SetTexture(Texture.TextureType.base2, tmpTex);

                        if (reader.Name == "envmap")
                            material.SetTexture(Texture.TextureType.envMap, tmpTex);

                        if (reader.Name == "lightMap")
                            material.SetTexture(Texture.TextureType.light, tmpTex);
                    }
                    reader.MoveToElement();
                }
            }

            library.Add(material);

            return material;
        }

        protected override string ContentSubDirectory { get { return "Materials/"; } }
        protected override string FileExtension { get { return ".mat"; } }
    }
}
