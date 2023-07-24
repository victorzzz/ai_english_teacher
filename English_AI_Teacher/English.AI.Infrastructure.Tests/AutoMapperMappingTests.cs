using AutoMapper;
using EnglishAI.Application.Models.AI;
using EnglishAI.Infrastructure;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenAI_API.Chat;

namespace English.AI.Infrastructure.Tests;

// Test class to test AutoMapper mappings
// MSTests are used here
[TestClass]
public class AutoMapperMappingTests
{
    // Test method to test AutoMapper configuration validity
    [TestMethod]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfileInfrastructure>());

        // Act
        var action = config.AssertConfigurationIsValid;

        // Assert
        action.Should().NotThrow();
    }

    // Test method to test AutoMapper mapping from Session to ChatRequest
    [TestMethod]
    public void AutoMapper_Session_To_ChatRequest()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfileInfrastructure>());
        var mapper = config.CreateMapper();
        var session = new Session();
        session.AddSystemItem("Hello");
        session.AddUserItem("Hi");
        session.AddAssystantItem("How are you?");

        // Act
        var chatRequest = mapper.Map<ChatRequest>(session);

        // Assert
        chatRequest.Should().NotBeNull();
        chatRequest.Messages.Should().NotBeNull();
        chatRequest.Messages.Should().HaveSameCount(session.Items);
        chatRequest.Messages.Select(m => new SessionItem { Role = m.Name, Content = m.Content }).Should().BeEquivalentTo(session.Items);
    }

    // Test method to test AutoMapper mapping from SessionItem to ChatMessage
    [TestMethod]
    public void AutoMapper_SessionItem_To_ChatMessage()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfileInfrastructure>());
        var mapper = config.CreateMapper();
        var sessionItem = new SessionItem() { Content = "Hello", Role = SessionItem.SystemRole };

        // Act
        var chatMessage = mapper.Map<ChatMessage>(sessionItem);

        // Assert
        chatMessage.Name.Should().Be(sessionItem.Role);
        chatMessage.Content.Should().Be(sessionItem.Content);
    }

    // Test method to test AutoMapper mapping from ChatRequest to Session
    [TestMethod]
    public void AutoMapper_ChatRequest_To_Session()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfileInfrastructure>());
        var mapper = config.CreateMapper();
        var chatRequest = new ChatRequest()
        {
            Messages = new List<ChatMessage>()
        };

        chatRequest.Messages.Add(new ChatMessage { Name = SessionItem.SystemRole, Content = "Hello" });
        chatRequest.Messages.Add(new ChatMessage { Name = SessionItem.UserRole, Content = "Hi" });
        chatRequest.Messages.Add(new ChatMessage { Name = SessionItem.AssistantRole, Content = "How are you?" });

        // Act
        var session = mapper.Map<Session>(chatRequest);

        // Assert
        session.Should().NotBeNull();
        session.Items.Should().NotBeNull();
        session.Items.Should().HaveSameCount(chatRequest.Messages);
        session.Items.Should().BeEquivalentTo(chatRequest.Messages.Select(m => new SessionItem { Role = m.Name, Content = m.Content }));
    }

    // Test method to test AutoMapper mapping from ChatMessage to SessionItem
    [TestMethod]
    public void AutoMapper_ChatMessage_To_SessionItem()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfileInfrastructure>());
        var mapper = config.CreateMapper();
        var chatMessage = new ChatMessage { Name = SessionItem.SystemRole, Content = "Hello" };

        // Act
        var sessionItem = mapper.Map<SessionItem>(chatMessage);

        // Assert
        sessionItem.Role.Should().Be(chatMessage.Name);
        sessionItem.Content.Should().Be(chatMessage.Content);
    }
}
