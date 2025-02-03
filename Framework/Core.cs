namespace dsm.Framework;

public class Core
{
    private CommandSystem cs = new();
    private CommandSystemOptions options = CommandSystemOptions.GetDefaultOptions();
    public Dictionary<string, string> VariableStrings = new();
    public Dictionary<string, int> VariableInts = new();
    public Dictionary<string, bool> VariableBools = new();
    public void Run(string code)
    {
        // define variable string
        cs.Add("dvs", argsStr =>
        {
            var args = argsStr.Split(",");
            if (args.Length == 2)
            {
                VariableStrings[args[0]] = args[1];
            }
        });
        // define variable int
        cs.Add("dvi", argsStr =>
        {
            var args = argsStr.Split(",");
            if (args.Length == 2)
            {
                if (int.TryParse(args[1], out int res))
                {
                    VariableInts[args[0]] = res;
                }
            }
        });
        // define variable from int string
        cs.Add("dvfis", argsStr =>
        {
            var args = argsStr.Split(",");
            if (args.Length == 2)
            {
                var stringName = args[0];
                var intToGetFromName = args[1];
                if (VariableInts.ContainsKey(intToGetFromName))
                {
                    var intVal = VariableInts[intToGetFromName];
                    VariableStrings[stringName] = intVal.ToString();
                }
            }
        });
        // define variable from string int
        cs.Add("dvfsi", argsStr =>
        {
            var args = argsStr.Split(",");
            if (args.Length == 2)
            {
                var intName = args[0];
                var stringToGetFromName = args[1];
                if (VariableInts.ContainsKey(stringToGetFromName))
                {
                    var stringVal = VariableStrings[stringToGetFromName];
                    if (int.TryParse(stringVal, out int res))
                    {
                        VariableInts[intName] = res;
                    }
                }
            }
        });
        // modify variable int add variable int
        cs.Add("mviavi", argsStr =>
        {
            var args = argsStr.Split(",");
            if (args.Length == 2)
            {
                var name1 = args[0];
                var name2 = args[1];
                if (VariableInts.ContainsKey(name1) && VariableInts.ContainsKey(name2))
                {
                    VariableInts[name1] += VariableInts[name2];
                }
            }
        });
        // modify variable int subtract variable int
        cs.Add("mvisvi", argsStr =>
        {
            var args = argsStr.Split(",");
            if (args.Length == 2)
            {
                var name1 = args[0];
                var name2 = args[1];
                if (VariableInts.ContainsKey(name1) && VariableInts.ContainsKey(name2))
                {
                    VariableInts[name1] -= VariableInts[name2];
                }
            }
        });
        // x (delete) variable string
        cs.Add("xvs", name =>
        {
            if (VariableStrings.ContainsKey(name))
            {
                VariableStrings.Remove(name);
            }
        });
        // x (delete) variable int
        cs.Add("xvi", name =>
        {
            if (VariableInts.ContainsKey(name))
            {
                VariableInts.Remove(name);
            }
        });
        // console clear
        cs.Add("cc", _ => Console.Clear());
        // console write utf-8
        cs.Add("cwu", codePoint =>
        {
            if (int.TryParse(codePoint, out int codePointInt))
            {
                string s = char.ConvertFromUtf32(codePointInt);
                Console.Write(s);
            }
        });
        // console writeline utf-8
        cs.Add("cwlu", codePoint =>
        {
            if (int.TryParse(codePoint, out int codePointInt))
            {
                string s = char.ConvertFromUtf32(codePointInt);
                Console.WriteLine(s);
            }
        });
        // console write variable int
        cs.Add("cwvi", str =>
        {
            if (VariableInts.ContainsKey(str))
            {
                var val = VariableInts[str];
                Console.Write(val);
            }
        });
        // console writeline variable int
        cs.Add("cwlvi", str =>
        {
            if (VariableInts.ContainsKey(str))
            {
                var val = VariableInts[str];
                Console.WriteLine(val);
            }
        });
        // console write variable string
        cs.Add("cwvs", str =>
        {
            if (VariableStrings.ContainsKey(str))
            {
                var val = VariableStrings[str];
                Console.Write(val);
            }
        });
        // console writeline variable string
        cs.Add("cwlvs", str =>
        {
            if (VariableStrings.ContainsKey(str))
            {
                var val = VariableStrings[str];
                Console.WriteLine(val);
            }
        });
        // console write string
        cs.Add("cws", str =>
        {
            Console.Write(str);
        });
        // console writeline string
        cs.Add("cwls", str =>
        {
            Console.WriteLine(str);
        });


        cs.Run(code, options);
    }
}
