using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Input;

namespace ChildBirth.SimSystem
{
    class KeyboardManager
    {
        KeyboardDevice keyboard;

        private static KeyboardManager instance = null;
        public static KeyboardManager GetInstance()
        {
            if (instance == null)
                instance = new KeyboardManager();
            return instance;
        }

        public KeyboardDevice Keyboard
        {
            get { return this.keyboard;  }
            set { this.keyboard = value; }
        }
        
        public bool W
        {
            get { return this[Key.W]; }
        }

        public bool A
        {
            get { return this[Key.A]; }
        }

        public bool S
        {
            get { return this[Key.S]; }
        }

        public bool D
        {
            get { return this[Key.D]; }
        }

        public bool Space
        {
            get { return this[Key.Space]; }
        }

        public bool LControl
        {
            get { return this[Key.LControl]; }
        }

        public bool this[Key key]
        {
            get { return keyboard[key]; }
        }
    }
}
