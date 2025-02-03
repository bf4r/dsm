namespace dsm;

class CommandSystem
{
    public Dictionary<string, Action<string>> Commands = new();
    public void Add(string name, Action<string> action)
    {
        Commands.Add(name, action);
    }
    public void Run(string code, CommandSystemOptions options)
    {
        var ter = options.Terminator;
        var currentPos = 0;
        while (currentPos < code.Length)
        {
            var nextTerIndex = code.IndexOf(ter, currentPos);
            if (nextTerIndex == -1) break;
            var cmd = code.Substring(currentPos, nextTerIndex - currentPos);

            var kvp = Commands.FirstOrDefault(x => cmd.StartsWith(x.Key));
            var name = kvp.Key;
            if (!string.IsNullOrEmpty(name))
            {
                var args = cmd.Substring(name.Length);
                var action = kvp.Value;
                action?.Invoke(args);
            }
            currentPos += nextTerIndex + ter.Length;
        }
    }
}
