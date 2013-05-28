using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBirth
{
    class Settings
    {
        private static Settings instance = null;

        public static Settings GetInstance()
        {
            if (instance == null)
                instance = new Settings();

            return instance;
        }

        private Settings() { }

        public String ContentDirectory = "Content/";
    }
}
