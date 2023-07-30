﻿using Microsoft.Extensions.Logging;
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
    private readonly IHttpClientFactory _httpClientFactory;

    public App(
        ILogger<App> logger,
        IOptions<MongoDBOptions> mongoDBOptions,
        IOptions<AppOptions> appOptions,
        IPhrasalVerbRepository phrasalVerbRepository,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _mongoDBOptions = mongoDBOptions.Value;
        _appOptions = appOptions.Value;
        _phrasalVerbRepository = phrasalVerbRepository;
        _httpClientFactory = httpClientFactory;
    }   

    public async Task ProcessPhrasalVerbs(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Start processing phrasal verbs");

        if (! await _phrasalVerbRepository.IsEmpty(cancellationToken))
        {
            _logger.LogInformation($"Phrasal verbs are already in the DB. Processing stopped.");
            return;
        }

        try
        {
            var httpClient = _httpClientFactory.CreateClient();

            var contentStream = await httpClient.GetStreamAsync(_appOptions.PhrasalVerbsPath);

            var phrasalVerbs = JsonDocument.Parse(contentStream, new JsonDocumentOptions { });

            foreach (var phrasalVerbItem in phrasalVerbs.RootElement.EnumerateObject())
            {
                var phrasalVerbValue = phrasalVerbItem.Value;

                var phrasalVerbEntity = phrasalVerbValue.Deserialize<PhrasalVerb>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (phrasalVerbEntity == null)
                {
                    _logger.LogError($"Phrasal verb {phrasalVerbItem.Name} is null");
                    continue;
                }

                phrasalVerbEntity.PrasalVerb = phrasalVerbItem.Name;

                await _phrasalVerbRepository.AddAsync(phrasalVerbEntity, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while processing phrasal verbs");
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
