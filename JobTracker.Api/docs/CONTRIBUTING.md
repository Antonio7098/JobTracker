# Contributing Guide

This guide explains the project's standards for commit messages, pull requests, and versioning. Following these conventions keeps our Git history clean, enables automation, and makes it easier to understand the project's evolution.

---

## Table of Contents

- [TL;DR (Quick Reference)](#tldr-quick-reference)
- [Branch Naming](#branch-naming)
- [Conventional Commits](#conventional-commits)
- [Writing Commit Messages](#writing-commit-messages)
- [Creating Pull Requests](#creating-pull-requests)
- [Semantic Versioning](#semantic-versioning)

---

## TL;DR (Quick Reference)

**Commit Format:**
```
type(scope): short description
```

**Common Types:**
- `feat:` - New feature → MINOR version bump
- `fix:` - Bug fix → PATCH version bump
- `docs:` - Documentation only → No bump
- `chore:` - Maintenance/housekeeping → No bump
- `refactor:` - Code restructuring → No bump

**Version Bumps:**
- Breaking change (`feat!:`, `fix!:`) → **MAJOR** (1.0.0 → 2.0.0)
- New feature (`feat:`) → **MINOR** (0.1.0 → 0.2.0)
- Bug fix (`fix:`, `perf:`) → **PATCH** (0.1.0 → 0.1.1)
- Other (`docs:`, `chore:`, `refactor:`) → **No bump**

**Commit Rules:**
- Use imperative mood: "add feature" not "added feature"
- Don't capitalize first letter
- No period at the end
- Max 72 characters

**Examples:**
```bash
feat(api): add employer search endpoint
fix(validation): correct email regex pattern
docs: update installation instructions
chore: bump Entity Framework to 8.0.1
```

---

## Branch Naming

Branch names should follow the same convention as commits for consistency.

### Format

```
<type>/<brief-description>
```

- Use **lowercase** and **hyphens** (kebab-case)
- Keep it short but descriptive
- Match the type to your planned commits

### Examples

```bash
# Feature branches
feat/employer-search-endpoint
feat/add-authentication

# Bug fix branches
fix/null-reference-in-update
fix/validation-error-messages

# Documentation branches
docs/add-api-examples
docs/update-architecture-diagram

# Refactoring branches
refactor/extract-mapping-logic
refactor/simplify-validation

# Chore/maintenance branches
chore/update-dependencies
chore/reorganize-project-structure

# Sprint branches (if working on a full sprint)
sprint-06
sprint-07
```

### Creating a Branch

```bash
# Create and switch to a new branch
git checkout -b feat/employer-search-endpoint

# Or with newer Git syntax
git switch -c feat/employer-search-endpoint
```

### Branch Workflow

1. **Create branch:** `git checkout -b feat/new-feature`
2. **Make commits:** Follow conventional commit format
3. **Push branch:** `git push -u origin feat/new-feature`
4. **Create PR:** Use the PR template
5. **Merge:** Squash or merge commits as appropriate
6. **Delete branch:** Clean up after merge

---

## Conventional Commits

We use the **Conventional Commits** specification for all commit messages. This creates a standardized, machine-readable Git history.

### Commit Message Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

- **Header (Required):** `<type>(<scope>): <subject>`
- **Body (Optional):** Detailed explanation of the change
- **Footer (Optional):** References to issues, breaking changes, etc.

### Commit Types

| Type | Purpose | Version Impact | Example |
|------|---------|----------------|---------|
| `feat:` | New feature for the user | MINOR bump | `feat: add email validation to employer creation` |
| `fix:` | Bug fix | PATCH bump | `fix: prevent null reference error in update endpoint` |
| `docs:` | Documentation changes only | None | `docs: update API usage examples in README` |
| `chore:` | Maintenance tasks, housekeeping | None | `chore: update NuGet dependencies` |
| `refactor:` | Code restructuring without behavior change | None | `refactor: extract mapping logic to separate class` |
| `style:` | Formatting, whitespace (not CSS) | None | `style: fix indentation in Program.cs` |
| `test:` | Adding or updating tests | None | `test: add unit tests for employer repository` |
| `perf:` | Performance improvements | PATCH bump | `perf: add database index on employer name` |
| `build:` | Build system or dependencies | None | `build: upgrade to .NET 9` |
| `ci:` | CI/CD pipeline changes | None | `ci: add automated testing workflow` |

### Scope (Optional)

Add a scope in parentheses to specify which part of the codebase is affected:

```bash
feat(api): add DELETE endpoint for employers
fix(validation): correct email regex pattern
docs(architecture): update persistence layer diagram
chore(deps): bump Entity Framework to 8.0.1
```

Common scopes for this project:
- `api` - API endpoints
- `validation` - Input validation
- `persistence` - Database/EF Core
- `docs` - Documentation
- `deps` - Dependencies

### Breaking Changes

Use `!` after the type (or scope) to indicate a **breaking change**. This triggers a **MAJOR** version bump:

```bash
feat!: change employer endpoint from /api/employers to /employers
refactor!: remove deprecated Status field from EmployerDto
```

You should also include a `BREAKING CHANGE:` footer explaining the impact:

```bash
feat!: change employer endpoint URL structure

BREAKING CHANGE: All employer endpoints have moved from /api/employers to /v1/employers.
Clients must update their base URL configuration.
```

---

## Writing Commit Messages

### The Header (Required)

**Format:** `<type>(<scope>): <subject>`

#### Rules for the Subject Line:
1. **Use imperative mood** - "add feature" not "added feature" or "adds feature"
2. **Don't capitalize** the first letter (lowercase)
3. **No period** at the end
4. **Max 72 characters** (ideally 50)
5. **Complete the sentence:** "If applied, this commit will _____"

#### Good Examples:

```bash
feat: add DELETE endpoint for employers
fix: prevent duplicate employer creation
docs: add installation instructions to README
chore: reorganize project folder structure
refactor: simplify employer validation logic
```

#### Bad Examples:

```bash
feat: Added a new feature.          # Wrong tense, capitalized, has period
fix: bug fix                        # Too vague
updated code                        # Missing type, too vague
feat: This commit adds validation   # Not imperative mood
```

### The Body (Optional but Recommended)

Use the body to explain **WHAT** and **WHY**, not **HOW** (the code shows the "how").

**Good body content:**
- Motivation for the change
- Contrast with previous behavior
- Context that isn't obvious from the code
- Reasoning behind implementation choices

**Example:**

```bash
feat(validation): add FluentValidation for employer DTOs

The existing Data Annotations approach was becoming insufficient for
complex validation rules, particularly for cross-field validation and
database lookups.

FluentValidation provides:
- Separation of validation logic from DTOs
- Easier unit testing of validation rules
- Async validation support for database checks
- More expressive and readable validation code

This change introduces validators for CreateEmployerDto and
UpdateEmployerDto, with rules for name format, length constraints,
and uniqueness checks.
```

### The Footer (Optional)

Use the footer for:
- Referencing issues: `Closes #123`, `Fixes #456`, `Refs #789`
- Breaking changes: `BREAKING CHANGE: <description>`
- Co-authors: `Co-authored-by: Name <email>`

**Example:**

```bash
feat: add employer search endpoint

Add a new GET endpoint that allows filtering employers by name using
a query parameter. The search is case-insensitive and matches partial
names.

Closes #42
```

### Using the Template

The project includes a commit template at `docs/templates/commit-template.md`. You can configure Git to use it:

```bash
git config commit.template docs/templates/commit-template.md
```

Now, when you run `git commit` (without `-m`), your editor will open with the template pre-filled.

---

## Creating Pull Requests

### Pull Request Title

Follow the same Conventional Commit format for PR titles:

```
feat(api): add employer search functionality
fix(validation): correct email validation regex
docs: update API documentation for Sprint 6
```

### Pull Request Description

Use the template at `docs/templates/pull-request-template.md`. A good PR description includes:

1. **Description:** Clear explanation of what the PR does and why
2. **Key Changes:** Bulleted list of the most important changes
3. **Related Sprint/Issue:** Link to the sprint or issue number
4. **How to Test:** Step-by-step manual testing instructions
5. **Checklist:** Pre-submission checklist

**Example:**

```markdown
## Description

This PR implements comprehensive input validation for all Employer endpoints using FluentValidation and adds standardized error handling using the Problem Details (RFC 7807) standard.

**Motivation:** The API currently accepts invalid data (empty names, excessively long strings) which causes database errors and returns unhelpful error messages to clients.

## Key Changes

- Add FluentValidation package and register with DI
- Create `CreateEmployerDtoValidator` and `UpdateEmployerDtoValidator`
- Implement manual validation in endpoints with 422 responses
- Add global exception handler returning 500 with Problem Details
- Update Swagger docs with error response schemas
- Update README and ARCHITECTURE docs

## Related Sprint/Issue

Completes Sprint 6 - Validation and Error Handling

## How to Manually Test

1. Start the application: `dotnet run`
2. Navigate to `/swagger`
3. Try to create an employer with an empty name - should return 422
4. Try to create an employer with a 300-character name - should return 422
5. Try to get an employer with ID 999 - should return 404
6. Create a valid employer - should return 201

## Checklist

- [x] My code follows the project's coding style and conventions
- [x] I have updated the documentation to reflect my changes
- [x] All new and existing tests pass
- [x] I have tested the changes manually using the steps above
```

### Before Submitting a PR

- [ ] All commits follow Conventional Commits format
- [ ] Code builds without errors
- [ ] Tests pass (if tests exist)
- [ ] Documentation is updated
- [ ] You've tested the changes manually
- [ ] Branch is up to date with main/master

---

## Semantic Versioning

This project follows **Semantic Versioning (SemVer)**: `MAJOR.MINOR.PATCH`

### Version Format: `MAJOR.MINOR.PATCH`

Example: `1.4.2`
- **MAJOR** = 1 → Breaking changes
- **MINOR** = 4 → New features (backward compatible)
- **PATCH** = 2 → Bug fixes (backward compatible)

### When to Increment Each Number

#### MAJOR Version (1.0.0 → 2.0.0)

Increment when you make **breaking changes** that are **not backward compatible**.

**Examples:**
- Changing an endpoint URL: `/employers` → `/v2/employers`
- Removing a field from a DTO: `EmployerDto.Status` is removed
- Changing response format: JSON → XML
- Renaming a required field: `Name` → `CompanyName`

**Commit type:** `feat!:` or `fix!:` or `refactor!:`

#### MINOR Version (0.1.0 → 0.2.0)

Increment when you add **new functionality** in a **backward compatible** way.

**Examples:**
- Adding a new endpoint: `GET /employers/search`
- Adding a new optional field to a DTO: `EmployerDto.Website`
- Adding a new feature: FluentValidation
- Adding a new query parameter: `/employers?sortBy=name`

**Commit type:** `feat:`

#### PATCH Version (0.1.0 → 0.1.1)

Increment for **backward compatible bug fixes** or **minor improvements**.

**Examples:**
- Fixing a null reference exception
- Correcting incorrect validation logic
- Fixing a typo in an error message
- Performance improvements without changing behavior

**Commit types:** `fix:`, `perf:`

### Starting Versions

- **0.x.y** → Pre-release, API may change
- **1.0.0** → First stable public release

### Setting the Project Version

Update the version in `JobTracker.Api.csproj`:

```xml
<PropertyGroup>
  <Version>0.2.0</Version>
</PropertyGroup>
```

### Creating a Git Tag

After updating the version and committing, create an annotated tag:

```bash
# Format: v<MAJOR>.<MINOR>.<PATCH>
git tag -a v0.2.0 -m "v0.2.0: Add comprehensive validation and error handling"
```

Push the tag to the remote:

```bash
git push origin v0.2.0
```

### Version Bump Decision Tree

```
Is this a breaking change?
├─ YES → MAJOR (1.0.0 → 2.0.0) - Use `feat!:` or `fix!:`
└─ NO
   ├─ Does it add new functionality?
   │  ├─ YES → MINOR (0.1.0 → 0.2.0) - Use `feat:`
   │  └─ NO
   │     ├─ Is it a bug fix or minor improvement?
   │     │  └─ YES → PATCH (0.1.0 → 0.1.1) - Use `fix:` or `perf:`
   │     └─ Is it docs/chore/refactor only?
   │        └─ NO VERSION CHANGE - Use `docs:`, `chore:`, `refactor:`
```

### Version History Example

| Version | Commit Type | Description |
|---------|-------------|-------------|
| `0.1.0` | `feat:` | Initial release with basic CRUD and persistence |
| `0.2.0` | `feat:` | Add validation and error handling |
| `0.2.1` | `fix:` | Fix null reference error in update endpoint |
| `0.3.0` | `feat:` | Add authentication and authorization |
| `1.0.0` | `feat:` | First stable release |
| `1.1.0` | `feat:` | Add search and filtering |
| `2.0.0` | `feat!:` | Change API structure (breaking change) |

---

## Quick Reference

### Branch Name Format

```bash
<type>/<brief-description>

# Examples
feat/employer-search-endpoint
fix/validation-error-messages
docs/update-architecture-diagram
```

### Commit Message Template

```bash
<type>(<scope>): <short description>

<longer description explaining what and why>

Closes #123
```

### Common Scenarios

| What You Did | Branch Name | Commit Type | Example Commit |
|--------------|-------------|-------------|----------------|
| Added a new endpoint | `feat/employer-search` | `feat:` | `feat(api): add employer search endpoint` |
| Fixed a bug | `fix/validation-error` | `fix:` | `fix(validation): correct email validation regex` |
| Updated README | `docs/add-instructions` | `docs:` | `docs: add installation instructions` |
| Refactored code structure | `refactor/extract-mapping` | `refactor:` | `refactor: extract mapping to separate class` |
| Updated dependencies | `chore/update-deps` | `chore:` | `chore(deps): bump EF Core to 8.0.1` |
| Changed endpoint URL (breaking) | `feat/change-endpoint-url` | `feat!:` | `feat!: change employers endpoint to /v1/employers` |

---

## Resources

- [Conventional Commits Specification](https://www.conventionalcommits.org/)
- [Semantic Versioning Specification](https://semver.org/)
- [How to Write a Git Commit Message](https://chris.beams.io/posts/git-commit/)
- [Commit Template](templates/commit-template.md)
- [Pull Request Template](templates/pull-request-template.md)

