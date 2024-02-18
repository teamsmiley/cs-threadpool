using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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



        Thread.Sleep(1000000); // Sleep for 10 seconds
        Console.WriteLine("Thread completed. go back to the pool.");
        return usedWorkerThread;
    }
}
