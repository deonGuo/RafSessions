using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafSessions
{
    public class Routines
    {
        public static List<Exercise> ChestWorkout()
        {
            List<Exercise> chestworkout = new List<Exercise>()
            {
                new Exercise("Get Ready boys.  Starting in 1 minute from now", 60),
                new Exercise("Plyo Release pushups", 25),
                new Exercise("rest", 15),
                new Exercise("Prowler pushups", 25),
                new Exercise("rest", 15),
                new Exercise("Decline pushups", 25),
                new Exercise("rest", 15),
                new Exercise("Lateral pushups", 25),
                new Exercise("rest", 15),
                new Exercise("Shoulder Tap pushups", 25),
            };
            return chestworkout;
        }

        public static List<Exercise> FullBodyWorkout()
        {
            List<Exercise> fullBody = new List<Exercise>()
            {
                new Exercise("Get Ready boys.  Starting in 1 minute from now", 60),
                new Exercise("Single Leg Box", 60),
                new Exercise("1 and 1/2 Bottomed Out Squats", 60),
                new Exercise("Jump Squats", 60),
                new Exercise("Body Weight Push away", 60),
                new Exercise("Rotational Pushups", 60),
                new Exercise("Cobra Pushups", 60),
                new Exercise("Single Leg Heel Touch Squats", 60),
                new Exercise("Sprinter Lunges", 60),
                new Exercise("Jump Sprinter Lunges", 60),
                new Exercise("rest", 60),
                new Exercise("Push ups", 60),
                new Exercise("Body weight Sliding Pulldowns", 60),
                new Exercise("Push ups", 60),
                new Exercise("Reverse Corkscrews", 60),
                new Exercise("Black Widow Knee Slides", 60),
                new Exercise("Levitation Crunches", 60),
                new Exercise("Angels and Devils", 60)


            };
            return fullBody;
        }

    }
}
