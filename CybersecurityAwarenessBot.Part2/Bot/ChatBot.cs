namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// The chatbot logic. This class takes the user's input and normalizes it and returns a reply. No interface code here.
    /// </summary>
    internal class ChatBot
    {

        private readonly InputValidator _validator = new();
        private readonly BotResponses _botResponses = new();

        /// <summary>
        /// The user's details, it is public so that the window can read the name for message labels and set it when the user first enters it.
        /// </summary>
        public UserProfile User { get; } = new();

        /// <summary>
        /// Keywords to end the conversation
        /// </summary>
        private static readonly string[] ExitInput = { "exit", "quit", "bye", "end", "good bye", "goodbye" };

        /// <summary>
        /// Takes in the user input and then returns the bot's reply.
        /// </summary>
        /// <param name="userInput">Raw text the user entered</param>
        /// <returns>Chatbot reply that is ready to be displayed in the chat</returns>
        public string GetReply(string userInput) { 
            
            string input = _validator.NormaliseInput(userInput);

            // A farewell is answered with a closing message rather than a tip. The window stays open so the user can carry on if they want to.
            if (ExitInput.Contains(input))
            {
                return $"Stay safe out there, {User.Name}! You asked {User.QuestionsAsked} question(s) today.";
            }
            // Increase question counter
            User.QuestionsAsked++;

            // The user is telling the chatbot which topic interest them, so store it and answer with a top straight away.
            if (input.Contains("interested in") || input.Contains("favourite topic"))
            {
                string? topic = _botResponses.FindTopic(input);

                if (topic != null)
                {
                    User.FavouriteTopic = topic;
                    User.CurrentTopic = topic;

                    return $"Great, I'll remember that you're interested in {topic}, {User.Name}. It's an important part of staying safe online.\n\n{_botResponses.GetResponseMatch(input)}";

                }
            }

            // Record which topic is being discussed so follow up questions are in line with it
            string? matched = _botResponses.FindTopic(input);
            User.CurrentTopic = matched;

            string reply = _botResponses.GetResponseMatch(input);

            // Every third question, refer back to the topic the user said they care about.
            if (User.FavouriteTopic != null && matched != null && matched != User.FavouriteTopic && User.QuestionsAsked % 3 == 0) {
                reply += $"\n\nBy the way {User.Name}, as someone interested in {User.FavouriteTopic}, it's worth reviewing your {User.FavouriteTopic} habits regularly too.";
            }

            return reply ;
        }



           
        }

    }

