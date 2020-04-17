using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Reflection;

namespace RafSessions
{
    public partial class frmMain : Form
    {
        SpeechSynthesizer speech;
        //System.Media.SoundPlayer player;
        int h, m, s, sCountDown, itemNumber;
        List<Exercise> program;
        Exercise rest;
        Random rnd;
        enum encouragement
        {
            [Description("Excellent work. Keep it up")]
            everybody=0,
            [Description("Princess warrior Kay, you look fabulous")]
            lordDeon=1,
            //[Description("Come on Deon, move your fat ass")]
            //fatDeon=2, 
            //[Description("Oi Hamish, move your fat hairy ass")]
            [Description("Come on, work that bubble ass")]
            lordHamish =2, 
            [Description("You can do this my princess")]
            fatHamish=3,
            [Description("For the horde!")]
            dontBeAPussy=4,
            [Description("OMG you will be hotter than Beyouncee I swear")]
            fatCow=5,
            [Description("Don't stop for the starving children of Africa")]
            dontBeAPussy2 = 6,
            [Description("Woohoo almost there")]
            dontBeAPussy3 = 7,
            [Description("You are doing great, sexy pirate princess warrior")]
            lordDeon2 = 8
        };
        public frmMain()
        {
            InitializeComponent();
            //program = new List<string>{
            //                    "Get Ready boys.  Starting in 1 minute from now",
            //                    "Single Leg Box						",
            //                    "1 and 1/2 Bottomed Out Squats      ",
            //                    "Jump Squats                        ",
            //                    "Body Weight Push away              ",
            //                    "Rotational Pushups                 ",
            //                    "Cobra Pushups                      ",
            //                    "Single Leg Heel Touch Squats       ",
            //                    "Sprinter Lunges                    ",
            //                    "Jump Sprinter Lunges               ",
            //                    "Push ups                           ",
            //                    "Body weight Sliding Pulldowns      ",
            //                    "Push ups                           ",
            //                    "Reverse Corkscrews                 ",
            //                    "Black Widow Knee Slides            ",
            //                    "Levitation Crunches                ",
            //                    "Angels and Devils                  "
            //  };
            rest = new Exercise("Rest", 20);
            program = new List<Exercise> {
                new Exercise("Starting in 60 seconds.  Get ready now", 60),
                new Exercise("Swing + Switch", 40),
                rest,
                new Exercise("Tick Tock Lunges Left", 40),
                rest,
                new Exercise("Tick Tock Lunges Right", 40),
                rest,
                new Exercise("American Swing", 40),
                rest,
                new Exercise("Toe Taps", 40),
                rest,
                new Exercise("Squat + Press Left", 40),
                rest,
                new Exercise("Squat + Press Right", 40),
                rest,
                new Exercise("Plank Reach", 40),
                rest,
                new Exercise("Side Lunge Jack", 40),
                rest,
                new Exercise("Swing + Squat", 40)
            };

            //set the initial value of nextitem to be the first entry of the program
            this.speech = new SpeechSynthesizer();
            speech.Volume = 100;
            //player = new System.Media.SoundPlayer(@"C:\Users\deong\source\repos\RafSessions\workoutMusic.mp3");
            speech.SelectVoice("Microsoft David Desktop");
            h = 0;
            m = 0;
            s = 0;
            //s60 = 60;
            sCountDown = 60;
            itemNumber = 0;
            rnd = new Random();
            //set the program text box to display exercises
            foreach(Exercise exercise in program)
            {
                txtProgram.Text += exercise.Name.Trim() + "\r\n";
            }
            //set the labels
            txtCurrent.Text = "My awesome exercise routine";
            //show installed voices
            txtDebug.Text = "Intalled voices: " + speech.GetInstalledVoices().Count + "\r\n";
            foreach (InstalledVoice voice in speech.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                txtDebug.Text += ("Name: " + info.Name + "\r\n");
            }
            txtDebug.Text += "Selected Voice: " + speech.Voice.Name;


        }
  

    //the code snippet below caters for dragging borderless form
    public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            //player.Stop();
        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            //when the form loads, initiate the initial countdown timer as the first exercise duration
            sCountDown = program[0].Duration;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //display the time count down
            lblTimer.Text = string.Format("{0} : {1} : {2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            lblMinCountDown.Text = sCountDown.ToString().PadLeft(2,'0');

            //setting the time increase: second, minute, hour
            if (s == 60)
            {
                m++;
                s = 0;
            }
            if (m == 60)
            {
                h++;
                m = 0;
            }
            //s60 ticks down from 60 every minute
            if (sCountDown == 0)
            {
                itemNumber++;  //because each exercise is 60sec no break; move to next exercise every minute
                if(itemNumber == program.Count)  //check if it has finished the program
                {
                    speech.SpeakAsync("The end.  Well done!");
                    timer1.Stop(); //stop the timer
                    return;
                }
                //if there are more exercises to go
                txtCurrent.Text = program[itemNumber].Name;
                speech.SpeakAsync("NOW " + program[itemNumber].Name); //announces "NOW" instead of 0
                sCountDown = program[itemNumber].Duration; //set count down for the next exercise
            }else if(sCountDown==25 && itemNumber != 0){  //and since the 1st minute is prep only, don't give encouragement
                int len = Enum.GetNames(typeof(encouragement)).Length; //this gets the length of the enum
                int randomEncouragementNum = rnd.Next(len); //use the length of the enum to specify random number range, int smaller than len
                //retrieve the description of the enum value based on the random number
                string randomEncouragement = this.GetEnumDescription((encouragement)randomEncouragementNum);
                speech.SpeakAsync(randomEncouragement);
            }
            //every minute when it reaches 10, it reads out the next line
            else if(sCountDown == 10)
            {
                if (itemNumber < program.Count-1) //only announces next exercise if there is more exercise items to go
                {
                    speech.SpeakAsync("in 10 seconds: " + program[itemNumber+1].Name);
                }
            }
            else if(sCountDown <= 6 && sCountDown > 1)  //count down for the last 5 seconds
            {
                speech.SpeakAsync((sCountDown-1).ToString());
            }

            //All operations for this tick are completed.  Now move to next tick
            s++;
            sCountDown--;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            //player.PlayLooping();
            if (itemNumber == program.Count)
            { //when the itemNumber has reached the end of the program, i.e.: program has completed - now we want to restart the whole program again
                itemNumber = 0;
                sCountDown = program[0].Duration;  //we need to reset the 1min timer to start a new 1min cycle otherwise it would be viewed as the end of the cycle
            }
            speech.SpeakAsync(program[itemNumber].Name);
           
        }

        //help method for Enums
        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

    }
}
