// See https://aka.ms/new-console-template for more information
using CommandLine;

using Throw;

public class Options
{
    [Option('e', "engine", HelpText = "Template Engine")]
    public Engine Engine { get; set; }

    public dynamic GetJsonValue()
    {
        return System.Text.Json.JsonSerializer.Deserialize<dynamic>(Json) ?? new { };
    }

    [Option('s', "stdin", Required = false, HelpText = "Read JSON from STDIN")]
    public bool Stdin { get; set; }

    [Option('m', "model", Required = false, HelpText = "File to read JSON")]
    public FileInfo Model { get; set; } = null!;

    [Option('t', "template", Required = false, HelpText = "File to read JSON")]
    public FileInfo Template { get; set; } = null!;

    [Option('j', "json", Required = false, HelpText = "Raw JSON")]
    public string Json { get; set; } = null!;
}
