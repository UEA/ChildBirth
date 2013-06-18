using System;
using System.Diagnostics;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using ChildBirth.Simulation;

namespace ChildBrith
{
    public class ChildBirthSim : GameWindow
    {
        //////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public ChildBirthSim()
            : base(640, 480,
            new GraphicsMode(), "ChildBirth Simulation System")
        { }

        protected override void OnLoad(System.EventArgs e)
        {
            ChildBirth.SimSystem.MouseManager.GetInstance().Mouse = Mouse;

            SimulationManager.Instance.AddSimulation(new Simulation());
            SimulationManager.Instance.Load();

            VSync = VSyncMode.On;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[OpenTK.Input.Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SimulationManager.Instance.Render();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        [STAThread]
        public static void Main()
        {
            using (ChildBirthSim example = new ChildBirthSim())
            {
                example.Title = "Childbirth Sim - UEA";
                example.Run(30.0, 30.0);
            }
        }
    }
}