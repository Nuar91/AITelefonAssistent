const startButton = document.getElementById("startButton");
const status = document.getElementById("status");
const recognizedText = document.getElementById("recognizedText");
const recognition = new SpeechRecognition();
recognition.lang = "de-DE";
recognition.continuous = false;
recognition.interimResults = false;




recognition.onresult = function (event) {
    const text = event.results[0][0].transcript;
    recognizedText.textContent = text;
}

startButton.addEventListener("click", async function () {
    console.log("Button wurde geklickt");
    recognition.start();

    try {
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