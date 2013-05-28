using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBirth.Loaders
{
    /// <summary>
    /// Base content loader class
    /// </summary>
    abstract class Loader
    {
        protected List<ContentObject> library = new List<ContentObject>();

        protected ContentObject GetObject(String name)
        {
            foreach (ContentObject contentObject in library)
            {
                if (contentObject.Name == name)
                    return contentObject;
            }

            return Load(name);
        }

        protected virtual ContentObject Load(String name);

        protected virtual String FileExtension
        {
            get { return ""; }
        }

        protected virtual String ContentSubDirectory
        {
            get;
        }

        protected String ConstructURIAndRefineName(ref String name)
        {
            if(name.Contains(FileExtension))
            {
                name.Replace(FileExtension, "");
            }

            return Settings.GetInstance().ContentDirectory + ContentSubDirectory + name + FileExtension;
        }
    }
}
