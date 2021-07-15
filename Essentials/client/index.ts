import * as signalR from "@microsoft/signalr";
import {CustomerLogger} from "./loggers/customerLogger";

// Force disable websocket
// WebSocket = undefined;

var counter = document.getElementById("viewCount");
let btn = document.getElementById("incrementView");

// create connection
let connection = new signalR.HubConnectionBuilder()
    // .configureLogging(signalR.LogLevel.Trace)
    .configureLogging( new CustomerLogger())
    .withUrl("/hubs/view", {transport: 
    // .withUrl("/hubs/stringtools", {transport: 
            signalR.HttpTransportType.WebSockets | 
            signalR.HttpTransportType.ServerSentEvents})
    .build();

btn.addEventListener("click", function (evt) {
    connection.invoke("incrementServerView");
});

connection.on("incrementView", (val) => {
    counter.innerText = val;
    if(val % 10 === 0) connection.off("incrementView");
});

// on view update message from client
connection.on("viewCountUpdate", (value: number) => {
    counter.innerText = value.toString();
});

// notify server we're watching
function notify(){
    connection.send("notifyWatching");
}

// start the connection
function startSuccess(){
    console.log("Connected.");
    notify();
}
function startFail(){
    console.log("Connection failed.");
}

connection.start().then(startSuccess, startFail);