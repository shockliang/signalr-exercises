import * as signalR from "@microsoft/signalr";

let body = (document.getElementsByTagName("body")[0]) as HTMLElement;
let btnRed = document.getElementById("btnRed") as HTMLElement;
let btnGreen = document.getElementById("btnGreen") as HTMLElement;
let btnBlue = document.getElementById("btnBlue") as HTMLElement;
let time = document.getElementById("currentTime") as HTMLElement;

// create connection
let colorConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/background")
    .build();

let timeConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/time")
    .build();

// on background change update message from client
colorConnection.on("changeBackground", (value: string) => {
    body.style.backgroundColor = value;
});

timeConnection.on("updatedTime", (value: string) => {
    time.innerText = value;
});

function onRed() {
    colorConnection.send("changeBackground", "red");
}

function onGreen() {
    colorConnection.send("changeBackground", "green");
}

function onBlue() {
    colorConnection.send("changeBackground", "blue");
}

// start the connection
function colorStartSuccess() {
    console.log("Connected to Color Hub.");
}
function timeStartSuccess() {
    console.log("Connected to Time Hub.");
    setInterval(() => {
        timeConnection.send("getCurrentTime");
    }, 500);
}
function startFail() {
    console.log("Connection failed.");
}

btnRed.onclick = onRed;
btnGreen.onclick = onGreen;
btnBlue.onclick = onBlue;

colorConnection.start().then(colorStartSuccess, startFail);
timeConnection.start().then(timeStartSuccess, startFail);