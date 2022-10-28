using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Diagnostics;

var json = await File.ReadAllTextAsync("swagger.json");
var document = await OpenApiDocument.FromJsonAsync(json);

var settings = new CSharpClientGeneratorSettings
{
    ClassName = "MyClass",
    CSharpGeneratorSettings =
    {
        Namespace = "MyNamespace"
    }
};

var generator = new CSharpClientGenerator(document, settings);
var code = generator.GenerateFile();

File.WriteAllText("RestClient.cs", code);

Debugger.Break();
