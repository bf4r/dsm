namespace dsm;

class Program
{
    static void Main(string[] args)
    {
        var code = string.Join(' ', args);
        var cs = new CommandSystem();
        var options = CommandSystemOptions.GetDefaultOptions();
        // console clear
        cs.Add("cc", _ => Console.Clear());
        // console write
        cs.Add("cw", str => Console.WriteLine(str));
        cs.Run(code, options);
    }
}
