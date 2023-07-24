using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EnglishAI.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBUploader.ConsoleApp;

public class App
{
    private readonly ILogger<App> _logger;
    private readonly MongoDBOptions _mongoDBOptions;
    private readonly HttpClient _httpClient;

    public App(ILogger<App> logger, IOptions<MongoDBOptions> mongoDBOptions, HttpClient httpClient)
    {
        _logger = logger;
        _mongoDBOptions = mongoDBOptions.Value;
        _httpClient = httpClient;
    }   

    public async Task ProcessPhrasalVerbs()
    {

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
