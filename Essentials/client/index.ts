import * as signalR from "@microsoft/signalr";
import CustomRetryPolicy from "./customRetryPolicy";

let counter = document.getElementById("viewCounter");

// create connection
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/view")
    .withAutomaticReconnect(new CustomRetryPolicy())
    .build();

// client events
connection.on("viewCountUpdate", (value: number) => {
    counter.innerText = value.toString();
});

// connection events
connection.onreconnected((connectionId: string) => {
    console.log(`Reconnected. connection id: ${connectionId}`);
});

connection.onreconnecting((error: Error) => {
   console.error(`Reconnecting error`, error); 
});

connection.onclose((error: Error) => {
   console.error(`On close`, error); 
});

// start the connection
function startSuccess() {
    console.log("Connected.");
}
function startFail() {
    console.log("Connection failed.");
}

connection.start().then(startSuccess, startFail);