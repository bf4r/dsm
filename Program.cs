namespace dsm;

class Program
{
    public static string GetProjectDirectory()
    {
        string? currentDirectory = Directory.GetCurrentDirectory();
        while (currentDirectory != null && !File.Exists(Path.Combine(currentDirectory, "Program.cs")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }
        return currentDirectory ?? throw new DirectoryNotFoundException("Couldn't find project directory containing Program.cs");
    }
    static void Main(string[] args)
    {
        var code = string.Join(' ', args);
        var core = new Framework.Core();
        core.Run(code);
    }
}
