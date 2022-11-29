# Chat 

This is a simple chat app that with this command "stock=stock_code" consumes an API that returns a csv file which is parsed and then sent to an AWS SQS Queue.

## Solution structure
The intention of this arquitecture is to be the closest of DDD. 

 1. BotAPI (Bot that manage the command sent in the chat)
 2. Domain
 3. Infrastructure
 4. Infracstructure.Tests
 5. WebChat (Application layer)
 6. SQSConsumer(pending)

## How to run the applications

Since there is not an installer yet to run the application we need to id manually.

 1. Open Visual Studio(2019, 2022) and load the solution.
 2. Set the WebChat as a startup project and run de WebChat profile.
 3.  Open another instance of Visual Studio and load the same solution again but this time set the BotAPI project as a startup project and run the https profile.
 4. Open another browser window of the chat and start chatting and sending the command.

## Considerations

I have created an AWS SQS queue to receive the csv parsed values but I haven't created the consumer yet to send the requested test(e.g.:“APPL.US quote is $93.42 per share”) so in order to see the message in the queue we need to do it in a video call.
I tried to finish everything but the time wasn't enough since this is the first time I've used SignalR.

If there is an issue to run the applications please let me know.

Thanks!
