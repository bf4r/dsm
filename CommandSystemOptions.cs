class CommandSystemOptions
{
    public required string Terminator { get; set; }
    public static CommandSystemOptions GetDefaultOptions()
    {
        return new CommandSystemOptions()
        {
            Terminator = "."
        };
    }
}
