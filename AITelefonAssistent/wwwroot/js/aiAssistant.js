console.log("aiAssistant.js wurde geladen.");
const startButton = document.getElementById("startButton");
const status = document.getElementById("status");
const recognizedText = document.getElementById("recognizedText");
const assistantResponse = document.getElementById("assistantResponse");
const speech = window.speechSynthesis;

function speak(text) {
    const utterance = new SpeechSynthesisUtterance(text);
    speech.speak(utterance);
}


const recognition = new SpeechRecognition();
recognition.lang = "de-DE";
recognition.continuous = false;
recognition.interimResults = false;



// Das 'event' enthält ein großes Paket mit allen erkannten Text-Alternativen
recognition.onresult = function (event) {
    // Holt den wahrscheinlichsten Text (Index [0][0]) aus dem Paket heraus
    const text = event.results[0][0].transcript;

    // Zeigt den gesprochenen Text live auf der Webseite an
    recognizedText.textContent = text;

    processUserInput(text);
}
function processUserInput(text) {
    console.log("Benutzereingabe:", text);

    if (text.includes("Termin")) {
        console.log("Der Benutzer möchte einen Termin.");
        assistantResponse.textContent = "Natürlich. Für welchen Tag?";
        speak("Natürlich. Für welchen Tag?");
    }

    if (text.includes("Wetter")) {
        console.log("Der Benutzer fragt nach dem Wetter.");
        assistantResponse.textContent = "Ich kann dir leider noch keine Wetterdaten geben.";
        speak("Ich kann dir leider noch keine Wetterdaten geben.");
    }
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


async function testChatEndpoint() {
    console.log("testChatEndpoint wurde gestartet.");

    const response = await fetch("/api/chat", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            message: "Hallo"
        })
    });

    const result = await response.text();

    console.log("Antwort vom Server:", result);
}

testChatEndpoint();