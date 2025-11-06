# Wordle Helper

A program to solve Wordle problems.

## Online Version

Visit the online version at: https://dreamprism.github.io/WordleHelper

The online version is a pure frontend JavaScript implementation that runs entirely in your browser, with no backend required.

### Features

- 🎮 **Interactive Interface**: Click on letter boxes to cycle through colors (gray → yellow → green)
- 📚 **Dual Dictionary Support**: Use the default words_alpha.txt (370,000+ words) or upload your custom dictionary
- 📱 **Responsive Design**: Works seamlessly on desktop, tablet, and mobile devices
- 🎨 **Minimalist UI**: Clean white background with intuitive controls
- 📝 **Guess History**: Track your previous guesses and their color patterns
- 🔍 **Real-time Filtering**: Instantly see possible words based on your clues

### How to Use

1. Enter your guess word in the input field
2. Click on each letter box to set its color:
   - **Gray**: Letter is not in the word
   - **Yellow**: Letter is in the word but in wrong position
   - **Green**: Letter is in the word and in correct position
3. Click "分析" (Analyze) to see possible matching words
4. Continue with your next guess until you find the answer

### C# Version

The C# console version is available in the main branch. See `Program.cs` for the implementation.