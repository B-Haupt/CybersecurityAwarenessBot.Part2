namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// Class purpose is to store the chatbot predefined responses and to match the user input against the correct one. I used a dictionary to store the topics in
    /// so that new entries can be added without changing any of the logic.
    /// </summary>
    internal class BotResponses
    {
        /// <summary>
        ///  The dictionary maps a keyword to its response. The keys are stored in lower case as I normalize the input from the user. I have placed the longer 
        ///  multi-word keys first so that general questions are not matched by single topic words appearing inside them.
        /// </summary>
        private readonly Dictionary<string, string> _responses = new()
        {
            // Key = Value (wifi and wi-fi is duplicated so that if you spell it as wi-fi you still get the safety tip)
            ["how are you"] = "I am good. Thank you for asking.",
            ["your purpose"] = "My purpose is to provide you with safety tips to help you navigate the dangers online.",
            ["what can i ask"] = "You can ask me about password, phishing and browsing." + "\nI can also offer tips about public wifi, online scams, links in emails and app permissions.",
            ["password"] = "Use a unique, long passphrase for every account and protect them all with multi-factor authentication (MFA).",
            ["phishing"] = "Check the sender's actual email address carefully and never click links or download attachments from unexpected messages.",
            ["browsing"] = "Before entering sensitive data, always check that the website URL starts with \"https\" and has the padlock icon to ensure your connection is fully encrypted.",
            ["wifi"] = "Public Wifi networks lack strong encryption. This allows hackers to intercept your data, steal passwords, or spread malware.",
            ["wi-fi"] = "Public Wifi networks lack strong encryption. This allows hackers to intercept your data, steal passwords, or spread malware.",
            ["scam"] = "To protect yourself from online scams, always pause, stay calm, and independently verify any unexpected messages or requests before sharing personal information or clicking links.",
            ["link"] = "Do not follow any links in emails to reach Internet banking websites. Malicious software could redirect the link to a fake site.",
            ["permission"] = "Review app permissions before installing an application. For example it doesn't make sense for a torch app to need your contacts or location."
        };

        /// <summary>
        /// Created a default response in case the user's input has no match. It also tells the users what available topics are available to ask the chatbot.
        /// </summary>
        private readonly string _defaultResponse = "I didn't quite understand. Could you please rephrase the question?"
            + "\nYou can ask me about password safety, phishing or safe browsing."
            + "\nI can also offer tips about public Wifi, online scams, links in emails and app permissions.";


        /// <summary>
        /// This method searches the dictionary for the first keyword contained in the user's input and then returns a matching response.
        /// </summary>
        /// <param name="input">This is the user's message, which is already trimmed and converted to lower case by the InputValidator</param>
        /// <returns>This method returns the response for the first matching keyword, or the default response if no keyword is found</returns>
        public string GetResponseMatch(string input)
        {
            // Check each keyword in turn and return as soon as one is found 
            foreach (var item in _responses)
            {
                if (input.Contains(item.Key))
                {
                    return item.Value;
                }
            }
            // if nothing is found then it returns the default response
            return _defaultResponse;
        }

    }
}
