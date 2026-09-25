namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// This class is used to check and clean up the input that is coming from the user. I put this in its own class so that the validation rules live in one
    /// place instead of being repeated everywhere when the user is giving input to the program.
    /// </summary>
    internal class InputValidator
    {
        /// <summary>
        /// Check the user has input something usable and returns false for null, empty or whitespace. I used string.IsNullOrWhiteSpace as it catches input that is only spaces.
        /// </summary>
        /// <param name="input">The text entered by the user.</param>
        /// <returns>False if the input is null, empty or whitespace.</returns>
        public bool IsValidInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Trims the extra space and then puts it to lowercase. This is done for the response lookup so that if the user types a word in all caps it would still match
        /// the response list.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <returns>The trimmed, lowercase version of the input.</returns>
        public string NormaliseInput(string input)
        {
            return input.Trim().ToLower();
        }

    }
}
