using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Speech.Synthesis;

namespace 点名器
{
    class Speaker
    {
        SpeechSynthesizer speaker;
        VoiceInfo vi;
        int rate, volume;
        public Speaker()
        {
            speaker = new SpeechSynthesizer();
            rate = 0;
            volume = 0;
            speaker.Rate = rate;
            speaker.Volume = volume;
        }
        public void Speak(string words)
        {
            speaker.SpeakAsync(words);
        }
        public void GetVoiceLibNames()
        {
            
        }
    }
}
