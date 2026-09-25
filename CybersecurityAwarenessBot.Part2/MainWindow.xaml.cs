using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CybersecurityAwarenessBot.Bot;

namespace CybersecurityAwarenessBot.Part2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main chat window deals with the handling of displaying messages and passing the user input to the bot. This window is only responsible for the user
        /// interface, as all the logic stays in the Bot classes.
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
            var bubble = new TextBlock { Text = $"{sender}: {message}",
            Foreground= colour,
            TextWrapping= TextWrapping.Wrap,
            Margin = new Thickness(0,0,0,10),
            FontSize= 14};

            ChatPanel.Children.Add(bubble);
            ChatScroll.ScrollToEnd();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // placeholder for now
        }



        /// <summary>
        /// Deals with the send button. Reads the input, shows it, gets a reply and clears the message for the next message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, RoutedEventArgs e) {
            SendMessage();
        }

        /// <summary>
        ///  The user can click the enter button to send a input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                SendMessage();
            }
        }

        /// <summary>
        /// Function that checks if user has enter something - if blank/white space then it returns. Shows the user and bots responses.
        /// </summary>
        private void SendMessage() {

            string userInput = InputBox.Text;

            // Checks if input is blank and returns if it is
            if (string.IsNullOrWhiteSpace(userInput)) {
                return;
            }

            AddMessage("You", userInput, (Brush)FindResource("TextLight"));

            //Placeholder till I build it
            AddMessage("Bot", "I heard you", (Brush)FindResource("AccentGreen"));

            InputBox.Clear();
            InputBox.Focus();  
        }
    }
}