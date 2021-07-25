import {ILogger, LogLevel} from "@microsoft/signalr";

export class CustomerLogger implements ILogger {
    log(logLevel:LogLevel, message:string) {
        console.log(`[${LogLevel[logLevel]}]: ${message}`);
    }
}