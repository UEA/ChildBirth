using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using OpenTK.Input;

namespace ChildBirth.SimSystem
{
    class MouseManager
    {
        private static MouseManager instance;
        public static MouseManager GetInstance()
        {
            if (instance == null)
                instance = new MouseManager();
            return instance;
        }

        private MouseDevice mouse;
        public MouseDevice Mouse
        {
            get { return this.mouse; }
            set { this.mouse = value; }
        }

        public Vector2h Position2
        {
            get { return new Vector2h(mouse.X, mouse.Y); }
        }

        public int X
        {
            get { return mouse.X; }
        }

        public int Y
        {
            get { return mouse.Y; }
        }

        public bool IsMouseLeftDown()
        {
            return this.mouse[MouseButton.Left];
        }

        public bool IsMouseRightDown()
        {
            return this.mouse[MouseButton.Right];
        }
    }
}
