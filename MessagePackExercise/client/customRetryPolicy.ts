import {RetryContext} from "@microsoft/signalr";

export default class CustomRetryPolicy implements signalR.IRetryPolicy {
    maxRetryAttempts = 0;
    
    nextRetryDelayInMilliseconds(retryContext: RetryContext): number | null {
        console.info(`Retry :: ${retryContext.retryReason}`);
        if (retryContext.previousRetryCount === 10) return null; // count to 10 that should stop it.
        
        var nextRetry = retryContext.previousRetryCount * 1000 || 1000;
        console.info(`Retry in ${nextRetry} milliseconds`);
        return nextRetry;
    }
    
}