using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChildBirth.Loaders;

namespace ChildBirth.Rendering
{
    class Texture : ContentObject
    {
        private int id;
        public int Id
        {
            get { return this.id;  }
            set { this.id = value; }
        }
    }
}
