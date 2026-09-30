
namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// The chatbot logic. This class takes the user's input and normalises it and returns a reply. No interface code here.
    /// </summary>
    internal class ChatBot
    {

        private readonly InputValidator _validator = new();
        private readonly BotResponses _botResponses = new();
        private readonly SentimentAnalyser _sentiment = new();

        /// <summary>
        /// The user's details, it is public so that the window can read the name for message labels and set it when the user first enters it.
        /// </summary>
        public UserProfile User { get; } = new();

        /// <summary>
        /// Keywords to end the conversation
        /// </summary>
        private static readonly string[] ExitInput = { "exit", "quit", "bye", "end", "good bye", "goodbye" };

        /// <summary>
        /// Words/phrases that prompt the chatbot to continue with the topic already being spoken about
        /// </summary>
        private static readonly string[] FollowUpPhrases = { "tell me more", "another tip", "more tips", "explain more", "go on", "what else", "more info", "tell me another", "anything else" };

        /// <summary>
        /// Takes in the user input and then returns the bot's reply.
        /// </summary>
        /// <param name="userInput">Raw text the user entered</param>
        /// <returns>Chatbot reply that is ready to be displayed in the chat</returns>
        public string GetReply(string userInput)
        {

            string input = _validator.NormaliseInput(userInput);

            string? matched = _botResponses.FindTopic(input);

            // A farewell is answered with a closing message rather than a tip. The window stays open so the user can carry on if they want to.
            if (ExitInput.Contains(input))
            {
                return $"Stay safe out there, {User.Name}! You asked {User.QuestionsAsked} question(s) today.";
            }

            // A follow-up question carries on with whatever topic was last spoken about
            if (FollowUpPhrases.Any(phrase => input.Contains(phrase)) && matched == null)
            {
                User.QuestionsAsked++;

                if (User.CurrentTopic != null)
                {
                    return _botResponses.GetResponseMatch(User.CurrentTopic);
                }

                return $"Happy to say more, {User.Name}, but which topic would you like? You can ask me about passwords, phishing, safe browsing, privacy, " +
                       "public Wi-Fi, scams, links in emails or app permissions.";
            }

            // Increase question counter
            User.QuestionsAsked++;

            // The user is telling the chatbot which topic interests them, so store it and answer with a tip straight away.
            if (input.Contains("interested in") || input.Contains("favourite topic"))
            {
                if (matched != null)
                {
                    User.FavouriteTopic = matched;
                    User.CurrentTopic = matched;

                    return $"Great, I'll remember that you're interested in {_botResponses.GetDisplayName(matched)}, {User.Name}. It's an important part of staying safe online.\n\n{_botResponses.GetResponseMatch(input)}";
                }
            }

            // Only updates when a topic is actually found
            if (matched != null) 
            {
                User.CurrentTopic = matched;
            }

            string reply = _botResponses.GetResponseMatch(input);

            string? topicForFeeling = matched ?? User.CurrentTopic;
            string? feeling = _sentiment.Detect(input, User.Name, topicForFeeling == null ? null : _botResponses.GetDisplayName(topicForFeeling));

            if (feeling != null)
            {
                if (matched == null)
                {
                    // The brief asks that confusion continues the current topic
                    if (User.CurrentTopic != null)
                    {
                        return feeling + "\n\n" + _botResponses.GetResponseMatch(User.CurrentTopic);
                    }

                    // Nothing discussed yet
                    return feeling + "\n\nWhat's on your mind? I can help with passwords, phishing, " +
                           "safe browsing, privacy, public Wi-Fi, scams, links in emails or app permissions.";
                }

                reply = feeling + "\n\n" + reply;
            }

            // Every third question, refer back to the topic the user said they care about.
            if (User.FavouriteTopic != null && matched != null && matched != User.FavouriteTopic && User.QuestionsAsked % 3 == 0)
            {
                reply += $"\n\nBy the way {User.Name}, as someone interested in {_botResponses.GetDisplayName(User.FavouriteTopic)}, it's worth reviewing your {_botResponses.GetDisplayName(User.FavouriteTopic)} habits regularly too.";
            }
            return reply;
        }
    }
}

