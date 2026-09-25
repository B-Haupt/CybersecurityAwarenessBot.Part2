using System.Media;
using System.IO;

namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// Class that calls the voice greeting when the application is started. The file is stored as WAV as System.Media.SoundPlayer only supports that format. 
    /// </summary>
    internal class GreetingPlayer
    {
        /// <summary>
        /// Stating the path to get greeting Bot file
        /// </summary>
        private const string GreetingPathway = @"Media\Bot.wav";
        private readonly ConsoleDisplayManager _ui;


        /// <summary>
        /// Constructor of Greeting Player which takes the shared display manager so any error messages are formatted the same way as the rest of the output.
        /// </summary>
        /// <param name="ui">Takes in the display manager.</param>
        public GreetingPlayer(ConsoleDisplayManager ui)
        {
            _ui = ui;
        }

        /// <summary>
        /// This method plays the voice greeting. It is written so that if the audio file is missing or can't play, that a warning message will be shown instead of the
        /// program just crashing. 
        /// </summary>
        public void PlayGreeting()
        {
            try
            {
                // Checking if the file exists and if not it shows a warning message.
                if (!File.Exists(GreetingPathway))
                {
                    _ui.BotWarning("Error: Voice Greeting was skipped - audio file not found.");
                    return;

                }

                // Creating a SoundPlayer Object to play the WAV file and "using" disposes the player automatically when the method ends
                using var player = new SoundPlayer(GreetingPathway);
                player.Load();
                // Ensure message and logo doesn't appear until the greeting is finished.
                player.PlaySync();

            }
            catch (Exception ex)
            {
                // Catches any error that may arise
                _ui.BotWarning("Error: " + ex.Message);
            }
        }
    }
}
