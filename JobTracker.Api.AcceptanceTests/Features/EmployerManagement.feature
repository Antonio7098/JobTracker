Feature: EmployersManagement
    As a user I want to be able to create, update, and remove employers
    So that I can manage employers on my job tracker

Background: 
    Given that the database is empty

Scenario: A new employer is created successfully
    Given I have the details of a valid employer
    When I send a request to create that employer
    Then the response status should be 201 created
    And the employer should be retrievable from the api

Scenario: An existing employer is updated successfully
    Given an employer is retrievable from the api
    And I have the details of a valid employer I want to replace it with
    When I send a request to update that employer
    Then the response status should be 204 No Content
    And the updated employer should be retrievable from the api

Scenario: Retrieve an employer that exists
    Given an employer exists in the database
    When I send a request to retrieve that employer
    Then the response status should be 200 OK and the employer

Scenario: An existing employer is deleted
    Given an employer exists in the database
    When I send a request to delete that employer
    Then the response status should be 204 No Content
    And the deleted employer should not be retrievable from the api

Scenario: Attempting to retrieve a non-existent employer
    When I send a request to retrieve an employer with a random ID
    Then the response status should be 404 Not Found
    And the response should contain problem details

Scenario: Attempting to update a non-existent employer
    Given I have the details of a valid employer I want to replace it with
    When I send a request to update an employer with a random ID
    Then the response status should be 404 Not Found
    And the response should contain problem details

Scenario: Attempting to delete a non-existent employer
    When I send a request to delete an employer with a random ID
    Then the response status should be 404 Not Found
    And the response should contain problem details

Scenario: Complete employer lifecycle workflow
    Given I have the details of a valid employer
    When I send a request to create that employer
    Then the response status should be 201 created
    And the employer should be retrievable from the api
    When I have the details of a valid employer I want to replace it with
    And I send a request to update that employer
    Then the response status should be 204 No Content
    And the updated employer should be retrievable from the api
    When I send a request to delete that employer
    Then the response status should be 204 No Content
    And the deleted employer should not be retrievable from the api

Scenario Outline: Attempting to create an employer with various invalid data
    Given I have an invalid employer with a name of "<Name>" and a description of "<Description>"
    When I send a request to create that employer
    Then the response status should be 422 Unprocessable Content
    And the response should contain problem details

    Examples:
    |                                               Name                                                      |     Description       |
    |                                                                                                        | A valid description |
    | Very_Long_Name_aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | A valid description |
