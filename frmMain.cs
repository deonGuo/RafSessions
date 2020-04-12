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

namespace RafSessions
{
    public partial class frmMain : Form
    {
        SpeechSynthesizer speech;
        int h, m, s, s60, itemNumber;
        List<string> program;
        public frmMain()
        {
            InitializeComponent();
            program = new List<string>{
                                "Get Ready.  Starting in 1 minute from now",
                                "Single Leg Box						",
                                "one and half Bottomed Out Squats   ",
                                "Jump Squats                        ",
                                "Handstand Pushups                  ",
                                "Rotational Pushups                 ",
                                "Cobra Pushups                      ",
                                "Single Leg Heel Touch Squats       ",
                                "Sprinter Lunges                    ",
                                "Jump Sprinter Lunges               ",
                                "Pullups OR Push ups                ",
                                "Body weight Sliding Pulldowns      ",
                                "Inverted Chin Curls or Push ups    ",
                                "Reverse Corkscrews                 ",
                                "Black Widow Knee Slides            ",
                                "Levitation Crunches                ",
                                "Angels and Devils                  "
              };
            //set the initial value of nextitem to be the first entry of the program
            this.speech = new SpeechSynthesizer();
            speech.SelectVoice("Microsoft David Desktop");
            h = 0;
            m = 0;
            s = 0;
            s60 = 60;
            itemNumber = 0;
            //set the program text box to display exercises
            foreach(string exercise in program)
            {
                txtProgram.Text += exercise.Trim() + "\r\n";
            }
            //show installed voices
            txtDebug.Text = "Intalled voices: " + speech.GetInstalledVoices().Count + "\r\n";
            foreach (InstalledVoice voice in speech.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                txtDebug.Text += ("Name: " + info.Name + "\r\n");
            }
            txtDebug.Text += "Selected Voice: " + speech.Voice.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //setting the time increase
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
            if (s60 == 0)
            {
                speech.SpeakAsync("NOW " + program[itemNumber]); //announces "NOW" instead of 0
                s60 = 60;
            }
            //every minute when it reaches 10, it reads out the next line
            if(s60 == 10)
            {
                itemNumber++;  //because each exercise is 60sec no break; move to next exercise every minute
                speech.SpeakAsync("in 10 seconds: " + program[itemNumber]);
                //nextItem = txtProgram.Text.Substring();
                //speech.SpeakAsync();
            }
            if(s60 <= 5 && s60 > 0)  //count down for the last 5 seconds
            {
                speech.SpeakAsync(s60.ToString());
            }
            //display the time count down
            lblTimer.Text = string.Format("{0} : {1} : {2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            lblMinCountDown.Text = s60.ToString();

            //All operations for this tick are completed.  Now move to next tick
            s++;
            s60--;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            speech.SpeakAsync(program[0]);
        }
    }
}
