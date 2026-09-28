# AI Phone Assistant

The AI Phone Assistant is a browser-based application that recognizes spoken language, sends it to a locally running AI model, and reads the response back to the user.

The application uses the browser's built-in speech recognition and text-to-speech capabilities. For AI processing, it uses the locally running **Qwen3 8B** language model through **Ollama**.

## Features

* Speech recognition using the browser's Web Speech API
* AI-powered conversations using Qwen3 8B
* Local AI processing through Ollama
* Text-to-speech using the browser's Speech Synthesis API
* Conversation history during the current session
* Automatic continuation of the conversation after the assistant's response

## Technologies

* C#
* ASP.NET Core
* Razor Pages
* JavaScript
* Web Speech API
* Speech Synthesis API
* Ollama
* Qwen3 8B

## Requirements

* .NET 8 SDK
* Ollama
* Qwen3 8B
* A browser with support for the Web Speech API

## Installation

1. Clone the repository.
2. Make sure Ollama is installed and running.
3. Download the Qwen3 8B model:

```bash
ollama pull qwen3:8b
```

4. Open the project in Visual Studio.
5. Start the application.

## Usage

1. Start the application.
2. Click the **Mikrofon starten** button.
3. Allow microphone access when prompted.
4. Speak to the AI assistant.
5. The assistant processes the spoken input and responds using voice output.
6. Continue speaking after the assistant has finished responding.

## Project Structure

* `Program.cs` – Configures the ASP.NET Core application and API endpoint
* `Services/OllamaService.cs` – Communicates with the local Ollama server
* `Pages/Index.cshtml` – Main application page
* `wwwroot/js/aiAssistant.js` – Handles speech recognition, API communication, and text-to-speech
* `wwwroot/css/site.css` – Application styling
