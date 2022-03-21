// See https://aka.ms/new-console-template for more information
using CommandLine;

using Fluid;

using HandlebarsDotNet;

using Newtonsoft.Json;

Parser.Default.ParseArguments<Options>(args)
        .WithParsed(Rende.Render)
        .WithNotParsed(Rende.HandleErrors)
       ;

public interface IRende
{
}

public static class Rende
{
    public static void Render(Options options)
    {
        string source = options.Source.OpenText().ReadToEnd() ?? string.Empty;
        string json = options.Model.OpenText().ReadToEnd();
        var model = JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? new object();
        string result = string.Empty;

        switch (options.Engine)
        {
            case Engine.scriban:
                result = RenderScriban(source, model);
                break;
            case Engine.fluid:
                result = RenderFluid(source, model);
                break;
            case Engine.handlebars:
                result = RenderHandlebars(source, model);
                break;
            default:
                break;
        }

        Console.Out.Write(result);
    }

    public static void HandleErrors(IEnumerable<Error> errors)
    {
        // TODO
    }


    static string RenderFluid(string source, dynamic model)
    {
        // https://github.com/sebastienros/fluid/
        var parser = new Fluid.FluidParser();

        if (!parser.TryParse(source, out var template, out var error))
            throw new Exception(error);

        var context = new Fluid.TemplateContext(model);
        return template.Render(context);
    }

    static string RenderHandlebars(string source, object model)
    {
        // https://github.com/Handlebars-Net/Handlebars.Net
        var template = Handlebars.Compile(source);
        var result = template(model);
        return result;
    }

    static string RenderScriban(string source, object model)
    {
        // https://github.com/scriban/scriban
        var template = Scriban.Template.Parse(source);
        //var model = new { Name = "Scriban" };
        var result = template.Render(model);
        return result;
    }


}