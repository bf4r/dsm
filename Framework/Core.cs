namespace dsm.Framework;

public class Core
{
    private CommandSystem cs = new();
    private CommandSystemOptions options = CommandSystemOptions.GetDefaultOptions();
    public Dictionary<string, string> VariableStrings = new();
    public Dictionary<string, int> VariableInts = new();
    public Dictionary<string, string> Functions = new();
    public void Run(string code)
    {
        // define function
        cs.Add("df", nameCode =>
        {
            if (nameCode.Contains(':'))
            {
                var parts = nameCode.Split(':');
                var name = parts[0];
                var code = parts[1];
                code = code.Replace('%', '.');
                code = code.Replace("/./", "%");
                Functions[name] = code;
            }
        });
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
        // execute source file dsm
        cs.Add("esfd", path =>
        {
            if (File.Exists(path))
            {
                var fileText = File.ReadAllText(path);
                cs.Run(fileText, options);
            }
        });
        // execute function
        cs.Add("ef", name =>
        {
            if (Functions.ContainsKey(name))
            {
                var functionCode = Functions[name];
                cs.Run(functionCode, options);
            }
        });
        // condition variable int equals
        cs.Add("cvie", nameVal =>
        {
            if (nameVal.Contains(','))
            {
                var parts = nameVal.Split(',');
                if (parts.Length >= 3)
                {
                    var name = parts[0];
                    var val = parts[1];
                    var cod = string.Join(',', parts[2..]);
                    if (int.TryParse(val, out int res))
                    {
                        if (VariableInts[name] == res)
                        {
                            cod = cod.Replace('%', '.');
                            cod = cod.Replace("/./", "%");
                            cs.Run(cod, options);
                        }
                    }
                }
            }
        });
        // condition variable int greater
        cs.Add("cvig", nameVal =>
        {
            if (nameVal.Contains(','))
            {
                var parts = nameVal.Split(',');
                if (parts.Length >= 3)
                {
                    var name = parts[0];
                    var val = parts[1];
                    var cod = string.Join(',', parts[2..]);
                    if (int.TryParse(val, out int res))
                    {
                        if (VariableInts[name] > res)
                        {
                            cod = cod.Replace('%', '.');
                            cod = cod.Replace("/./", "%");
                            cs.Run(cod, options);
                        }
                    }
                }
            }
        });
        // condition variable int less
        cs.Add("cvil", nameVal =>
        {
            if (nameVal.Contains(','))
            {
                var parts = nameVal.Split(',');
                if (parts.Length >= 3)
                {
                    var name = parts[0];
                    var val = parts[1];
                    var cod = string.Join(',', parts[2..]);
                    if (int.TryParse(val, out int res))
                    {
                        if (VariableInts[name] < res)
                        {
                            cod = cod.Replace('%', '.');
                            cod = cod.Replace("/./", "%");
                            cs.Run(cod, options);
                        }
                    }
                }
            }
        });
        // condition variable string equals
        cs.Add("cvse", nameVal =>
        {
            if (nameVal.Contains(','))
            {
                var parts = nameVal.Split(',');
                if (parts.Length >= 3)
                {
                    var name = parts[0];
                    var val = parts[1];
                    var cod = string.Join(',', parts[2..]);
                    if (VariableStrings[name] == val)
                    {
                        cod = cod.Replace('%', '.');
                        cod = cod.Replace("/./", "%");
                        cs.Run(cod, options);
                    }
                }
            }
        });
        // condition string equals
        cs.Add("cse", nameVal =>
        {
            if (nameVal.Contains(','))
            {
                var parts = nameVal.Split(',');
                if (parts.Length >= 3)
                {
                    var val1 = parts[0];
                    var val2 = parts[1];
                    var cod = string.Join(',', parts[2..]);
                    if (val1 == val2)
                    {
                        cod = cod.Replace('%', '.');
                        cod = cod.Replace("/./", "%");
                        cs.Run(cod, options);
                    }
                }
            }
        });
        // read line variable string
        cs.Add("rlvs", name =>
        {
            var input = Console.ReadLine() ?? "";
            VariableStrings[name] = input;
        });

        cs.Run(code, options);
    }
}
