using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;

// Program should be running in ROOT\src\AutoHome.RestClientGenerator\bin\Debug\net7.0\
// Our working directory is in the src folder so lets go up 5 directories
var root = "..\\..\\..\\..\\..\\";
var runner = new ProcessRunner($"{root}src\\AutoHome.Server");

// Restore dotnet swagger tool
runner.Invoke("dotnet", "tool restore");
// Invoke the swagger cli to generate swagger.json in the AutoHome.RestClient project
runner.Invoke("dotnet", "swagger tofile --output ..\\AutoHome.RestClientGenerator\\swagger.json bin\\Debug\\net7.0\\AutoHome.Server.dll v1");

// Open swagger.json as an api document
var json = await File.ReadAllTextAsync($"{root}src\\AutoHome.RestClientGenerator\\swagger.json");
var document = await OpenApiDocument.FromJsonAsync(json);

// Setup code generation settings
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

// Generate the rest client
var generator = new CSharpClientGenerator(document, settings);
var code = generator.GenerateFile();

// Write the rest client class to the AutoHome.RestClient porject
File.WriteAllText($"{root}src\\AutoHome.RestClient\\{className}.cs", code);

Console.WriteLine($"{className}.cs generated");
