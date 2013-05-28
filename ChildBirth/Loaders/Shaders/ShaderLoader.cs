using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ChildBirth.Rendering;

namespace ChildBirth.Loaders.Shaders
{
    class ShaderLoader
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

        private List<Shader> library = new List<Shader>();

        private String ShaderDirectory = "Shaders/";

        public Shader GetShader(String name)
        {
            foreach (Shader shader in library)
            {
                if (shader.Name == name)
                    return shader;
            }

            return LoadShader(name);
        }

        private Shader LoadShader(String name)
            {
                String uri = Settings.GetInstance().ContentDirectory + name;

                if(!uri.EndsWith(".shad"))
                {
                    uri += ".shad";
                }

                Shader shader = new Shader();

                return shader;
            }
    }
}
