using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using ChildBirth.SimSystem;

namespace ChildBirth.Cameras
{
    class CameraOrbit : Camera
    {
        private float xDelta = 0.1f;
        private float yDelta = 0.1f;

        private float speed = 0.1f;

        public void UpdateMouse()
        {
            if (MouseManager.GetInstance().IsMouseLeftDown())
            {
                Origin = Origin + Up * yDelta * ySens;
                Origin = Origin + Right * xDelta * xSens;
            }

            mouseXOld = MouseManager.GetInstance().X;
            mouseYOld = MouseManager.GetInstance().Y;
        }

        private void MoveByVector(Vector3 vector)
        {
            Eye = Eye + vector * speed;
            Origin = Origin + vector * speed;
        }

        public override void Update(double time)
        {
            if (KeyboardManager.GetInstance().W)
            {
                MoveByVector(Forward);
            }
            else if (KeyboardManager.GetInstance().S)
            {
                MoveByVector(-Forward);
            }
            else if (KeyboardManager.GetInstance().D)
            {
                MoveByVector(Right);
            }
            else if (KeyboardManager.GetInstance().A)
            {
                MoveByVector(-Right);
            }
            else if (KeyboardManager.GetInstance().LControl)
            {
                MoveByVector(Up);
            }
            else if (KeyboardManager.GetInstance().Space)
            {
                MoveByVector(-Up);
            }

            UpdateMouse();

            base.Update(time);
        }


        public override void UpdateViewMatrix()
        {
            this.view = Matrix4.LookAt(eye, origin, up);
        }

        public override void UpdateProjMatrix()
        {
            this.projection = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
        }
    }
}