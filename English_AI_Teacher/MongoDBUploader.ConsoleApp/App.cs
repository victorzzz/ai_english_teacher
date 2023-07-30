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
using EnglishAI.Application.Interfaces;
using EnglishAI.Application.Models.DB;

namespace MongoDBUploader.ConsoleApp;

public class App
{
    private readonly ILogger<App> _logger;
    private readonly MongoDBOptions _mongoDBOptions;
    private readonly AppOptions _appOptions;
    private readonly IPhrasalVerbRepository _phrasalVerbRepository;
    private readonly HttpClient _httpClient;

    public App(
        ILogger<App> logger,
        IOptions<MongoDBOptions> mongoDBOptions,
        IOptions<AppOptions> appOptions,
        IPhrasalVerbRepository phrasalVerbRepository,
        HttpClient httpClient)
    {
        _logger = logger;
        _mongoDBOptions = mongoDBOptions.Value;
        _appOptions = appOptions.Value;
        _phrasalVerbRepository = phrasalVerbRepository;
        _httpClient = httpClient;
    }   

    public async Task ProcessPhrasalVerbs(CancellationToken cancellationToken)
    {
        var content = await _httpClient.GetStringAsync(_appOptions.PhrasalVerbsPath);
        var phrasalVerbs = JsonDocument.Parse(content);

        // enumerate all top level properties in Json document phrasalVerbs
        foreach (var phrasalVerbItem in phrasalVerbs.RootElement.EnumerateObject())
        {
            var phrasalVerbValue = phrasalVerbItem.Value;

            var phrasalVerbEntity = phrasalVerbValue.Deserialize<PhrasalVerb>();

            if (phrasalVerbEntity == null)
            {
                _logger.LogError($"Phrasal verb {phrasalVerbItem.Name} is null");
                continue;
            }

            await _phrasalVerbRepository.AddAsync(phrasalVerbEntity, cancellationToken);
        }

    }

    public async Task ProcessAWL(CancellationToken cancellationToken)
    {

    }
    public async Task ProcessNGSL(CancellationToken cancellationToken)
    {

    }

    public async Task ProcessAllWords(CancellationToken cancellationToken)
    {

    }
}
