namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// Class where the Logo Display Art is stored and the logo is displayed at the start of the application.
    /// </summary>
    internal class LogoArt
    {
        /// <summary>
        /// The logo uses box drawing characters, which is why the console output encoding is set to UTF-8 before it is displayed.
        /// Art work is the title with a small shield next to it. A raw string is used so that backslashes don't need to be added.
        /// </summary>
        private const string Logo = """
            ╔═╗┬ ┬┌┐ ┌─┐┬─┐┌─┐┌─┐┌─┐┬ ┬┬─┐┬┌┬┐┬ ┬  ╔═╗┬ ┬┌─┐┬─┐┌─┐┌┐┌┌─┐┌─┐┌─┐  ╔╗ ┌─┐┌┬┐  /\
            ║  └┬┘├┴┐├┤ ├┬┘└─┐├┤ │  │ │├┬┘│ │ └┬┘  ╠═╣│││├─┤├┬┘├┤ │││├┤ └─┐└─┐  ╠╩╗│ │ │  |<>|
            ╚═╝ ┴ └─┘└─┘┴└─└─┘└─┘└─┘└─┘┴└─┴ ┴  ┴   ╩ ╩└┴┘┴ ┴┴└─└─┘┘└┘└─┘└─┘└─┘  ╚═╝└─┘ ┴   \/
            """;

        private readonly ConsoleDisplayManager _ui;

        /// <summary>
        /// Constructor takes the shared display manager so the logo is printed through the same class as other console output.
        /// </summary>
        /// <param name="ui">The display manager used to print the art to the console.</param>
        public LogoArt(ConsoleDisplayManager ui)
        {
            _ui = ui;
        }

        /// <summary>
        /// Method is used to display the logo as the chatbot title screen, after the voice greeting has finished playing. 
        /// </summary>
        public void DisplayLogo()
        {
            _ui.ShowBanner(Logo, ConsoleColor.Green);
        }




    }
}
