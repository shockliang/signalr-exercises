import * as signalR from "@microsoft/signalr";
import CustomRetryPolicy from "./customRetryPolicy";

let pieVotes = document.getElementById("pieVotes");
let baconVotes = document.getElementById("baconVotes");

// create connection
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/vote")
    .withAutomaticReconnect(new CustomRetryPolicy())
    .build();

// client events
connection.on("updateVotes", (votes) => {
    pieVotes.innerText = votes.pie;
    baconVotes.innerText = votes.bacon;
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
    connection.invoke("GetCurrentVotes").then((votes) => {
        pieVotes.innerText = votes.pie;
        baconVotes.innerText = votes.bacon;
    });
}
function startFail() {
    console.log("Connection failed.");
}

connection.start().then(startSuccess, startFail);