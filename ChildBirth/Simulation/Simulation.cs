using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ChildBirth.Cameras;

namespace ChildBirth.Simulation
{
    /// <summary>
    /// Base class for all the simulations to be implemented in the system
    /// </summary>
    class Simulation
    {
        /// <summary>
        /// List of all the object in this simulation
        /// </summary>
        protected List<SimObject> objects = new List<SimObject>();

        /// <summary>
        /// Virtual method to be overriden in the subclasses
        /// </summary>
        public virtual void Simulate()
        {
            
        }

        /// <summary>
        /// An update method to update all the objects in the simulation
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(double time)
        { 
            foreach(SimObject simObject in objects)
            {
                simObject.Update(time);
            }
        }

        /// <summary>
        /// Public property to retreive the main camera used in the simulaiton
        /// </summary>
        public Camera MainCamera
        {
            get {
                    Camera camera = (Camera) GetObject("MainCamera");
                    if (camera == null)
                    {
                        camera = new CameraOrbit();
                        camera.Name = "MainCamera";
                        objects.Add(camera);    
                    }
                    
                    return camera;
                }
        }

        public SimObject GetObject(String name)
        {
            foreach (SimObject simObject in objects)
            {
                if (simObject.Name == name)
                    return simObject;
            }
            return null;
        }

        internal void Load()
        {
            Rendering.Model model = new Rendering.Model("Baby", "Phong");
            objects.Add(model);
        }

        internal void Render()
        {
            objects.ForEach( obj => { obj.Render(); } );
        }
    }
}
