// See https://aka.ms/new-console-template for more information
using CommandLine;

using Throw;

public class Options
{
    [Option('e', "engine", HelpText = "Template Engine", Required = true)]
    public Engine Engine { get; set; }

    [Option('m', "model", Required = true, HelpText = "File to read JSON")]
    public FileInfo Model { get; set; } = null!;

    [Option('s', "source", Required = true, HelpText = "Template source")]
    public FileInfo Source { get; set; } = null!;

}