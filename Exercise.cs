using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafSessions
{
    public class Exercise
    {
        private string name;
        private int duration;
        public Exercise(string inputName, int inputDuration)
        {
            name = inputName;
            duration = inputDuration;
        }

        public string Name { get => name; set => name = value; }
        public int Duration { get => duration; set => duration = value; }
    }
}
