# 🎯 Sprint 09: CI/CD Automation with GitHub Actions

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** Continuous Integration (CI), Continuous Deployment (CD), GitHub Actions, YAML Configuration, Build Pipelines, Automated Testing, Workflow Triggers, Build Artifacts, Status Badges
* **Primary Goal (Must-Have):** By the end, I must be able to **create a GitHub Actions workflow that automatically builds the .NET solution, runs all unit and acceptance tests, and reports the build status on every push and pull request.**
* **Secondary Goals:**
    * Understand what CI/CD is and why it's essential in modern software development
    * Write YAML configuration files for GitHub Actions workflows
    * Configure matrix builds to test against multiple .NET versions
    * Set up build status badges in the README
    * Understand the difference between CI and CD
    * Implement a basic CD pipeline that could deploy to production

---

## ✅ Task List

### Phase 1: Understanding CI/CD Philosophy

- [ ] **Task 1: Research the History & Purpose of CI/CD**
    > *Description: Before automating anything, understand what problems CI/CD solves and why manual processes don't scale.*
    - [ ] **Sub-task 1.1:** Research the origins of Continuous Integration (hint: it emerged from Extreme Programming in the late 1990s)
    - [ ] **Sub-task 1.2:** Read about "integration hell" - the problem that CI was designed to solve
    - [ ] **Sub-task 1.3:** Write a comment explaining: "What would happen if 5 developers all worked on separate branches for 2 weeks and then tried to merge everything at once?"
    - [ ] **Sub-task 1.4:** Research: "What's the difference between Continuous Integration, Continuous Delivery, and Continuous Deployment?"
    - [ ] **Sub-task 1.5:** Add a comment explaining: "Why is it important to run tests automatically on every commit, rather than relying on developers to run them locally?"

