import * as signalR from "@microsoft/signalr";
import {CustomerLogger} from "./loggers/customerLogger";

// Force disable websocket
// WebSocket = undefined;

let btnJoinYellow = document.getElementById("btnJoinYellow");
let btnJoinBlue = document.getElementById("btnJoinBlue");
let btnJoinOrange = document.getElementById("btnJoinOrange");
let btnTriggerYellow = document.getElementById("btnTriggerYellow");
let btnTriggerBlue = document.getElementById("btnTriggerBlue");
let btnTriggerOrange = document.getElementById("btnTriggerOrange");

// create connection
let connection = new signalR.HubConnectionBuilder()
    // .configureLogging(signalR.LogLevel.Trace)
    .configureLogging( new CustomerLogger())
    // .withUrl("/hubs/view", {transport: 
    // .withUrl("/hubs/stringtools", {transport: 
    .withUrl("/hubs/color", {transport: 
            signalR.HttpTransportType.WebSockets | 
            signalR.HttpTransportType.ServerSentEvents})
    .build();

btnJoinYellow.addEventListener("click", () => {connection.invoke("JoinGroup", "Yellow")});
btnJoinBlue.addEventListener("click", () => {connection.invoke("JoinGroup", "Blue")});
btnJoinOrange.addEventListener("click", () => {connection.invoke("JoinGroup", "Orange")});

btnTriggerYellow.addEventListener("click", () => {connection.invoke("TriggerGroup", "Yellow")});
btnTriggerBlue.addEventListener("click", () => {connection.invoke("TriggerGroup", "Blue")});
btnTriggerOrange.addEventListener("click", () => {connection.invoke("TriggerGroup", "Orange")});

connection.on("triggerColor", (color) => {
    document.getElementsByTagName("body")[0].style.backgroundColor = color;
});


// start the connection
function startSuccess(){
    console.log("Connected.");
}
function startFail(){
    console.log("Connection failed.");
}

connection.start().then(startSuccess, startFail);