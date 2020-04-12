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
        public frmMain()
        {
            InitializeComponent();
            this.speech = new SpeechSynthesizer();
            speech.SelectVoice("Microsoft David Desktop");

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

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string speechContent = textBox1.Text;
            label1.Text = speech.Voice.Id + ", " + speech.Voice.Description;

            if(speechContent == "")
            {
                speech.SpeakAsync("I have nothing to say");
            }
            else
            {
                speech.SpeakAsync(speechContent);
            }

        }
    }
}
