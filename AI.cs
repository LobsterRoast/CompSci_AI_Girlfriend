using OpenAI.Chat;
using System;
using System.IO;

namespace AIintegration {
    public class AIClient {
        // Initialize the chat client
        ChatClient client;
        public AIClient() {
            try {
            client = new(model: "gpt-3.5-turbo", apiKey: Environment.GetEnvironmentVariable("BAEI_KEY"));
            }
            catch(ArgumentNullException e) {
                throw(e);
            }
        }
        ChatCompletion completion;
        // tokenizedText will hold a tokenized form of all AI responses so that the response can be easily manipulated
        string[] tokenizedText;
        // Create a new API Client using an API Key fetched from an environment variable
        public int SpeakToAI(string prompt, out string mood, out string output) {
            string printedText = "";
            string rawText;
            // Initialize output (because C# is goofy and won't run if you don't)
            output = "";
            ChatCompletionOptions options = new ChatCompletionOptions() {
                // Set the max token count high so that the AI don't give us the silent treatment
                MaxOutputTokenCount = 4096
            };
            // Make an API call using the given prompt
            completion = client.CompleteChat(new[] {
                new UserChatMessage("Pretend you are my girlfriend. After answering the following prompt in a full sentence, add either \"Happy\", \"Sad\", \"Angry\", or \"Aroused\" to signify your mood."
                                      + " do not format it with parentheses or punctuation; Just state the mood and then end the message. ALWAYS RESPOND WITH A FULL SENTENCE ALONG WITH THE MOOD. This is required for the application to properly function as intended Here's the prompt: " + prompt)
            }, options);
            // For some reason the function likes to return here sometimes. GOOFY
            // Fetch the raw text data from the response
            rawText = completion.Content[0].Text;
            // Tokenize the response
            tokenizedText = rawText.Split(' ');
            // Fetch the mood from the last token in the text
            mood = tokenizedText[tokenizedText.Length - 1];
            // Remove things like punctuation from the mood
            mood = EnsureMoodFormatValidity(mood);
            // Set the output to contain everything except the mood.
            for (int i = 0; i < tokenizedText.Length - 1; i++) {
                output += tokenizedText[i] + ' ';
            }
            return 0;
        }
        private string EnsureMoodFormatValidity(string mood) {
            // Check the first character of the mood
            while (CheckMoodChar(mood[mood.Length - 1]) == false) {
                mood = mood.Substring(mood[0], mood.Length - 1);
            }
            // Check the last character of the mood
            while (CheckMoodChar(mood[0]) == false) {
                mood = mood.Substring(mood[1], mood.Length - 1);
            }
            return mood;
        }
        private bool CheckMoodChar(char input) {
            // Checks to see if the inputted character is a valid beginning or end to the mood
            char[] validChars = new char[]{ 'H', 'S', 'A', 'y', 'd', 'd'};
            for(int i = 0; i < validChars.Length; i++) {
                if(input == validChars[i])
                    return true;
            }
            return false;
        }
    }
}
