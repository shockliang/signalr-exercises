import * as signalR from "@microsoft/signalr";
import CustomRetryPolicy from "./customRetryPolicy";
import { MessagePackHubProtocol} from "@microsoft/signalr-protocol-msgpack";

let btnGetOne = document.getElementById("btnGetOne");
let btnGetTen = document.getElementById("btnGetTen");
let btnGetOneThousand = document.getElementById("btnGetOneThousand");
let userJson = document.getElementById("userJson") as HTMLTextAreaElement;

function receiveUsers(users) {
    userJson.value = JSON.stringify(users, null, 2);
}
function clear() {
    userJson.value = "Loading...";
}

btnGetOne.addEventListener("click", () => { clear(); connection.invoke("GetUsers", 1).then(receiveUsers); });
btnGetTen.addEventListener("click", () => { clear(); connection.invoke("GetUsers", 10).then(receiveUsers); });
btnGetOneThousand.addEventListener("click", () => { clear(); connection.invoke("GetUsers", 1000).then(receiveUsers); });

// create connection
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/users")
    .configureLogging(signalR.LogLevel.Trace)
    .withHubProtocol(new MessagePackHubProtocol())
    .withAutomaticReconnect(new CustomRetryPolicy())
    .build();

// client events

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