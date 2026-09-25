namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// Class of User profile to store details about the current user for the length that the program is running.  Automatic properties are used rather than public fields
    /// </summary>
    internal class UserProfile
    {
        /// <summary>
        /// The user name, which is used in the welcome message, as the input prompt and in the goodbye. It is initialized to an empty string so that it is never null.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// This is a counter to keep track of how many questions the user asks the chatbot and show the user in the goodbye message. By default it is set to 0.
        /// </summary>
        public int QuestionsAsked { get; set; }
    }
}