- [ ] **Task 2: Explore GitHub Actions Architecture**
    > *Description: GitHub Actions has its own terminology and mental model. Understand the building blocks before writing YAML.*
    - [ ] **Sub-task 2.1:** Research and document the hierarchy: `Workflow → Jobs → Steps → Actions`
    - [ ] **Sub-task 2.2:** Understand what "runners" are (GitHub-hosted vs self-hosted)
    - [ ] **Sub-task 2.3:** Browse the [GitHub Actions Marketplace](https://github.com/marketplace?type=actions) and find actions for:
        - Checking out code
        - Setting up .NET
        - Running tests
        - Publishing test results
    - [ ] **Sub-task 2.4:** Add a comment explaining: "What's the difference between a 'workflow' and an 'action'?"

### Phase 2: Your First CI Workflow

- [ ] **Task 3: Create the Workflow Directory Structure**
    > *Description: GitHub Actions looks for workflows in a specific location in your repository.*
    - [ ] **Sub-task 3.1:** Create the `.github/workflows/` directory in the root of your repository
    - [ ] **Sub-task 3.2:** Research: "Why does GitHub look for workflows in `.github/workflows/` specifically? What other special directories exist in `.github/`?"
    - [ ] **Sub-task 3.3:** Add a `.gitignore` rule if needed (though workflow files should be committed)

- [ ] **Task 4: Write Your First Workflow - Build Only**
    > *Description: Start simple. Create a workflow that just builds the project without running tests.*
    - [ ] **Sub-task 4.1:** Create `ci-build.yml` in `.github/workflows/`
    - [ ] **Sub-task 4.2:** Configure the workflow to trigger on:
        - Push to `main` branch
        - Pull requests targeting `main`
    - [ ] **Sub-task 4.3:** Add a job that:
        - Checks out the code (`actions/checkout@v4`)
        - Sets up .NET 8 SDK (`actions/setup-dotnet@v4`)
        - Restores dependencies (`dotnet restore`)
        - Builds the solution (`dotnet build --no-restore`)
    - [ ] **Sub-task 4.4:** Research: "What does the `--no-restore` flag do? Why is it good practice to separate restore and build steps?"
    - [ ] **Sub-task 4.5:** Commit this workflow, push it, and observe it run in the GitHub Actions tab

- [ ] **Task 5: Understand YAML Syntax & Workflow Execution**
    > *Description: YAML is whitespace-sensitive and has its own quirks. Debug any syntax errors.*
    - [ ] **Sub-task 5.1:** If the workflow fails, examine the logs in GitHub Actions and identify the error
    - [ ] **Sub-task 5.2:** Research YAML basics: indentation rules, lists vs dictionaries, multiline strings
    - [ ] **Sub-task 5.3:** Add a comment explaining: "What's the difference between `run` and `uses` in a workflow step?"
    - [ ] **Sub-task 5.4:** Experiment: Add an `echo` command to print a message during the build

### Phase 3: Adding Automated Tests

- [ ] **Task 6: Extend Workflow to Run Unit Tests**
    > *Description: Now that building works, add test execution to catch bugs automatically.*
    - [ ] **Sub-task 6.1:** Add a new step that runs `dotnet test` after the build
    - [ ] **Sub-task 6.2:** Configure the test command to:
        - Run all test projects in the solution
        - Generate test result files (TRX format)
        - Collect code coverage (optional: `--collect:"XPlat Code Coverage"`)
    - [ ] **Sub-task 6.3:** Research: "What happens to the workflow if tests fail? Does the build still pass?"
    - [ ] **Sub-task 6.4:** Intentionally break a test, commit, and observe the workflow fail

- [ ] **Task 7: Publish Test Results**
    > *Description: Make test results visible directly in the GitHub UI, not just in logs.*
    - [ ] **Sub-task 7.1:** Install and use the `dorny/test-reporter@v1` action to publish test results
    - [ ] **Sub-task 7.2:** Configure it to read the TRX files generated by `dotnet test`
    - [ ] **Sub-task 7.3:** Fix the broken test from Task 6 and observe the green checkmark
    - [ ] **Sub-task 7.4:** Research: "What's the advantage of publishing test results vs just reading console output in the logs?"

- [ ] **Task 8: Run Acceptance Tests in CI**
    > *Description: Your acceptance tests need special consideration because they spin up an in-memory API.*
    - [ ] **Sub-task 8.1:** Verify that acceptance tests run successfully in CI with the same `dotnet test` command
    - [ ] **Sub-task 8.2:** If tests fail due to environment differences, add a comment explaining: "What environmental factors might cause tests to pass locally but fail in CI?"
    - [ ] **Sub-task 8.3:** Research: "Should acceptance tests run in the same job as unit tests, or in a separate job? What are the trade-offs?"
    - [ ] **Sub-task 8.4:** (Optional) Create separate jobs for unit tests and acceptance tests that run in parallel

### Phase 4: Matrix Builds & Advanced Configuration

- [ ] **Task 9: Test Against Multiple .NET Versions**
    > *Description: Use a build matrix to ensure your code works on different .NET versions.*
    - [ ] **Sub-task 9.1:** Research the `strategy.matrix` feature in GitHub Actions
    - [ ] **Sub-task 9.2:** Configure your workflow to run tests against both .NET 8.0 and .NET 9.0 (if available)
    - [ ] **Sub-task 9.3:** Add a comment explaining: "Why would we want to test against multiple framework versions? When would this catch bugs?"
    - [ ] **Sub-task 9.4:** Observe the workflow create multiple parallel jobs (one per matrix combination)

- [ ] **Task 10: Add Build Caching for Speed**
    > *Description: Downloading NuGet packages on every build is slow. Cache them.*
    - [ ] **Sub-task 10.1:** Research the `actions/cache@v4` action
    - [ ] **Sub-task 10.2:** Configure caching for NuGet packages (cache key should include OS and `packages.lock.json` hash)
    - [ ] **Sub-task 10.3:** Run the workflow twice and compare execution times (first run: cache miss, second run: cache hit)
    - [ ] **Sub-task 10.4:** Add a comment explaining: "What happens if the cache key changes (e.g., a new package is added)? Does the workflow break?"

- [ ] **Task 11: Add Environment Variables & Secrets**
    > *Description: Learn to handle configuration and sensitive data in workflows.*
    - [ ] **Sub-task 11.1:** Research how to set environment variables in GitHub Actions (`env` keyword)
    - [ ] **Sub-task 11.2:** Extract the .NET version into a workflow-level environment variable
    - [ ] **Sub-task 11.3:** Research GitHub Secrets: how to store them, how to reference them
    - [ ] **Sub-task 11.4:** Add a comment explaining: "When would you use a GitHub Secret vs a public environment variable?"
    - [ ] **Sub-task 11.5:** (Optional) If your API has any configuration (e.g., connection strings), set them as environment variables in the workflow

### Phase 5: Status Badges & Documentation

- [ ] **Task 12: Add Build Status Badge to README**
    > *Description: Make the build status visible to anyone viewing the repository.*
    - [ ] **Sub-task 12.1:** Navigate to your workflow in GitHub Actions and click "Create status badge"
    - [ ] **Sub-task 12.2:** Copy the Markdown snippet and add it to the top of your root `README.md`
    - [ ] **Sub-task 12.3:** Research: "What information does a status badge show? What are the possible states (passing, failing, etc.)?"
    - [ ] **Sub-task 12.4:** Commit and verify the badge appears on your GitHub repository page

- [ ] **Task 13: Document the CI/CD Pipeline**
    > *Description: Explain how the pipeline works for future contributors.*
    - [ ] **Sub-task 13.1:** Create `docs/CI-CD.md` explaining:
        - What triggers the workflow
        - What each job does
        - How to view build logs
        - How to troubleshoot failures
    - [ ] **Sub-task 13.2:** Update root `README.md` to link to this documentation
    - [ ] **Sub-task 13.3:** Add a section explaining: "What should a developer do if their PR fails the CI checks?"

### Phase 6: Introduction to Continuous Deployment

- [ ] **Task 14: Design a Deployment Workflow (Conceptual)**
    > *Description: Understand what Continuous Deployment would look like, even if you don't implement it yet.*
    - [ ] **Sub-task 14.1:** Research the difference between:
        - Continuous Integration (build + test)
        - Continuous Delivery (build + test + ready to deploy)
        - Continuous Deployment (build + test + automatic deployment)
    - [ ] **Sub-task 14.2:** Write pseudocode or comments outlining a CD workflow that would:
        - Run on pushes to `main` only (not PRs)
        - Build a Docker image of the API
        - Push it to a container registry
        - Deploy it to a cloud provider (AWS, Azure, etc.)
    - [ ] **Sub-task 14.3:** Add a comment explaining: "Why might a team choose Continuous Delivery (manual approval before deploy) instead of Continuous Deployment (automatic deploy)?"
    - [ ] **Sub-task 14.4:** Research: "What are 'deployment gates' or 'approval steps' in a CD pipeline?"

- [ ] **Task 15: Create a Manual Deployment Workflow (Optional)**
    > *Description: If you want hands-on CD experience without cloud costs, create a workflow with manual approval.*
    - [ ] **Sub-task 15.1:** Create `cd-deploy.yml` that only triggers via `workflow_dispatch` (manual trigger)
    - [ ] **Sub-task 15.2:** Add jobs that simulate deployment steps:
        - Build Docker image (just the Dockerfile, don't push anywhere)
        - Run a smoke test (ping a health endpoint)
        - Add a comment: "In a real pipeline, what would the next steps be?"
    - [ ] **Sub-task 15.3:** Trigger this workflow manually from the GitHub Actions tab

### Phase 7: Workflow Hygiene & Best Practices

- [ ] **Task 16: Implement Workflow Security Best Practices**
    > *Description: Workflows can be a security risk if not configured carefully.*
    - [ ] **Sub-task 16.1:** Research: "What are the risks of using `pull_request_target` vs `pull_request` as a trigger?"
    - [ ] **Sub-task 16.2:** Ensure your workflow uses pinned action versions (e.g., `actions/checkout@v4` not `@main`)
    - [ ] **Sub-task 16.3:** Add a comment explaining: "Why is it dangerous to use `@main` or `@latest` for action versions?"
    - [ ] **Sub-task 16.4:** Research: "What is a 'supply chain attack' in the context of CI/CD?"

- [ ] **Task 17: Add Workflow Linting & Validation**
    > *Description: Catch YAML syntax errors before pushing.*
    - [ ] **Sub-task 17.1:** Install the `actionlint` tool locally or use the VS Code extension
    - [ ] **Sub-task 17.2:** Run it against your workflow files and fix any warnings
    - [ ] **Sub-task 17.3:** (Optional) Add a pre-commit hook that runs `actionlint` automatically

### Phase 8: Commit Strategy & Retrospective

- [ ] **Task 18: Plan Your Commits**
    > *Description: CI/CD setup involves multiple iterations. Plan logical commit boundaries.*
    - [ ] **Sub-task 18.1:** Review the commit strategy from the sprint instructions
    - [ ] **Sub-task 18.2:** Identify at least 4 logical commits for this sprint:
        1. **ci: add basic build workflow with .NET setup**
           - Initial workflow file, build-only job
        2. **ci: add automated test execution and reporting**
           - Test steps, test result publishing, parallel test jobs
           - Use body to explain test coverage strategy
        3. **ci: add matrix builds and caching for performance**
           - Multi-version testing, NuGet cache
           - Use body to explain caching strategy and performance gains
        4. **docs: add CI/CD documentation and status badge**
           - CI-CD.md guide, README badge, troubleshooting tips
    - [ ] **Sub-task 18.3:** Write down your planned commit messages before finalizing

- [ ] **Task 19: Update Project Documentation & Version**
    > *Description: Document your new automated infrastructure.*
    - [ ] **Sub-task 19.1:** Update root `README.md` to:
        - Add build status badge at the top
        - Mention that the project has automated CI
        - Link to `docs/CI-CD.md`
    - [ ] **Sub-task 19.2:** Create `docs/CI-CD.md` with comprehensive CI/CD documentation
    - [ ] **Sub-task 19.3:** Update `docs/ARCHITECTURE.md` to add a "CI/CD Pipeline" section
    - [ ] **Sub-task 19.4:** Increment version in `.csproj` following SemVer:
        - Since this adds CI/CD infrastructure (feature), increment MINOR version (e.g., 0.8.0 → 0.9.0)
    - [ ] **Sub-task 19.5:** Create a conventional commit: `ci: implement GitHub Actions CI/CD pipeline`
    - [ ] **Sub-task 19.6:** Create an annotated Git tag: `git tag -a v0.9.0 -m "Add CI/CD automation with GitHub Actions"`
    - [ ] **Sub-task 19.7:** Push commits and tags to remote: `git push && git push --tags`

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** `[AI will provide a brief, high-level summary of the work.]`

**Final Status:** `[Pass / Rework Needed / Incomplete]`

---

### Strengths

*`[AI will list specific things that were done correctly and well.]`*
* ...

### Areas for Improvement & Corrections

*`[AI will list specific, actionable feedback on what was done incorrectly or could be improved. This is where errors will be flagged.]`*
- [ ] **Correction 1:** ...
- [ ] **Correction 2:** ...

### Conceptual Gaps

*`[AI will identify any underlying conceptual misunderstandings based on the submitted work.]`*
* ...

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is **Continuous Integration (CI)** and what problem does it solve? What would happen in a team project without CI?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  What is the difference between **Continuous Integration**, **Continuous Delivery**, and **Continuous Deployment**?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  In GitHub Actions, explain the hierarchy: `Workflow → Jobs → Steps → Actions`. What is the purpose of each level?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  What does the `actions/checkout@v4` action do? Why is it almost always the first step in every workflow?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  What is a **GitHub-hosted runner**? What operating systems are available? When would you use a self-hosted runner instead?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

6.  Explain how **caching** works in GitHub Actions. What is a cache key? What happens when the cache key changes?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** You were instructed to run tests automatically on every commit. What are the trade-offs of this approach?
    - What happens if the test suite takes 30 minutes to run?
    - How does this affect developer productivity?
    - What strategies could you use to mitigate slow test suites in CI?
    
    Compare this to an alternative approach where tests only run nightly or when a developer manually triggers them.
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  **The Paradox of Automation:** CI/CD is supposed to save time by automating repetitive tasks. However, setting up and maintaining CI/CD pipelines itself takes significant time. When is it worth investing in CI/CD? At what team size or project complexity does it pay off?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  **Security & Trust:** Your workflow uses third-party actions from the GitHub Marketplace (e.g., `actions/checkout@v4`, `dorny/test-reporter@v1`). These actions run arbitrary code in your CI environment with access to your source code.
    - What are the security risks?
    - Why do we pin action versions (e.g., `@v4`) instead of using `@main`?
    - What is a "supply chain attack" in the context of CI/CD?
    - How could a malicious actor exploit a popular GitHub Action?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  **The Build vs Deploy Decision:** In Task 14, you explored Continuous Deployment. Why might a team choose **Continuous Delivery** (manual approval before production) instead of **Continuous Deployment** (automatic deployment to production)?
    - What are the risks of automatic deployment?
    - In what types of projects/industries would you NEVER use Continuous Deployment?
    - What is a "deployment gate" or "approval step"?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  **Connecting Concepts - The Testing Pyramid & CI:** In Sprint 7 and 8, you created unit tests and acceptance tests. Your CI workflow runs both types.
    - Why do we run unit tests before acceptance tests in the pipeline?
    - If unit tests pass but acceptance tests fail, what does that tell you?
    - Some teams run acceptance tests only on the `main` branch (post-merge), not on every PR. Why might they make this choice?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

6.  **Matrix Builds & Compatibility:** You configured a matrix build to test against .NET 8.0 and .NET 9.0. Imagine your project grows to support 3 .NET versions, 3 operating systems (Ubuntu, Windows, macOS), and 2 database types (MySQL, PostgreSQL).
    - How many parallel jobs would your matrix create? (Calculate: 3 × 3 × 2 = ?)
    - What are the cost implications (build minutes, GitHub Actions limits)?
    - How would you decide which combinations are actually necessary to test?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

7.  **Critical Thinking - The False Positive:** Imagine a scenario where:
    - All tests pass in CI ✅
    - The build is marked as successful ✅
    - You deploy to production
    - The application immediately crashes in production ❌
    
    What could cause this discrepancy? What are the limitations of CI testing? What types of bugs can CI *not* catch?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

8.  **The Merge Conflict Dilemma:** Two developers (Alice and Bob) create separate PRs at the same time:
    - Alice's PR: passes all CI checks ✅
    - Bob's PR: passes all CI checks ✅
    - Alice merges her PR first
    - Bob's PR is now behind `main` and has merge conflicts
    
    Should Bob's PR automatically merge once conflicts are resolved, or should the CI checks run again? Why?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
* **Next Action:** [e.g., Start Sprint 10 for Observability, Deep dive into security best practices for workflows]

**AI Final Feedback:**