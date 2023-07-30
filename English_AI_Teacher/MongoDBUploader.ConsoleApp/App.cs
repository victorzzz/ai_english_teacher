using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EnglishAI.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDBUploader.ConsoleApp.Options;
using MongoDB.Bson.IO;
using EnglishAI.Infrastructure.DBRepositories.Models;

namespace MongoDBUploader.ConsoleApp;

public class App
{
    private readonly ILogger<App> _logger;
    private readonly MongoDBOptions _mongoDBOptions;
    private readonly AppOptions _appOptions;
    private readonly HttpClient _httpClient;

    public App(
        ILogger<App> logger,
        IOptions<MongoDBOptions> mongoDBOptions,
        IOptions<AppOptions> appOptions,
        HttpClient httpClient)
    {
        _logger = logger;
        _mongoDBOptions = mongoDBOptions.Value;
        _appOptions = appOptions.Value;
        _httpClient = httpClient;
    }   

    public async Task ProcessPhrasalVerbs()
    {
        var content = await _httpClient.GetStringAsync(_appOptions.PhrasalVerbsPath);
        var phrasalVerbs = JsonDocument.Parse(content);

        // enumerate all top level properties in Json document phrasalVerbs
        foreach (var phrasalVerbItem in phrasalVerbs.RootElement.EnumerateObject())
        {
            var phrasalVerbValue = phrasalVerbItem.Value;

            var phrasalVerbEntity = phrasalVerbValue.Deserialize<PhrasalVerbEntity>();
        }

    }

    public async Task ProcessAWL()
    {

    }
    public async Task ProcessNGSL()
    {

    }

    public async Task ProcessAllWords()
    {

    }
}
