using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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
    public int Get()
    {
        Console.WriteLine($"The number of processors on this computer is {Environment.ProcessorCount}.");

        var maxWorkerThreads = 0;
        var maxCompletionPortThreads = 0;

        ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
        Console.WriteLine("Maximum worker threads: {0}", maxWorkerThreads);

        var availableWorkerThreads = 0;
        var completionPortThreads = 0;

        ThreadPool.GetAvailableThreads(out availableWorkerThreads, out completionPortThreads);
        Console.WriteLine($"Available Worker threads: {availableWorkerThreads}", availableWorkerThreads);

        var usedWorkerThread = maxWorkerThreads - availableWorkerThreads;
        Console.WriteLine($"Used worker threads: {usedWorkerThread}");

        int minWorker, minIOC;
        // Get the current settings.
        ThreadPool.GetMinThreads(out minWorker, out minIOC);
        Console.WriteLine("Minimum worker threads: {0}", minWorker);

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

        Thread.Sleep(1000 * 10); // Sleep for 10 seconds
        Console.WriteLine("Thread completed. go back to the pool.");
        return usedWorkerThread;
    }
}
