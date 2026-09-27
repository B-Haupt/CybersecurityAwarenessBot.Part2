namespace CybersecurityAwarenessBot.Bot
{
    /// <summary>
    /// Stores the chatbot's predefined responses and matches the user's input against them. A dictionary of lists is used so each topic can hold several tips, and
    /// new topics or tips can be added without changing the matching logic.
    /// </summary>
    internal class BotResponses
    {
        /// <summary>
        /// The dictionary maps each keyword to a list of responses. The keys are stored in lower case because the input is normalised before matching. Storing several
        /// responses per topic means the chatbot can pick a different tip each time, so the conversation stays varied rather than repeating the same line.
        /// </summary>
        private readonly Dictionary<string, List<string>> _responses = new()
        {
            // Key = keyword, Value = list of possible responses
            ["how are you"] = new List<string> { "I am good. Thank you for asking." },
            ["your purpose"] = new List<string> { "My purpose is to provide you with safety tips to help you navigate the dangers online." },
            ["what can i ask"] = new List<string> { "You can ask me about password, phishing and browsing." + "\nI can also offer tips about public wifi, online scams, links in emails and app permissions." },
            ["password"] = new List<string> {
                "Use a unique, long passphrase for every account and protect them all with multi-factor authentication (MFA).",
                "Never reuse a password across multiple accounts; if one site suffers a data breach, your other logins remain safe.",
                "Your password should include a blend of uppercase letters, lowercase letters, numbers, and special symbols if a system requires them."
            },
            ["phishing"] = new List<string> {
                "Check the sender's actual email address carefully and never click links or download attachments from unexpected messages.",
                "Avoid opening unexpected files, especially .zip, .exe, .js, or unverified office documents.",
                "Be wary of messages claiming dire consequences, account lockouts, or immediate action requirements."
            },
            ["browsing"] = new List<string> {
                "Before entering sensitive data, always check that the website URL starts with \"https\" and has the padlock icon to ensure your connection is fully encrypted.",
                "Turn on multi-factor authentication (MFA) to add an extra layer of protection to your accounts.",
                "Restrict site permissions and limit what data browsers and extensions can access."
             },
            ["wifi"] = new List<string> {
                "Public Wifi networks lack strong encryption. This allows hackers to intercept your data, steal passwords, or spread malware.",
                "Do not log into online banking or shop with credit cards on public networks.",
                "Turn off AirDrop or network folder sharing so strangers cannot access your device files."
            },
            ["scam"] = new List<string> {
                "To protect yourself from online scams, always pause, stay calm, and independently verify any unexpected messages or requests before sharing personal information or clicking links.",
                "Do not click links or scan QR codes in unexpected texts or emails. Go directly to the official website instead.",
                "Be wary of urgent messages or callers demanding instant action to fix a \"crisis\" or \"locked account\"."
            },
            ["link"] = new List<string> {
                "Do not follow any links in emails to reach Internet banking websites. Malicious software could redirect the link to a fake site.",
                "Place your mouse cursor over a link on a computer, or long-press it on a mobile device, to preview the actual URL.",
                "Navigate to sensitive portals, banks, or payment sites by typing the official URL directly into your browser."
             },
            ["permission"] = new List<string> {
                "Review app permissions before installing an application. For example it doesn't make sense for a torch app to need your contacts or location.",
                "Set sensitive permissions like location, microphone, or camera to \"Only while using the app\" or \"Ask every time.\"",
                "Regularly open your phone's Permission Manager to turn off access for apps you no longer use."
             }
        };

        /// <summary>
        /// Created a default response in case the user's input has no match. It also tells the user what topics are available to ask the chatbot.
        /// </summary>
        private readonly string _defaultResponse = "I didn't quite understand. Could you please rephrase the question?"
            + "\nYou can ask me about password safety, phishing or safe browsing. I can also offer tips about public Wifi, online scams, links in emails and app permissions.";


        /// <summary>
        /// This is used to pick a random tip from a topic's list. Declared once as a field rather than creating it inside the method as creating several Random
        /// objects in quick succession can produce the same sequence of numbers.
        /// </summary>
        private readonly Random _random = new();

        /// <summary>
        /// Tracks which responses have already been shown for each topic, so the chatbot works through all of its tips before repeating any. Once a topic's tips are
        /// exhausted the list is cleared and the cycle starts again.
        /// </summary>
        private readonly Dictionary<string, List<int>> _shown = new();

        /// <summary>
        /// Searches the dictionary for the first keyword contained in the user's input and returns one of that topic's tips.
        /// </summary>
        /// <param name="input">This is the user's message, which is already trimmed and converted to lower case by the InputValidator.</param>
        /// <returns> 
        /// A tip for the first matching keyword, chosen at random from the tips not yet shown for that topic. Once all of a topic's tips have been used the cycle
        /// restarts and a short message is added to say so. If no keyword matches, the default response is returned.
        /// </returns>
        public string GetResponseMatch(string input)
        {
            // Check each keyword in turn and return as soon as one is found 
            foreach (var item in _responses)
            {
                if (input.Contains(item.Key))
                {
                    List<string> options = item.Value;

                    // Only one option, so there is nothing to vary
                    if (options.Count == 1)
                    {
                        return options[0];
                    }

                    // First time a topic is asked about, this creates an empty record of which of its tips have been shown.
                    if (!_shown.ContainsKey(item.Key))
                    {
                        _shown[item.Key] = new List<int>();
                    }

                    List<int> used = _shown[item.Key];
                    string prefix = "";

                    // All tips have been shown for the topic, so record is cleared and the cycle starts again, letting the user know the tips are repeating
                    if (used.Count >= options.Count)
                    {
                        used.Clear();
                        prefix = "I've shared all my tips on that, here they are again:\n\n";
                    }

                    // Build a list of the tips not yet shown, then pick randomly from those
                    List<int> remaining = new();
                    for (int i = 0; i < options.Count; i++)
                    {
                        if (!used.Contains(i))
                        {
                            remaining.Add(i);
                        }
                    }

                    // Picking from the unshown tips
                    int index = remaining[_random.Next(remaining.Count)];
                    used.Add(index);

                    return prefix + options[index];

                }
            }
            // if nothing is found then it returns the default response
            return _defaultResponse;
        }

    }
}
