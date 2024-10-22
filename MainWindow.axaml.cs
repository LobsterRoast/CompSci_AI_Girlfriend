using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.Diagnostics;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using AIintegration;

namespace AI_Girlfriend;
public partial class MainWindow : Window
{
    public ReactiveCommand<Unit, Unit> SendMessage { get; }
    AIClient client = new AIClient();
    Dictionary<string, byte[]> moods = new Dictionary<string, byte[]>();
    SolidColorBrush hairBrush; 
    string sMood;
    byte[] currentMood = new byte[4];
    public MainWindow()
    {
        // Initialize the window
        InitializeComponent();
        // Define the 4 respective hair colors for the 4 possible moods for the AI
        moods.Add("Happy", new byte[] { 100, 0, 255, 0 } );
        moods.Add("Sad", new byte[] { 100, 30, 130, 255 } );
        moods.Add("Angry", new byte[] { 100, 255, 0, 0 } );
        moods.Add("Aroused", new byte[] { 100, 255, 0, 255 } );
        hairBrush = (SolidColorBrush)hair.Fill;
        // Make SendMessage a bindable function in the XAML code
        SendMessage = ReactiveCommand.Create(SendMessageFunc);
        DataContext = this;
    }
    private void UpdateHairColor() {
        // Fetch the AI's mood's respective RGB data from the dictionary
        byte[] currentMood = new byte[4];
        moods.TryGetValue(sMood, out currentMood);
        // Sets the hair color
        hairBrush.Color = Color.FromArgb(currentMood[0], currentMood[1], currentMood[2], currentMood[3]);
    }
    // Runs when the user presses the button to talk to the AI
    public void SendMessageFunc() {
        // Gets the inputted prompt from the text box into memory and then clears the box
        string text = InputBox.Text;
        string outputText;
        InputBox.Text = "";
        // Gives the prompt to the AI and fetches the mood and outputted text as well
        client.SpeakToAI(text, out sMood, out outputText);
        // Sets the ResponseBlock to show the outputted text
        ResponseBlock.Text = outputText;
        // Sets the MoodBlock to explicitly state the mood of the AI
        MoodBlock.Text = "My Mood is: " + sMood;
        UpdateHairColor();
    }
}