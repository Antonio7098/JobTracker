using TechTalk.SpecFlow;
using JobTracker.Api.DTOs;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.AcceptanceTests.StepDefinitions;

[Binding]
public class EmployerManagementSteps
{
    private readonly ScenarioContext _scenarioContext;
    private HttpClient HttpClient => _scenarioContext.Get<HttpClient>("HttpClient");

    public EmployerManagementSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    // ==================== GIVEN ====================

    [Given(@"that the database is empty")]
    public void GivenThatTheDatabaseIsEmpty()
    {
        // The database is already empty because the [BeforeScenario] hook
        // creates a fresh in-memory database for each scenario
    }

    [Given(@"I have the details of a valid employer")]
    public void GivenIHaveTheDetailsOfAValidEmployer()
    {
        var employer = new CreateEmployerDto
        {
            Name = "Test Company",
            CompanyDescription = "A test company"
        };

        _scenarioContext["NewEmployer"] = employer;
    }

    [Given(@"I have an invalid employer with a name of {string} and a description of {string}")]
    public void GivenIHaveAnInvalidEmployer(string name, string description)
    {
        var newInvalidEmployer = new CreateEmployerDto
        {
            Name = name,
            CompanyDescription = description
        }; 

        _scenarioContext["NewEmployer"] = newInvalidEmployer;
    }

    [Given(@"I have the details of a valid employer I want to replace it with")]
    [When(@"I have the details of a valid employer I want to replace it with")]
    public void GivenIHaveTheDetailsOfAValidEmployerToReplaceItWith()
    {
        var updatedEmployer = new UpdateEmployerDto
        {
            Name = "Updated Company Name",
            CompanyDescription = "Updated description"
        };

        _scenarioContext["UpdatedEmployer"] = updatedEmployer;
    }

    [Given(@"an employer is retrievable from the api")]
    [Given(@"an employer exists in the database")]
    public async Task GivenAnEmployerExists()
    {
        var employer = new CreateEmployerDto
        {
            Name = "Test Company",
            CompanyDescription = "A test company"
        };

        var response = await HttpClient.PostAsJsonAsync("/employers", employer);
        response.EnsureSuccessStatusCode();

        var createdEmployer = await response.Content.ReadFromJsonAsync<EmployerDto>();
        
        _scenarioContext["EmployerId"] = createdEmployer!.Id;
        _scenarioContext["Employer"] = createdEmployer;
    }

    // ==================== WHEN ====================

    [When(@"I send a request to create that employer")]
    public async Task WhenISendARequestToCreateThatEmployer()
    {
        var employer = _scenarioContext.Get<CreateEmployerDto>("NewEmployer");

        var response = await HttpClient.PostAsJsonAsync("/employers", employer);

        _scenarioContext["Response"] = response;

        // Extract the created employer's ID for later steps
        if (response.IsSuccessStatusCode)
        {
            var createdEmployer = await response.Content.ReadFromJsonAsync<EmployerDto>();
            _scenarioContext["EmployerId"] = createdEmployer!.Id;
        }
    }

    [When(@"I send a request to retrieve that employer")]
    public async Task WhenISendARequestToRetrieveThatEmployer()
    {
        var employerId = _scenarioContext.Get<Guid>("EmployerId");

        var response = await HttpClient.GetAsync($"/employers/{employerId}");

        _scenarioContext["Response"] = response;
    }

    [When(@"I send a request to update that employer")]
    public async Task WhenISendARequestToUpdateThatEmployer()
    {
        var employerId = _scenarioContext.Get<Guid>("EmployerId");
        var updatedEmployer = _scenarioContext.Get<UpdateEmployerDto>("UpdatedEmployer");

        var response = await HttpClient.PutAsJsonAsync($"/employers/{employerId}", updatedEmployer);

        _scenarioContext["Response"] = response;
    }

