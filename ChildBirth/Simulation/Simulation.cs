using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected
        List<SimObject> objects = new List<SimObject>();

        /// <summary>
        /// Virtual method to be overriden in the subclasses
        /// </summary>
        public virtual void Simulate()
        {
            
        }

        protected void Update(double time)
        { 
            foreach(SimObject simObject in objects)
            {
                simObject.Update(time);
            }
        }
    }
}
