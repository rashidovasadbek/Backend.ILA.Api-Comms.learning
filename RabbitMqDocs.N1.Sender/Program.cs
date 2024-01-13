
// create connection factory

using System.Text;
using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection 
using var connection = await connectionFactory.CreateConnectionAsync();

// create channel
using var channel = await connection.CreateChannelAsync();

//declare queue
await channel.QueueDeclareAsync(
    queue: "hello-world",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

//produce  message
var message = "Hello Messaging";
var body = Encoding.UTF8.GetBytes(message);

//publish message

await channel
    .BasicPublishAsync(
        exchange: string.Empty,
        routingKey: "hello-world",
        body: body);