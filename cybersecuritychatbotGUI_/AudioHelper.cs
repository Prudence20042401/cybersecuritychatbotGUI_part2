using System.Media;
using System.IO;
using System.Windows;

namespace CyberSecurityChatbotGUI
{
    class AudioHelper
    {
        // method used to play greeting audio
        public static void PlayGreeting()
        {
            try
            {
                string path = Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "greeting.wav"
                );

                SoundPlayer player = new SoundPlayer(path);

                player.Load();

                player.Play();
            }

            catch
            {
                MessageBox.Show("Could not play audio.");
            }
        }
    }
}