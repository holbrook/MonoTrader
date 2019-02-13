using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using TangleTrading.Command;

namespace TangleTrading.Framework
{
    class SpeakerActor : ReceiveActor
    {
        SpeechSynthesizer speaker;

        public SpeakerActor()
        {
            speaker = new SpeechSynthesizer();
            speaker.Volume = 100;  //设置朗读音量 [范围 0 ~ 100] 
            speaker.Rate = 0;      //设置朗读频率 [范围  -10 ~ 10] 
            speaker.SelectVoice("Microsoft Lili");  //SpeakChina
            //speaker.SelectVoice("Microsoft Anna");  //SpeakEnglish
            speaker.SelectVoiceByHints(VoiceGender.Male, 
                VoiceAge.Child, 2, System.Globalization.CultureInfo.CurrentCulture);

            Receive<SpeechCommand>((cmd) =>
            {
                ExecuteSpeech(cmd);
            })
        }

        private void ExecuteSpeech(SpeechCommand cmd)
        {
            speaker.Volume = cmd.Volume;  //设置朗读音量 [范围 0 ~ 100] 
            speaker.Rate = cmd.Rate;      //设置朗读频率 [范围  -10 ~ 10] 

            // 同步朗读
            //speaker.Speak(txt.Text.Trim());
            //speaker.Dispose();  //释放之前的资源

            //异步朗读
            speaker.SpeakAsync(cmd.Message.Trim());            
        }
    }
}
