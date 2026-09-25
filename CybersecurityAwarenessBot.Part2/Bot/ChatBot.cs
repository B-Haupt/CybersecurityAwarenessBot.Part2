namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// This class controls the chatbot session from beginning to end. It creates the other classes, and then runs the start up order in terms of the voice greeting, logo
    /// appearing, personalized welcome message. This class also contains the conversation loop which keeps the loop running and then ends the conversation if the user
    /// want to exit.
    /// </summary>
    internal class ChatBot
    {
        /// <summary>
        /// The Chatbot class creates one shared display manager and then passed on to the other classes that produce the output. I did this so that all the console
        /// formatting is done in one place and that it can all be changed in one place.
        /// </summary>
        private readonly ConsoleDisplayManager _ui = new();
        private readonly LogoArt _logo;
        private readonly GreetingPlayer _player;
        private readonly InputValidator _validator = new();
        private readonly BotResponses _botResponses = new();
        private readonly UserProfile _user = new();

        /// <summary>
        /// This is a static readonly list that contains the words that the user can use to exit the program.
        /// </summary>
        private static readonly string[] ExitInput = { "exit", "quit", "bye", "end", "good bye", "goodbye" };

        /// <summary>
        /// This is the ChatBot Constructor that builds the classes that are need for the display manager to be passed into.
        /// </summary>
        public ChatBot()
        {
            _player = new GreetingPlayer(_ui);
            _logo = new LogoArt(_ui);
        }


        /// <summary>
        /// RunChatBot is a void method as it starts the chatbot. It is also the only public method so that Program.cs can have a single entry point into the program.
        /// </summary>
        public void RunChatBot()
        {
            // Ensure art is rendering in UTF-8 for logo
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Cybersecurity Awareness Bot";

            // Calling methods - it's important that they are called in the correct order
            _player.PlayGreeting();
            _logo.DisplayLogo();
            HelloToUser();
            ConversationLoop();
            Goodbye();

        }

        /// <summary>
        /// Method that asks for the user name and then greets them personally, as well as display the available topics. It also keeps asking the user for input until a name is entered.
        /// </summary>
        private void HelloToUser()
        {
            _ui.SectionHeader("Welcome");


            // Keep asking the user for their name as they can't use a blank space as their name 
            string userName = _ui.AskUser("Please input your name");
            while (!_validator.IsValidInput(userName))
            {
                _ui.BotWarning("I didn't get your name. Please re-enter it for me.");
                userName = _ui.AskUser("What is your name?");
            }
            // Only used Trim to get rid of any extra white space on their name.
            _user.Name = userName.Trim();

            _ui.BotReply($"Welcome, {_user.Name}! I'm a Cybersecurity Awareness Bot. My goal is to keep you safe online.");
            _ui.BotReply("You can ask me about:" +
             "\n  - Password safety" +
             "\n  - Phishing" +
             "\n  - Safe browsing" +
             "\n  - Public Wifi" +
             "\n  - Online scams" +
             "\n  - Links in emails" +
             "\n  - App permissions" +
             "\nYou can also ask how I am or what my purpose is." +
             "\nType 'exit' when you want to leave.");
            _ui.SectionFooter();


        }
        /// <summary>
        /// This method runs the main conversation. The loop will continue until the user has typed in an exit word. This means the chatbot will keep answering questions
        /// until the user want to stop.
        /// </summary>
        private void ConversationLoop()
        {
            while (true)
            {
                string rawUserInput = _ui.AskUser(_user.Name);

                // This handles any blank input and will re-prompt the user for valid input
                if (!_validator.IsValidInput(rawUserInput))
                {
                    _ui.BotWarning("You didn't type anything. Ask me about passwords, phishing or safe browsing.");
                    continue;
                }

                string input = _validator.NormaliseInput(rawUserInput);

                // Break out of the conversation loop
                if (ExitInput.Contains(input))
                {
                    break;
                }

                // Increase question counter
                _user.QuestionsAsked++;
                _ui.Divider();
                _ui.BotReply(_botResponses.GetResponseMatch(input));
                _ui.Divider();

            }
        }

        /// <summary>
        /// This method displays the closing message and tells the user the number of questions they asked during the conversation.
        /// </summary>
        private void Goodbye()
        {
            _ui.SectionHeader("Goodbye");
            _ui.BotReply($"Stay safe out there, {_user.Name}! You asked {_user.QuestionsAsked} question(s) today.");
            _ui.SectionFooter();
        }


    }
}
