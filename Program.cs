namespace dsm;

class Program
{
    static void Main(string[] args)
    {
        var code = string.Join(' ', args);
        var core = new Framework.Core();
        core.Run(code);
    }
}
