using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// Private list storage for all the shaders that have been loaded
        /// </summary>
        private List<Shader> library = new List<Shader>();

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
            // Construct the full relative URI of the file to be loaded
            String uri = ConstructURIAndRefineName(ref name);

            Shader shader = new Shader();

            return (ContentObject) shader;
        }

        /// <summary>
        /// String to indicate the directory where the shader files are stored
        /// It is used along with the global content directory from Settings class
        /// </summary>

        protected override string ContentSubDirectory
        {
            get
            {
                return "Shaders/";
            }
        }

        protected override string FileExtension
        {
            get
            {
                return ".shad";
            }
        }
    }
}
