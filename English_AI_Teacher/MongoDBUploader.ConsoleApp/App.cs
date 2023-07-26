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

            var phrasalVerbEntity = new PhrasalVerbEntity
            { 
                PrasalVerb = phrasalVerbItem.Name,
                Descriptions = phrasalVerbValue.GetProperty("descriptions").Deserialize<List<string>>() ?? new List<string>(),
                Deriviates = phrasalVerbValue.GetProperty("derivatives").Deserialize<List<string>>() ?? new List<string>(),
                Examples = phrasalVerbValue.GetProperty("examples").Deserialize<List<string>>() ?? new List<string>(),  
                Synonyms = phrasalVerbValue.GetProperty("synonyms").Deserialize<List<string>>() ?? new List<string>(),
                Frequency = phrasalVerbValue.GetProperty("frequency").GetInt32()



            // get phrasal verb definition
            var phrasalVerbDefinition = phrasalVerbValue.GetProperty("definition").GetString();

            // get phrasal verb examples
            var phrasalVerbExamples = phrasalVerbValue.GetProperty("examples").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb synonyms
            var phrasalVerbSynonyms = phrasalVerbValue.GetProperty("synonyms").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb antonyms
            var phrasalVerbAntonyms = phrasalVerbValue.GetProperty("antonyms").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb synonyms
            var phrasalVerbRelatedWords = phrasalVerbValue.GetProperty("related_words").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb synonyms
            var phrasalVerbExamplesWithSynonyms = phrasalVerbValue.GetProperty("examples_with_synonyms").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb synonyms
            var phrasalVerbExamplesWithAntonyms = phrasalVerbValue.GetProperty("examples_with_antonyms").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb synonyms
            var phrasalVerbExamplesWithRelatedWords = phrasalVerbValue.GetProperty("examples_with_related_words").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();

            // get phrasal verb synonyms
            var phrasalVerbExamplesWithSynonymsAndAntonyms = phrasalVerbValue.GetProperty("examples_with_synonyms_and_antonyms").EnumerateArray()
                .Select(x => x.GetString())
                .ToList();
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
