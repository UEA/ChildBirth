using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChildBirth.Loaders
{
    /// <summary>
    /// Base content object class
    /// </summary>
    class ContentObject
    {
        /// <summary>
        /// Private field and public property to store and access the name of the content object
        /// </summary>
        private String name;
        public String Name
        {
            get { return this.name;  }
            set { this.name = value; }
        }
    }
}
