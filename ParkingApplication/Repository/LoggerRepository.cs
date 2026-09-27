namespace ParkingApplication.Repository;

public class LoggerRepository
{
    private readonly string _filePath;

    private SemaphoreSlim _semaphoreSlim= new SemaphoreSlim(1,1);

    public LoggerRepository(string filepath)
    {
        this._filePath = filepath;
    }

    public async Task Log(string message)
    {
        await _semaphoreSlim.WaitAsync();

        try
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            await File.AppendAllTextAsync(_filePath, logMessage + Environment.NewLine);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}
