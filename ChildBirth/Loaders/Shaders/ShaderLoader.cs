using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml;

using ChildBirth.Rendering;

namespace ChildBirth.Loaders.Shaders
{
    class ShaderLoader : Loader
    {
        #region Singleton declaration
        private static ShaderLoader instance = null;

        public static ShaderLoader GetInstance()
        {
            if (instance == null)
                instance = new ShaderLoader();
            return instance;
        }

        private ShaderLoader()
        { 
            
        }

        #endregion

        /// <summary>
        /// The shader
        /// </summary>
        /// <param name="name">The name of the shader to be returned</param>
        /// <returns></returns>
        public Shader GetShader(String name)
        {
            return (Shader) base.GetObject(name);
        }

        protected override ContentObject Load(String name)
        {
            try
            {
                // Construct the full relative URI of the file to be loaded and crop the file extension
                String uri = ConstructURIAndRefineName(ref name);

                Shader shader = new Shader();

                XmlTextReader reader = new XmlTextReader(uri);
                while (reader.Read())
                {
                    if (reader.Name == "vertex")
                        shader.VShader = reader.ReadString();

                    else if (reader.Name == "fragment")
                        shader.FShader = reader.ReadString();
                }

                shader.Initialize();

                library.Add(shader);
                return (ContentObject)shader;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
                Console.Error.WriteLine("Failed to load shader with name: " + name);
                Console.Error.WriteLine("Using the default shader");
                
                return (ContentObject) new Rendering.Defaults.DefaultShader();
            }
        }

        /// <summary>
        /// String to indicate the directory where the shader files are stored
        /// It is used along with the global content directory from Settings class
        /// </summary>
        protected override string ContentSubDirectory { get { return "Shaders/"; } }
        protected override string FileExtension { get { return ".shad"; } }
    }
}
