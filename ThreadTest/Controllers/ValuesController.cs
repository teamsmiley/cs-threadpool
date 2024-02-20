using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace ThreadTest.Controllers;

[ApiController]
[Route("values")]
public class ValuesController : ControllerBase
{
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(ILogger<ValuesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public OkResult Get()
    {
        // Console.WriteLine($"The number of processors on this computer is {Environment.ProcessorCount}.");

        // var maxWorkerThreads = 0;
        // var maxCompletionPortThreads = 0;

        // ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
        // Console.WriteLine("Maximum worker threads: {0}", maxWorkerThreads);

        // var availableWorkerThreads = 0;
        // var completionPortThreads = 0;

        // ThreadPool.GetAvailableThreads(out availableWorkerThreads, out completionPortThreads);
        // Console.WriteLine($"Available Worker threads: {availableWorkerThreads}", availableWorkerThreads);

        // var usedWorkerThread = maxWorkerThreads - availableWorkerThreads;
        // Console.WriteLine($"Used worker threads: {usedWorkerThread}");

        // int minWorker, minIOC;
        // // Get the current settings.
        // ThreadPool.GetMinThreads(out minWorker, out minIOC);
        // Console.WriteLine("Minimum worker threads: {0}", minWorker);

        // using var connection = new MySqlConnection("server=192.168.1.21;User ID=root;Password=root;database=test; ");
        // connection.Open();

        // using var command = connection.CreateCommand();
        // // command.CommandText = "DO SLEEP(1000);SELECT Url FROM Blogs;";
        // command.CommandText = "SELECT Url FROM Blogs;";

        // using var reader = command.ExecuteReader();
        // while (reader.Read())
        // {
        //     var url = reader.GetString(0);
        //     System.Console.WriteLine($"blog url {url}");
        // }

        // Thread.Sleep(1000 * 10); // Sleep for 10 seconds
        // Console.WriteLine("Thread completed. go back to the pool.");

        // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"); // single
        // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,localhost:6380,localhost:6381"); // main-secondary
        // if (redis == null) return Ok();
        // if (!redis.IsConnected) return Ok();

        // IDatabase db = redis.GetDatabase(); //database 0 을 사용한다.
        // // IDatabase db = redis.GetDatabase(1); //database 1 을 사용한다.

        // Console.WriteLine(db.Database.ToString());

        // db.StringSet("foo2", "bar2");

        // Console.WriteLine(db.StringGet("foo")); // prints bar

        //sentinel
        // var sentinelConfig = new ConfigurationOptions
        // {
        //     AllowAdmin = false,
        //     AbortOnConnectFail = false,
        //     CommandMap = CommandMap.Sentinel,
        //     EndPoints =
        //         {
        //             "host.docker.internal:26379",
        //             "host.docker.internal:26380",
        //             "host.docker.internal:26381"
        //         },
        // };

        // var masterConfig = new ConfigurationOptions
        // {
        //     CommandMap = CommandMap.Default,
        //     ServiceName = "mymaster",
        // };

        // var redis = ConnectionMultiplexer.SentinelConnect(sentinelConfig, Console.Out);
        // var conn = redis.GetSentinelMasterConnection(masterConfig, Console.Out);
        // var db = conn.GetDatabase(0);

        // db.StringSet("foo2", "bar2");

        // Console.WriteLine(db.StringGet("foo2")); // prints bar2

        // redis.Close();

        // cluster

        ConnectionMultiplexer redisCluster = null;
        try
        {
            string connectString = "127.0.0.1:7000,127.0.0.1:7001,127.0.0.1:7002";
            var options = ConfigurationOptions.Parse(connectString);
            options.AllowAdmin = true;
            options.ConfigCheckSeconds = 10;
            options.SyncTimeout = 1000;
            //  options.Password = "password";
            redisCluster = ConnectionMultiplexer.Connect(options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        int loop = 100000;
        IDatabase clusterDb = redisCluster.GetDatabase();
        Console.WriteLine("SET Sync " + loop + " - Press any key to start");
        Console.ReadKey();

        return Ok();
    }
}
