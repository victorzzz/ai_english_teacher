using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.Models.AI;

public record SessionItem
{
    public static readonly string SystemRole = "system";
    public static readonly string UserRole = "user";
    public static readonly string AssistantRole = "assistant";

    public required string Role { get; init; }
    public required string Content { get; init; }
}
