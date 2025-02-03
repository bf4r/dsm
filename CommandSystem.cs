namespace dsm;

class CommandSystem
{
    public Dictionary<string, Action<string>> Commands = new();

    public void Add(string name, Action<string> action)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Command name cannot be empty", nameof(name));
        Commands[name] = action ?? throw new ArgumentNullException(nameof(action));
    }

    public void Run(string code, CommandSystemOptions options)
    {
        if (options?.Terminator == null) throw new ArgumentNullException(nameof(options));
        code = new string(code.Where(c => !char.IsWhiteSpace(c)).ToArray());
        var ter = options.Terminator;
        var currentPos = 0;
        while (currentPos < code.Length)
        {
            var nextTerIndex = code.IndexOf(ter, currentPos);
            if (nextTerIndex == -1) break;
            var cmd = code.Substring(currentPos, nextTerIndex - currentPos);
            if (!string.IsNullOrEmpty(cmd))
            {
                var excmd = Commands.Keys.FirstOrDefault(key => cmd.StartsWith(key));
                if (!string.IsNullOrEmpty(excmd))
                {
                    var args = cmd.Substring(excmd.Length);
                    Commands[excmd]?.Invoke(args);
                }
            }
            currentPos = nextTerIndex + ter.Length;
        }
    }
}
