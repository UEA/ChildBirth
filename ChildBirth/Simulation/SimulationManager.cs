using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChildBirth.Simulation
{
    class SimulationManager
    {
        private List<Simulation> simulations = new List<Simulation>();

        private static SimulationManager instance = null;
        public static SimulationManager Instance
        {
            get
            {
                if (instance == null) instance = new SimulationManager();
                return instance;
            }
        }

        private SimulationManager()
        { 
            
        }

        private Simulation currentSimulation;
        public Simulation CurrentSimulation
        {
            get { return this.currentSimulation;  }
            set { this.currentSimulation = value; }
        }

        public void AddSimulation(Simulation sim)
        {
            simulations.Add(sim);
            currentSimulation = sim;
        }

        internal void Load()
        {
            foreach (Simulation sim in simulations)
            {
                sim.Load();
            }
        }

        internal void Render()
        {
            currentSimulation.Render();
        }
    }
}
