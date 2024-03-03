using System;
using System.Linq;

/*namespace text_to_speech
{
  public partial class Form1 : Form
  {
    SpeechSynthesizer reader;
    public string RemoveWhitespace(string input)
    {
      return new string(input.ToCharArray()
          .Where(c => !Char.IsWhiteSpace(c))
          .ToArray());
    }

    void Say(string voiceName, string words)
    {
      reader.SelectVoice("Microsoft " + voiceName + " Desktop");
      foreach (var word in words.Split(','))
      {
        //reader.SetOutputToDefaultAudioDevice();
        //reader.Speak(word);
        reader.SetOutputToWaveFile(string.Format(@"D:\Workspace\Voice\{0}\{1}.wav", voiceName, RemoveWhitespace(word)));
        reader.Speak(word);
      }
    }

    void Say(string words)
    {
      Say("Zira", words);
      Say("David", words);
    }

    public Form1()
    {
      InitializeComponent();

      reader = new SpeechSynthesizer();
      reader.Rate = 0;


      Say("Nice, Good, Very Good, Great, Excellent, Godlike, Wonderful, Awesome, Perfect");

      reader.Rate = -1;
      Say("All Your Base Belong To Us");

      Say("Health Restored, Ship Damage Repaired, Critical Condition");
      Say("Combo Lost, Laser, Shield, Nuke, Energy, Anti Proton Pulsar, Weapon Upgrade, Enemy Destroyed");
      Say("Mission Begins, Mission Report, Mission Accomplished, Mission Failed, Game Over");
      Say("Yes, No");
      Say("Danger Eliminated");
      Say("Explosion Imminent");
      Say("Resource Extracted, Orichalchum Extracted, Orichalchum Mined, Infinitum Mined, Chronothium 192 Mined, Mined, Mining Machinery Deployed");
      Say("1st, 2nd, 3rd, 4th, 5th, 6th, 7th, 8th, 9th, 10th, 11th, 12th, 13th, 14th, 15th, 16th, 17th, 18th, 19th, 20th, 30th, 40th, 50th, 60th, 70th, 80th, 90th, Hundredth, Thousandth, Millionth, Billionth");
      Say("1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 30, 40, 50, 60, 70, 80, 90, Hundred, Thousand, Million, Billion");
  // Then audacity 

    }


  }
}
*/