using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Demo2.Console
{
    internal class Program
    {
        private static async void InteractWithSqlDatabase()
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CS");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Required environmental variable SQL_CS was not set");
            }

            await using var connection = new SqlConnection(connectionString);
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT getdate()";
            command.CommandType = CommandType.Text;
            await connection.OpenAsync().ConfigureAwait(false);
            var result = await command.ExecuteScalarAsync().ConfigureAwait(false);
            System.Console.WriteLine(result.ToString());
        }

        private static void InteractWithMessageQueue()
        {
            var messageQueueHost = Environment.GetEnvironmentVariable("MQ_HOST");
            if (string.IsNullOrEmpty(messageQueueHost))
            {
                throw new InvalidOperationException("Required environmental variable MQ_HOST was not set");
            }

            var factory = new ConnectionFactory {HostName = messageQueueHost};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("hello",
                false,
                false,
                false,
                null);

            var message = "Hello World!";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("",
                "hello",
                null,
                body);

            System.Console.WriteLine("Message enqueued");
        }

        public static async Task Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Press CTRL-C or CTRL-Break to terminate");

                System.Console.CancelKeyPress += delegate { Environment.Exit(0); };

                while (true)
                {
                    await Task.Delay(3000);
                    InteractWithSqlDatabase();
                    InteractWithMessageQueue();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}