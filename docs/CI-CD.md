# CI/CD Pipeline Documentation

## Overview

This project uses **GitHub Actions** for Continuous Integration and testing. Every code change is automatically built and tested across multiple platforms to ensure quality and compatibility.

**Pipeline Status:** [![CI Build and Test](https://github.com/Antonio7098/JobTracker/actions/workflows/ci-build.yaml/badge.svg)](https://github.com/Antonio7098/JobTracker/actions/workflows/ci-build.yaml)

---

## 📋 Table of Contents

- [What Triggers the Workflow](#what-triggers-the-workflow)
- [Pipeline Jobs](#pipeline-jobs)
- [How to View Build Logs](#how-to-view-build-logs)
- [Troubleshooting Failures](#troubleshooting-failures)
- [Configuration Reference](#configuration-reference)

---

## What Triggers the Workflow

The CI pipeline runs automatically in the following scenarios:

### 1. **Push to Main or Sprint Branch**
```yaml
on:
  push:
    branches:
      - main
      - sprint-09-ci-cd-automation
```
- Triggers when commits are pushed directly to `main` or `sprint-09-ci-cd-automation`
- Ensures the main codebase is always validated

### 2. **Pull Requests Targeting Main**
```yaml
on:
  pull_request:
    branches:
      - main
```
- Triggers when a Pull Request is opened, updated, or synchronized
- PR cannot be merged until all checks pass
- Provides immediate feedback to contributors

### 3. **Manual Trigger**
You can also manually trigger the workflow from the GitHub Actions tab using the "Run workflow" button.

---

## Pipeline Jobs

The workflow consists of **6 parallel jobs** organized into 3 phases:

### Phase 1: Build (2 jobs)

| Job | OS | Purpose |
|-----|-----|---------|
| `build (ubuntu-latest)` | Ubuntu Linux | Validates code compiles on Linux |
| `build (windows-latest)` | Windows | Validates code compiles on Windows |

**What it does:**
1. Checks out the source code
2. Sets up .NET 8.0 SDK
3. Caches NuGet packages for faster subsequent runs
4. Restores dependencies (`dotnet restore`)
5. Builds the solution (`dotnet build --no-restore`)

**Why it matters:** Ensures the code compiles successfully before running expensive tests.

---

### Phase 2: Unit Tests (2 jobs)

Runs **after** the build phase completes successfully.

| Job | OS | Purpose |
|-----|-----|---------|
| `unit_tests (ubuntu-latest)` | Ubuntu Linux | Runs unit tests on Linux |
| `unit_tests (windows-latest)` | Windows | Runs unit tests on Windows |

**What it does:**
1. Checks out the source code
2. Sets up .NET 8.0 SDK
3. Restores dependencies and builds the project
4. Runs unit tests (`dotnet test JobTracker.Api.Tests`)
5. Collects code coverage
6. Publishes test results to GitHub UI

**Permissions Required:**
- `checks: write` - To publish test results as check runs
- `contents: read` - To read the repository code

---

### Phase 3: Acceptance Tests (2 jobs)

Runs **in parallel** with unit tests, after the build phase completes.

| Job | OS | Purpose |
|-----|-----|---------|
| `acceptance_tests (ubuntu-latest)` | Ubuntu Linux | Runs BDD scenarios on Linux |
| `acceptance_tests (windows-latest)` | Windows | Runs BDD scenarios on Windows |

**What it does:**
1. Checks out the source code
2. Sets up .NET 8.0 SDK
3. Restores dependencies and builds the project
4. Runs acceptance tests (`dotnet test JobTracker.Api.AcceptanceTests`)
5. Collects code coverage
6. Publishes test results to GitHub UI

**Test Framework:** SpecFlow (BDD) with xUnit

---

## How to View Build Logs

### Option 1: From a Pull Request

1. Open your Pull Request on GitHub
2. Scroll to the **"Checks"** section at the bottom
3. Click on **"CI Build and Test"**
4. You'll see all 6 jobs listed:
   - Build (ubuntu-latest)
   - Build (windows-latest)
   - Unit Tests (ubuntu-latest)
   - Unit Tests (windows-latest)
   - Acceptance Tests (ubuntu-latest)
   - Acceptance Tests (windows-latest)
5. Click on any job to view detailed logs

### Option 2: From the Actions Tab

1. Go to the repository on GitHub
2. Click the **"Actions"** tab
3. Find your workflow run in the list
4. Click on it to view all jobs
5. Click on a specific job to see step-by-step logs

### Option 3: Test Results Report

After a workflow completes, test results are published as **Check Runs**:

1. Go to your PR or commit
2. Click **"Checks"** tab
3. Look for:
   - **Unit Tests Report**
   - **Acceptance Tests Report**
4. Click to see:
   - Pass/Fail summary
   - Individual test results
   - Execution time
   - Error details for failed tests

---

## Troubleshooting Failures

### 🔴 Build Failures

**Symptom:** Red ❌ next to the Build job

**Common Causes:**
1. **Compilation errors**
   - Missing using statements
   - Type mismatches
   - Syntax errors
   
   **How to fix:** Check the build log for the specific error message. Fix it locally, then push again.

2. **Missing dependencies**
   - NuGet package not restored
   - Version conflicts
   
   **How to fix:** Run `dotnet restore` locally to identify the issue.

3. **Platform-specific issues**
   - Failed on Windows but passed on Ubuntu (or vice versa)
   
   **How to fix:** Check for file path issues (e.g., `/` vs `\`), case-sensitive file names, or OS-specific dependencies.

---

### 🔴 Unit Test Failures

**Symptom:** Red ❌ next to the Unit Tests job

**Common Causes:**
1. **Test assertion failed**
   - Expected value doesn't match actual value
   
   **How to fix:**
   - Click the job to see which test failed
   - View the test output and error message
   - Run the failing test locally: `dotnet test --filter FullyQualifiedName~TestName`
   - Fix the code or update the test assertion

2. **Exception during test execution**
   - NullReferenceException
   - Setup/teardown issues
   
   **How to fix:** Check the stack trace in the logs to identify the failing line.

3. **Flaky test (passes locally, fails in CI)**
   - Time-dependent logic
   - Uninitialized state
   - Concurrency issues
   
   **How to fix:** Add proper setup/cleanup, avoid `DateTime.Now` in tests, use deterministic test data.

---

### 🔴 Acceptance Test Failures

**Symptom:** Red ❌ next to the Acceptance Tests job

**Common Causes:**
1. **Gherkin step not implemented**
   ```
   No matching step definition found for one or more steps
   ```
   **How to fix:** Implement the missing step definition in `EmployerManagementSteps.cs`.

2. **API behavior changed**
   - Endpoint returns different status code
   - Response body structure changed
   
   **How to fix:** Update the step definitions or feature file to match the new behavior.

3. **WebApplicationFactory setup issues**
   - Database not seeded correctly
   - Configuration missing
   
   **How to fix:** Check the `BeforeScenario` hook in `TestHooks.cs`.

---

### 🟡 Cache Warnings

**Symptom:** Yellow warning about cache

**Common Causes:**
- Cache key changed (e.g., new package added)
- Cache size limit exceeded

**Impact:** Slower build (cache miss), but won't fail the workflow.

**How to fix:** Usually no action needed. The cache will rebuild automatically.

---

## Configuration Reference

### Environment Variables

| Variable | Value | Purpose |
|----------|-------|---------|
| `DOTNET_VERSION` | `8.0.x` | .NET SDK version used across all jobs |

### Caching Strategy

**What's cached:** NuGet packages (`~/.nuget/packages`)

**Cache Key:**
```yaml
${{ runner.os }}-nuget-${{ hashFiles('**/packages.lock.json', '**/*.csproj') }}
```

**Cache Invalidation:**
- If any `.csproj` file changes
- If `packages.lock.json` changes
- Separate caches for Ubuntu and Windows

**Benefits:**
- Faster dependency restoration (cache hit: ~5 seconds vs ~30 seconds)
- Reduced network bandwidth

---

## Best Practices for Contributors

### ✅ Before Pushing Code

1. **Run tests locally:**
   ```bash
   dotnet test
   ```
   
2. **Build the project:**
   ```bash
   dotnet build
   ```

3. **Fix any warnings** - CI treats warnings as potential issues.

### ✅ If CI Fails

1. **Don't force-push to fix typos** - Amend and force-push only if necessary
2. **Read the error logs carefully** - They usually tell you exactly what's wrong
3. **Reproduce the failure locally** - CI shouldn't catch anything you can't catch locally
4. **Ask for help if stuck** - Tag the PR with `help-wanted`

### ✅ When Adding Dependencies

1. **Update both test projects** if needed
2. **Let the cache rebuild** - First run after adding dependencies will be slower
3. **Check compatibility** with .NET 8.0

---

## Pipeline Performance

**Typical execution time:**

| Phase | Duration (cache hit) | Duration (cache miss) |
|-------|---------------------|----------------------|
| Build (per OS) | ~30 seconds | ~1 minute |
| Unit Tests (per OS) | ~45 seconds | ~1 minute 15 seconds |
| Acceptance Tests (per OS) | ~50 seconds | ~1 minute 20 seconds |

**Total (all 6 jobs in parallel):** ~1-2 minutes

---

## Future Improvements

Potential enhancements to the CI/CD pipeline:

- [ ] Add code coverage reporting (Codecov or Coverlet)
- [ ] Implement CD (Continuous Deployment) to staging environment
- [ ] Add security scanning (Dependabot, SAST tools)
- [ ] Add performance benchmarking
- [ ] Implement branch protection rules requiring CI to pass

---

## Questions?

If you have questions about the CI/CD setup, please:
1. Check this document first
2. Review the workflow file: `.github/workflows/ci-build.yaml`
3. Open an issue tagged `ci-cd`
