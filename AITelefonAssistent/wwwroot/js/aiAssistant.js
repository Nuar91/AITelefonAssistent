console.log("aiAssistant.js wurde geladen.");
const startButton = document.getElementById("startButton");
const status = document.getElementById("status");
const recognizedText = document.getElementById("recognizedText");
const assistantResponse = document.getElementById("assistantResponse");
const speech = window.speechSynthesis;
const conversation = [];



let isAssistantSpeaking = false;

function speak(text) {
    isAssistantSpeaking = true;

    const utterance = new SpeechSynthesisUtterance(text);

    utterance.onend = function () {
        isAssistantSpeaking = false;
        recognition.start();
    };

    speech.speak(utterance);
}

const recognition = new SpeechRecognition();
recognition.lang = "de-DE";
recognition.continuous = false;
recognition.interimResults = false;



// Das 'event' enthält ein großes Paket mit allen erkannten Text-Alternativen
recognition.onresult = function (event) {

    if (isAssistantSpeaking) {
        return;
    }

    const text = event.results[0][0].transcript;

    recognizedText.textContent = text;

    processUserInput(text);
}



function processUserInput(text) {
    console.log("Benutzereingabe:", text);
    

    conversation.push({
        role: "user",
        content: text
    });


    testChatEndpoint(text);
}

startButton.addEventListener("click", async function () {
    console.log("Button wurde geklickt");
    recognition.start(); // Aktiviert das Mikrofon und startet das Zuhören. Sobald Text erkannt wurde, wird automatisch 'onresult' ausgelöst.


    try {
        // Dieser Block läuft sofort nach dem Start-Befehl parallel weiter,
        // während im Hintergrund das Mikrofon aufnimmt.
        console.log("mediaDevices:", navigator.mediaDevices);

        const stream = await navigator.mediaDevices.getUserMedia({ audio: true});

        console.log("Stream:", stream);

        console.log("Mikrofon wurde freigegeben!");

        status.textContent = "Status: Mikrofon ist aktiv";
    }
    catch (error) {
        console.error("Mikrofon konnte nicht aktiviert werden:", error);

        status.textContent = "Status: Mikrofon konnte nicht aktiviert werden.";
    }
});

//async function testChatEndpoint()
async function testChatEndpoint(text)
{
    console.log("testChatEndpoint wurde gestartet.");

    const response = await fetch("/api/chat", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            //message: "Hallo"
            // message: text
            messages: conversation
        })
    });

    const result = await response.text();
    conversation.push({
        role: "assistant",
        content: result
    });

    console.log("Antwort vom Server:", result);

    assistantResponse.textContent = result;

    speak(result);
}



