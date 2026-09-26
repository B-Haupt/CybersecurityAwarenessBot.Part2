namespace CybersecurityAwarenessBot.Bot
{

    internal class ChatBot
    {

        private readonly InputValidator _validator = new();
        private readonly BotResponses _botResponses = new();


        public UserProfile User { get; } = new();

        /// <summary>
        /// Takes in the user input and then returns the bot's reply. Contains no interface code
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public string GetReply(string userInput) { 
            
            string input = _validator.NormaliseInput(userInput);
            User.QuestionsAsked++;
            return _botResponses.GetResponseMatch(input);
        }

    }
}
