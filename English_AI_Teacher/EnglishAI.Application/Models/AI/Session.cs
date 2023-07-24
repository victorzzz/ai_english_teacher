using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.Models.AI;

public record Session
{
    public IList<SessionItem> Items { get; init; } = new List<SessionItem>();

    public void AddUserItem(string content) => Items.Add(new SessionItem { Role = SessionItem.UserRole, Content = content });
    public void AddSystemItem(string content) => Items.Add(new SessionItem { Role = SessionItem.SystemRole, Content = content });
    public void AddAssystantItem(string content) => Items.Add(new SessionItem { Role = SessionItem.AssistantRole, Content = content });
}
