SemaphoreSlim threadsSemaphore = new (1);

for (int i = 0; i < 100; i++)
{
    new Thread(() =>
    {
        if (!threadsSemaphore.Wait(0))
        {
            Console.WriteLine("{0} Passed!", Thread.CurrentThread.ManagedThreadId);
        }

        Console.WriteLine("{0} Waiting!", Thread.CurrentThread.ManagedThreadId);

        Thread.Sleep(5000);

        threadsSemaphore.Release();

        Console.WriteLine("{0} Released!", Thread.CurrentThread.ManagedThreadId);

    }).Start();
}

Console.ReadLine();

SemaphoreSlim tasksSemaphore = new(1);

for (int i = 0; i < 100; i++)
{
    _ = Task.Run(async () =>
    {
        if (!await tasksSemaphore.WaitAsync(0))
        {
            Console.WriteLine("{0} Passed!", Task.CurrentId);
        }

        Console.WriteLine("{0} Waiting!", Task.CurrentId);

        await Task.Delay(5000);

        tasksSemaphore.Release();

        Console.WriteLine("{0} Released!", Task.CurrentId);
    });
}

Console.ReadLine();