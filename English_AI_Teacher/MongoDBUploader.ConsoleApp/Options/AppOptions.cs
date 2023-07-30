using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBUploader.ConsoleApp.Options;

public record AppOptions
{
    public static readonly string SectionName = "App";

    public string PhrasalVerbsPath { get; init; } = default!;

    public string AWLPath { get; init; } = default!;

    public string NGSLPath { get; init; } = default!;

    public string AllWordsPath { get; init; } = default!;

    public string IrregularVerbsPath { get; init; } = default!;

    public string IdiomsPath { get; init; } = default!;
}
