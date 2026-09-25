namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// Class that deals with all the heading, dividers and general displays to the user. It also does the typing effect and reading of user input. Every other class
    /// sends its output through this class, so the styling stays consistent and can be changed in one single place.
    /// </summary>
    internal class ConsoleDisplayManager
    {
        /// <summary>
        /// This variable determines the width of the borders and dividers. I set it so that it matches the width of the ASCII logo so the interface are in line.
        /// </summary>
        private const int Width = 83;

        /// <summary>
        /// This method prints a titled banner with a border above and below it. It is used at the start of the welcome or goodbye section.
        /// </summary>
        /// <param name="title">The section name, display in upper case.</param>
        public void SectionHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', Width));
            Console.WriteLine($"  {title.ToUpper()}");
            Console.WriteLine(new string('=', Width));
            Console.ResetColor();
            Console.WriteLine();
        }
        /// <summary>
        /// Prints the closing border to show the end of a section.
        /// </summary>
        public void SectionFooter()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', Width));
            Console.ResetColor();
            Console.WriteLine();
        }
        /// <summary>
        /// This method prints a thin line to separate one exchange from the next, keeping the conversation easy to follow.
        /// </summary>
        public void Divider()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string('-', Width));
            Console.ResetColor();
        }

        /// <summary>
        ///  This method is used to write a message one character at a time so it seems like the chatbot is typing the reply. This is done by having  default delay of 
        ///  15 milliseconds. The delay can be changed by the caller to another number. It is set to private as it only a helper for the BotReply.
        /// </summary>
        /// <param name="message">The text that must be typed out.</param>
        /// <param name="delayMs">The length of the pass between the characters in milliseconds.</param>
        private void ResponseDelay(string message, int delayMs = 15)
        {
            foreach (char m in message)
            {
                Console.Write(m);
                Thread.Sleep(delayMs);
            }

        }

        /// <summary>
        /// Changes the colour of the bot responses and calls the delay method to print out console.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public void BotReply(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Bot: ");
            ResponseDelay(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Colour and calling of a warning message from the Bot. Warning messages are set to the colour red and there is no typing effect. The message is displayed 
        /// immediately.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public void BotWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Bot: ");
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Ask the user for input
        /// </summary>
        /// <param name="message">The prompt shown before the input cursor.</param>
        /// <returns> It returns the text entered by the user. </returns>
        public string AskUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{message}: ");
            Console.ResetColor();
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Showing the logo to the console in the given colour. It also skips the typing effect to show the art immediately.
        /// </summary>
        /// <param name="art">The art to be displayed.</param>
        /// <param name="colour">The colour the art must be displayed in.</param>
        public void ShowBanner(string art, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(art);
            Console.ResetColor();
        }

    }
}
