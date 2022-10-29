using System.Diagnostics;

public class ProcessRunner
{
    private readonly string _workingDirectory;

    public ProcessRunner(string workingDirectory)
    {
        _workingDirectory = workingDirectory;
    }

    public void Invoke(string program, string arguments = "")
    {
        ProcessStartInfo processStartInfo = new()
        {
            WorkingDirectory = _workingDirectory,
            FileName = program,
            Arguments = arguments
        };
        Process? process = Process.Start(processStartInfo);
        if (process is null)
        {
            throw new NullReferenceException("Process is null, it was not created");
        }

        process.WaitForExit();
    }
}
