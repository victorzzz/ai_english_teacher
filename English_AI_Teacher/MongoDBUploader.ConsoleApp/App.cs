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
using System.Text.Json.Nodes;

namespace MongoDBUploader.ConsoleApp;

public class App
{
    private readonly ILogger<App> _logger;
    private readonly MongoDBOptions _mongoDBOptions;
    private readonly AppOptions _appOptions;
    private readonly IPhrasalVerbRepository _phrasalVerbRepository;
    private readonly IIrregularVerbRepository _irregularVerbRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public App(
        ILogger<App> logger,
        IOptions<MongoDBOptions> mongoDBOptions,
        IOptions<AppOptions> appOptions,
        IPhrasalVerbRepository phrasalVerbRepository,
        IIrregularVerbRepository irregularVerbRepository,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _mongoDBOptions = mongoDBOptions.Value;
        _appOptions = appOptions.Value;
        _phrasalVerbRepository = phrasalVerbRepository;
        _irregularVerbRepository = irregularVerbRepository;
        _httpClientFactory = httpClientFactory;
    }   

    public async Task ProcessPhrasalVerbs(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start processing phrasal verbs");

        if (! await _phrasalVerbRepository.IsEmpty(cancellationToken))
        {
            _logger.LogInformation("Phrasal verbs are already in the DB. Processing stopped.");
            return;
        }

        try
        {
            using var phrasalVerbs = await DownloadAndParseJsonDocument(_appOptions.PhrasalVerbsPath, cancellationToken);

            foreach (var phrasalVerbItem in phrasalVerbs.RootElement.EnumerateObject())
            {
                var phrasalVerbValue = phrasalVerbItem.Value;

                var phrasalVerbEntity = phrasalVerbValue.Deserialize<PhrasalVerb>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (phrasalVerbEntity == null)
                {
                    _logger.LogWarning("Deserialized value for phrasal verb {phrasalVerb} is null. Ignoring ...", phrasalVerbItem.Name);
                    continue;
                }

                phrasalVerbEntity.PrasalVerb = phrasalVerbItem.Name;

                await _phrasalVerbRepository.AddAsync(phrasalVerbEntity, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing phrasal verbs");
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

    public async Task ProcessIrregularVerbs(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start processing irregular verbs");

        if (!await _irregularVerbRepository.IsEmpty(cancellationToken))
        {
            _logger.LogInformation("Irregular verbs are already in the DB. Processing stopped.");
            return;
        }

        try
        {
            using var irregularVerbs = await DownloadAndParseJsonDocument(_appOptions.IrregularVerbsPath, cancellationToken);

            foreach (var irregularVerbItem in irregularVerbs.RootElement.EnumerateObject())
            {
                var irregularVerbValue = irregularVerbItem.Value;

                if (irregularVerbValue.ValueKind != JsonValueKind.Array)
                {
                    _logger.LogWarning("Value for irregular verb {irregularVerb} is not array. Ignoring...", irregularVerbItem.Name);
                    continue;
                }

                var arrayEnumerator = irregularVerbValue.EnumerateArray();
                if (!arrayEnumerator.MoveNext())
                {
                    _logger.LogWarning("Value for irregular verb {irregularVerb} is empty. Ignoring...", irregularVerbItem.Name);
                    continue;
                }

                var firstElement = arrayEnumerator.Current;

                var irregularVerbEntity = firstElement.Deserialize<IrregularVerb>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (irregularVerbEntity == null)
                {
                    _logger.LogWarning("Deserialized value for irregular verb {irregularVerb} is null. Ignoring ...", irregularVerbItem.Name);
                    continue;
                }

                irregularVerbEntity.BaseForm = irregularVerbItem.Name;

                await _irregularVerbRepository.AddAsync(irregularVerbEntity, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing irregular verbs");
        }
    }

    public async Task ProcessIdioms(CancellationToken cancellationToken)
    {

    }

    private async Task<JsonDocument> DownloadAndParseJsonDocument(string url, CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        await using var contentStream = await httpClient.GetStreamAsync(url, cancellationToken);

       return JsonDocument.Parse(contentStream, new JsonDocumentOptions { });
    }
}
