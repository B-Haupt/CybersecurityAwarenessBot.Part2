using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CybersecurityAwarenessBot.Bot;

namespace CybersecurityAwarenessBot.Part2
{
    /// <summary>
    /// This class is responsible for the user interface. Meaning it deals with the displaying of messages, reading what the user types and passing it to the bot. All the chatbot logic
    /// stays in the Bot classes.
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly GreetingPlayer _greeting = new();
        private readonly LogoArt _logo = new();
        private readonly ChatBot _bot = new();
        private readonly InputValidator _validator = new();
        /// <summary>
        /// True until the user has given their name. Set it so that while it is true, whatever the user types in is stored as their name instead of treated like a question.
        /// </summary>
        private bool _awaitingName = true;

        /// <summary>
        /// Sets up the window and its controls
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function is to add a message bubble to the chat panel and scrolls to the bottom
        /// </summary>
        /// <param name="sender">Who is speaking, used as a label</param>
        /// <param name="message">The text to display.</param>
        /// <param name="colour">The colour of the text.</param>
        private void AddMessage(string sender, string message, Brush colour)
        {
            var bubble = new TextBlock { 
                Text = $"{sender}: {message}",
                Foreground= colour,
                TextWrapping= TextWrapping.Wrap,
                Margin = new Thickness(0,0,0,10),
                FontSize= 14
            };

            ChatPanel.Children.Add(bubble);
            ChatScroll.ScrollToEnd();
        }

        /// <summary>
        /// Runs when the window opens. Displays the logo, plays the greeting and then asks the user for their name.
        /// </summary>
        /// <param name="sender">The window raising the event.</param>
        /// <param name="e">Event data</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Store the art in the header
            AsciiHeader.Text = _logo.GetLogo();

            // Play the voice greeting and report it in the chat if it doesn't play
            string? error = _greeting.PlayGreeting();
            if (error != null) {
                AddMessage("Bot", error, (Brush)FindResource("AccentGreen"));
            }


            AddMessage("Bot", "Hello! Welcome to the Cybersecurity Awareness Bot. What is your name?",(Brush)FindResource("AccentGreen"));

            InputBox.Focus();

        }



        /// <summary>
        /// Deals with the send button being clicked
        /// </summary>
        /// <param name="sender">The button raising the event.</param>
        /// <param name="e">Event data</param>
        private void SendButton_Click(object sender, RoutedEventArgs e) {
            SendMessage();
        }

        /// <summary>
        ///  The user can click the enter button to send a input
        /// </summary>
        /// <param name="sender">Text box raising the event</param>
        /// <param name="e">Event data used to check which key was pressed</param>
        private void InputBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) 
            {
                SendMessage();
            }
        }

        /// <summary>
        /// Function that checks if user has enter something - if blank/white space then it returns. Shows the user and bots responses.
        /// </summary>
        private void SendMessage() {

            string userInput = InputBox.Text;

            // Checks if input is blank and returns if it is
            if (!_validator.IsValidInput(userInput)) {
                return;
            }

            if (_awaitingName) 
            {
                _bot.User.Name = userInput.Trim();
                _awaitingName = false;

                AddMessage("You", userInput, (Brush)FindResource("TextLight"));

                AddMessage("Bot", $"Welcome, {_bot.User.Name}! I'm a Cybersecurity Awareness Bot. You can ask me about password safety, phishing, safe browsing, "
                    +"public wifi, privacy, online scams, links in emails and app permissions.",(Brush)FindResource("AccentGreen"));

                InputBox.Clear();
                InputBox.Focus();
                return;
            }

            string label = string.IsNullOrWhiteSpace(_bot.User.Name) ? "You" : _bot.User.Name;
            AddMessage(label, userInput, (Brush)FindResource("TextLight"));

            AddMessage("Bot", _bot.GetReply(userInput), (Brush)FindResource("AccentGreen"));

            InputBox.Clear();
            InputBox.Focus();  
        }
    }
}