    [When(@"I send a request to delete that employer")]
    public async Task WhenISendARequestToDeleteThatEmployer()
    {
        var employerId = _scenarioContext.Get<Guid>("EmployerId");

        var response = await HttpClient.DeleteAsync($"/employers/{employerId}");

        _scenarioContext["Response"] = response;
    }

    [When(@"I send a request to retrieve an employer with a random ID")]
    public async Task WhenISendARequestToRetrieveARandomEmployer()
    {
        var response = await HttpClient.GetAsync($"/employers/{Guid.NewGuid()}");

        _scenarioContext["Response"] = response;
    }

    [When(@"I send a request to update an employer with a random ID")]
    public async Task WhenISendARequestToUpdateARandomEmployer()
    {
        var updatedEmployer = new UpdateEmployerDto
        {
            Name = "Updated Company Name",
            CompanyDescription = "Updated description"
        };

        var response = await HttpClient.PutAsJsonAsync($"/employers/{Guid.NewGuid()}", updatedEmployer);

        _scenarioContext["Response"] = response;
    }

    [When(@"I send a request to delete an employer with a random ID")]
    public async Task WhenISendARequestToDeleteARandomEmployer()
    {
        var response = await HttpClient.DeleteAsync($"/employers/{Guid.NewGuid()}");

        _scenarioContext["Response"] = response;
    }

    // ==================== THEN ====================

    [Then(@"the response status should be 201 created")]
    public void ThenTheResponseStatusShouldBe201Created()
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Then(@"the response status should be {int} No Content")]
    public void ThenTheResponseStatusShouldBeNoContent(int statusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        response.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the response status should be {int} Unprocessable Content")]
    public void ThenTheResponseStatusShouldBeUnprocessableContent(int statusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        response.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the response status should be {int} OK")]
    public void ThenTheResponseStatusShouldBeOK(int statusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        response.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the response status should be {int} Not Found")]
    public void ThenTheResponseStatusShouldBeNotFound(int statusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        response.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the response status should be {int} OK and the employer")]
    public async Task ThenTheResponseShouldBeOKAndTheEmployer(int statusCode)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        
        response.StatusCode.Should().Be((HttpStatusCode)statusCode);
        
        var employer = await response.Content.ReadFromJsonAsync<EmployerDto>();
        employer.Should().NotBeNull();
    }

    [Then(@"the employer should be retrievable from the api")]
    public async Task ThenTheEmployerShouldBeRetrievableFromTheApi()
    {
        var employerId = _scenarioContext.Get<Guid>("EmployerId");

        var response = await HttpClient.GetAsync($"/employers/{employerId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var employer = await response.Content.ReadFromJsonAsync<EmployerDto>();

        employer.Should().NotBeNull();
    }

    [Then(@"the updated employer should be retrievable from the api")]
    public async Task ThenTheUpdatedEmployerShouldBeRetrievableFromTheApi()
    {
        var employerId = _scenarioContext.Get<Guid>("EmployerId");

        var response = await HttpClient.GetAsync($"/employers/{employerId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var employer = await response.Content.ReadFromJsonAsync<EmployerDto>();
        var expectedEmployer = _scenarioContext.Get<UpdateEmployerDto>("UpdatedEmployer");

        employer!.Name.Should().Be(expectedEmployer.Name);
        employer.CompanyDescription.Should().Be(expectedEmployer.CompanyDescription);
    }

    [Then(@"the deleted employer should not be retrievable from the api")]
    public async Task ThenTheDeletedEmployerShouldNotBeRetrievableFromTheApi()
    {
        var employerId = _scenarioContext.Get<Guid>("EmployerId");

        var response = await HttpClient.GetAsync($"/employers/{employerId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then(@"the response should contain problem details")]
    public async Task ThenTheResponseShouldContainProblemDetails()
    {
        var response = _scenarioContext.Get<HttpResponseMessage>("Response");
        
        // Verify Content-Type header
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/problem+json");
        
        // Deserialize the problem details
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        
        // Verify the structure
        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be((int)response.StatusCode);
        problemDetails.Title.Should().NotBeNullOrEmpty();
    }
}