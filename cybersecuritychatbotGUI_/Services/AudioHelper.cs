using System;
using System.IO;
using System.Media;
using System.Windows;

namespace CyberSecurityChatbotGUI.Services
{
    class AudioHelper
    {
        public static void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Assets",
                    "greeting.wav");

                if (!File.Exists(path))
                {
                    MessageBox.Show("File not found:\n" + path);
                    return;
                }

                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = path;
                player.Load();

                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}