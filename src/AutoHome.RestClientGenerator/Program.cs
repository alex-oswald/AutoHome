using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Diagnostics;

var json = await File.ReadAllTextAsync("swagger.json");
var document = await OpenApiDocument.FromJsonAsync(json);

string className = "AutoHomeRestClient";

var settings = new CSharpClientGeneratorSettings
{
    ClassName = className,
    CSharpGeneratorSettings =
    {
        Namespace = "AutoHome.Api",
        JsonLibrary = CSharpJsonLibrary.SystemTextJson,
    }
};

var generator = new CSharpClientGenerator(document, settings);
var code = generator.GenerateFile();

File.WriteAllText($"..\\..\\..\\..\\AutoHome.RestClient\\{className}.cs", code);

Console.WriteLine($"{className}.cs generated");

Debugger.Break();
