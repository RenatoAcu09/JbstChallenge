class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
   
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let when = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container = document.createElement('div');
    container.className = "row";
    let container2= document.createElement('div');
    container2.className = isCurrentUserMessage ? "col-md-6 offset-md-6" : "";
    let container3 = document.createElement('div');
    container3.className = isCurrentUserMessage ? "container darker bg-primary" : "container bg-light";



    let text = document.createElement('p');
    text.className = isCurrentUserMessage ? " text-right text-white" : " text-left";
    text.style = "font-weight:bold";
    text.innerHTML = message.text;


    let container_container = document.createElement('p');
    container_container.className = isCurrentUserMessage ? " text-right text-white" : " text-left";

    let sender = document.createElement('span');
    sender.innerHTML = message.userName;

    let space = document.createElement('span');
    space.innerHTML = "  -  ";

    let when = document.createElement('span');
    when.className = (isCurrentUserMessage ? "time-right" : "time-left");
    var currentdate = new Date();
    when.innerHTML = 
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    container3.appendChild(text);
    container_container.appendChild(sender);
    container_container.appendChild(space);
    container_container.appendChild(when);
    container3.appendChild(container_container);
    container2.appendChild(container3);
    container.appendChild(container2);
    chat.appendChild(container);
}
