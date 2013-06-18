using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

        protected abstract ContentObject Load(String name);

        
        protected virtual String FileExtension { get { return ""; } }
        protected virtual String ContentSubDirectory { get { return ""; } }

        /// <summary>
        /// Combines the content directory, the subdirectory and the filename with the extension
        /// to form a full URI to access the file of the content object
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <returns>The full URI as a string</returns>
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
