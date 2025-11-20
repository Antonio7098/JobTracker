using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc.Testing;

namespace JobTracker.Api.AcceptanceTests.Hooks;

[Binding]
public class TestHooks
{
    private readonly ScenarioContext _scenarioContext;
    private TestWebApplicationFactory? _factory;
    private HttpClient? _client;
    
    public TestHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _factory = new TestWebApplicationFactory();

        _client = _factory.CreateClient();

        _scenarioContext["Factory"] = _factory;
        _scenarioContext["HttpClient"] = _client;
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }
}
