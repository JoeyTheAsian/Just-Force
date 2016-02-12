using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Tools {
    class FPSHandling {
        //stores samples of the frame rates for the past 3 seconds
        protected Queue<int> FPSSample = new Queue<int>(5);
        //the printed fps on screen
        protected int avgFPS = 0;
        public int frames = 0;

        public int AvgFPS {
            get {
                return avgFPS;
            }
        }
        public FPSHandling() {
            Console.WriteLine("FPS Recording Started.");
        }
        
        public string UpdateFPS() {
            avgFPS = (int)FPSSample.Average();
            return "FPS: " + avgFPS;
        }
        public void AddSample(int s) {
            FPSSample.Enqueue(s);
        }
    }
}
