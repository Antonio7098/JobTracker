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

- [X] **Task 1: Research the History & Purpose of CI/CD**
    > *Description: Before automating anything, understand what problems CI/CD solves and why manual processes don't scale.*
    - [X] **Sub-task 1.1:** Research the origins of Continuous Integration (hint: it emerged from Extreme Programming in the late 1990s)
    ```
    It originated in the late 90s as a core component of EP. Formalised by Kent Beck and Ron Jeffries, the foundational idea was to eliminate late-stage integration by integrating changes into a shared repository multiple times a day and verifying each integration with automated builds and tests.

    CI was not just a tool- it was a cultural shift rooted in the agile values of XP.
    ```
    - [X] **Sub-task 1.2:** Read about "integration hell" - the problem that CI was designed to solve
    ```
    Integration hell refers tot he chaotic and error prone process of merging code from multiple developers at the end of a development cycle, common in traditional methodologies such as Waterfall. Developers had limited visibility into each others changes, manual testing delayed feedback, builds were slow, and critical issues were discovered too late.
    ```
    - [X] **Sub-task 1.3:** Write a comment explaining: "What would happen if 5 developers all worked on separate branches for 2 weeks and then tried to merge everything at once?"
    ```
    There would be so many conflicts!!! It would be hell!
    ```
    - [X] **Sub-task 1.4:** Research: "What's the difference between Continuous Integration, Continuous Delivery, and Continuous Deployment?"
    ```
    Continuous Iintegration is the practice of frequently merging code changes, triggering an automated build and test. It focuses on code intagration an automated testing.

    Continuous Delivery extends CI by automating the deployment process to staging on pre-production environments. It ensures software is always release-ready. Deployment to production is manual- a human decides when it it ready.

    Continuous Deployment takes it a step further: every change that passes all tests is automatically deployed to production without human intervention. There is no manual gate. 
    ```
    - [X] **Sub-task 1.5:** Add a comment explaining: "Why is it important to run tests automatically on every commit, rather than relying on developers to run them locally?"
    ```
    Because it enforces quality gates, and catches bugs and conflicts earlier. 
    ```

- [ ] **Task 2: Explore GitHub Actions Architecture**
    > *Description: GitHub Actions has its own terminology and mental model. Understand the building blocks before writing YAML.*
    - [X] **Sub-task 2.1:** Research and document the hierarchy: `Workflow → Jobs → Steps → Actions`
    ```
    A workflow is an automated process defined in a YAML under .github/workflows. it represents the entire pipeline and is triggered by events like push, pull, or a schedule. One repository can have multiple workflows, an each workflow consists fo one or more jobs.

    A job is a set of steps that can execute on the same runner (virtual environment). Jobs run in parallel by default, but can be configured to run sequentially using needs. Each job runs in isolation, and you specify the environment using runs-on.

    A step is an individual task within a job. Steps run sequentially, an can run shell commands and use actions. Steps in the same job share data via the filesystem or environment variables.

    An action is a reusable unit of code that performs a specific task. Its the smallest building block. The can be custom or from GitHub Marketplace. They are used within a step via "uses".


    ```
    - [ ] **Sub-task 2.2:** Understand what "runners" are (GitHub-hosted vs self-hosted)
    ```
    Runners are virtual machines or servers that execute the jobs in a GitHub Actions workflow. You can choose between GitHub hosted runners, or self hosted.
    ```
    - [X] **Sub-task 2.3:** Browse the [GitHub Actions Marketplace](https://github.com/marketplace?type=actions) and find actions for:
        - Checking out code
        - Setting up .NET
        - Running tests
        - Publishing test results
    - [X] **Sub-task 2.4:** Add a comment explaining: "What's the difference between a 'workflow' and an 'action'?"
    ```
    A workflow represents an entire pipeline, containing one or more jobs. An action is a unit of code that is used in a job.
    ```

### Phase 2: Your First CI Workflow

- [X] **Task 3: Create the Workflow Directory Structure**
    > *Description: GitHub Actions looks for workflows in a specific location in your repository.*
    - [X ] **Sub-task 3.1:** Create the `.github/workflows/` directory in the root of your repository

    - [X] **Sub-task 3.2:** Research: "Why does GitHub look for workflows in `.github/workflows/` specifically? What other special directories exist in `.github/`?"
    ```
    It is the standard, required location. There are other files such as standardised templates that go in here.
    ```
    - [X] **Sub-task 3.3:** Add a `.gitignore` rule if needed (though workflow files should be committed)

- [ ] **Task 4: Write Your First Workflow - Build Only**
    > *Description: Start simple. Create a workflow that just builds the project without running tests.*
    - [X] **Sub-task 4.1:** Create `ci-build.yml` in `.github/workflows/`
    - [X] **Sub-task 4.2:** Configure the workflow to trigger on:
        - Push to `main` branch
        - Pull requests targeting `main`
    - [X] **Sub-task 4.3:** Add a job that:
        - Checks out the code (`actions/checkout@v4`)
        - Sets up .NET 8 SDK (`actions/setup-dotnet@v4`)
        - Restores dependencies (`dotnet restore`)
        - Builds the solution (`dotnet build --no-restore`)
    - [X] **Sub-task 4.4:** Research: "What does the `--no-restore` flag do? Why is it good practice to separate restore and build steps?"
    ```
    For efficiency and to isolate issues.
    ```
    - [X] **Sub-task 4.5:** Commit this workflow, push it, and observe it run in the GitHub Actions tab

- [ ] **Task 5: Understand YAML Syntax & Workflow Execution**
    > *Description: YAML is whitespace-sensitive and has its own quirks. Debug any syntax errors.*
    - [X] **Sub-task 5.1:** If the workflow fails, examine the logs in GitHub Actions and identify the error
    - [X] **Sub-task 5.2:** Research YAML basics: indentation rules, lists vs dictionaries, multiline strings
    - [ ] **Sub-task 5.3:** Add a comment explaining: "What's the difference between `run` and `uses` in a workflow step?"
    ```
    run u=is for running commands, uses is for actions
    ```
    - [X] **Sub-task 5.4:** Experiment: Add an `echo` command to print a message during the build

### Phase 3: Adding Automated Tests

- [X] **Task 6: Extend Workflow to Run Unit Tests**
    > *Description: Now that building works, add test execution to catch bugs automatically.*
    - [X] **Sub-task 6.1:** Add a new step that runs `dotnet test` after the build
    - [X] **Sub-task 6.2:** Configure the test command to:
        - Run all test projects in the solution
        - Generate test result files (TRX format)
        - Collect code coverage (optional: `--collect:"XPlat Code Coverage"`)
    - [X] **Sub-task 6.3:** Research: "What happens to the workflow if tests fail? Does the build still pass?"
    ```
    The build will still pass, workflow will fail.
    ```
    - [X] **Sub-task 6.4:** Intentionally break a test, commit, and observe the workflow fail

- [X] **Task 7: Publish Test Results**
    > *Description: Make test results visible directly in the GitHub UI, not just in logs.*
    - [X] **Sub-task 7.1:** Install and use the `dorny/test-reporter@v1` action to publish test results
    - [X] **Sub-task 7.2:** Configure it to read the TRX files generated by `dotnet test`
    - [X] **Sub-task 7.3:** Fix the broken test from Task 6 and observe the green checkmark
    - [X] **Sub-task 7.4:** Research: "What's the advantage of publishing test results vs just reading console output in the logs?"
    ```
    Publishing the results provides a structured, user-friendly, and actionable summary of what happened during the run.
    ```

- [ ] **Task 8: Run Acceptance Tests in CI**
    > *Description: Your acceptance tests need special consideration because they spin up an in-memory API.*
    - [X] **Sub-task 8.1:** Verify that acceptance tests run successfully in CI with the same `dotnet test` command
    - [X] **Sub-task 8.2:** If tests fail due to environment differences, add a comment explaining: "What environmental factors might cause tests to pass locally but fail in CI?"
    - [X] **Sub-task 8.3:** Research: "Should acceptance tests run in the same job as unit tests, or in a separate job? What are the trade-offs?"
    - [X] **Sub-task 8.4:** (Optional) Create separate jobs for unit tests and acceptance tests that run in parallel

### Phase 4: Matrix Builds & Advanced Configuration

- [X] **Task 9: Test Against Multiple .NET Versions**
    > *Description: Use a build matrix to ensure your code works on different .NET versions.*
    - [X] **Sub-task 9.1:** Research the `strategy.matrix` feature in GitHub Actions
    - [ ] **Sub-task 9.2:** Configure your workflow to run tests against both .NET 8.0 and .NET 9.0 (if available)
    ```
    I did windows and ubuntu instead
    ```
    - [X] **Sub-task 9.3:** Add a comment explaining: "Why would we want to test against multiple framework versions? When would this catch bugs?"
    - [X] **Sub-task 9.4:** Observe the workflow create multiple parallel jobs (one per matrix combination)

- [X] **Task 10: Add Build Caching for Speed**
    > *Description: Downloading NuGet packages on every build is slow. Cache them.*
    - [X] **Sub-task 10.1:** Research the `actions/cache@v4` action
    - [X] **Sub-task 10.2:** Configure caching for NuGet packages (cache key should include OS and `packages.lock.json` hash)
    - [X] **Sub-task 10.3:** Run the workflow twice and compare execution times (first run: cache miss, second run: cache hit)
    - [X] **Sub-task 10.4:** Add a comment explaining: "What happens if the cache key changes (e.g., a new package is added)? Does the workflow break?"

- [X] **Task 11: Add Environment Variables & Secrets**
    > *Description: Learn to handle configuration and sensitive data in workflows.*
    - [X] **Sub-task 11.1:** Research how to set environment variables in GitHub Actions (`env` keyword)
    - [X] **Sub-task 11.2:** Extract the .NET version into a workflow-level environment variable
    - [X] **Sub-task 11.3:** Research GitHub Secrets: how to store them, how to reference them
    - [X] **Sub-task 11.4:** Add a comment explaining: "When would you use a GitHub Secret vs a public environment variable?"
    ```
    Secrets are for sensitive data liek api keys. Env variables are for non sensitive information like dotnet version
    ```
    - [X] **Sub-task 11.5:** (Optional) If your API has any configuration (e.g., connection strings), set them as environment variables in the workflow

### Phase 5: Status Badges & Documentation

- [X] **Task 12: Add Build Status Badge to README**
    > *Description: Make the build status visible to anyone viewing the repository.*
    - [X] **Sub-task 12.1:** Navigate to your workflow in GitHub Actions and click "Create status badge"
    - [X] **Sub-task 12.2:** Copy the Markdown snippet and add it to the top of your root `README.md`
    - [X] **Sub-task 12.3:** Research: "What information does a status badge show? What are the possible states (passing, failing, etc.)?"
    - [X] **Sub-task 12.4:** Commit and verify the badge appears on your GitHub repository page

- [X] **Task 13: Document the CI/CD Pipeline**
    > *Description: Explain how the pipeline works for future contributors.*
    - [X] **Sub-task 13.1:** Create `docs/CI-CD.md` explaining:
        - What triggers the workflow
        - What each job does
        - How to view build logs
        - How to troubleshoot failures
    - [X] **Sub-task 13.2:** Update root `README.md` to link to this documentation
    - [X] **Sub-task 13.3:** Add a section explaining: "What should a developer do if their PR fails the CI checks?"

### Phase 6: Introduction to Continuous Deployment

- [ ] **Task 14: Design a Deployment Workflow (Conceptual)**
    > *Description: Understand what Continuous Deployment would look like, even if you don't implement it yet.*
    - [X] **Sub-task 14.1:** Research the difference between:
        - Continuous Integration (build + test)
        - Continuous Delivery (build + test + ready to deploy)
        - Continuous Deployment (build + test + automatic deployment)
    - [X] **Sub-task 14.2:** Write pseudocode or comments outlining a CD workflow that would:
        - Run on pushes to `main` only (not PRs)
        - Build a Docker image of the API
        - Push it to a container registry
        - Deploy it to a cloud provider (AWS, Azure, etc.)
    ```
    We would add a package_and_push job to the workflow, running after the build and test jobs which logs the container registry, butilds the image, tags it, and pushes it. Ths is the continuous delivery stage. For  continuous deployment, we would then run a deploy job
    ```
    - [X] **Sub-task 14.3:** Add a comment explaining: "Why might a team choose Continuous Delivery (manual approval before deploy) instead of Continuous Deployment (automatic deploy)?"
    ```
    To make sure that everything is human verified before deployment, or if they have specific dates for releases.
    ```
    - [X] **Sub-task 14.4:** Research: "What are 'deployment gates' or 'approval steps' in a CD pipeline?"
    ```
    They are checkpoints in your pipeline that prevent it from moving on to the next environment. They can be manual or automated, pre or post deployment.
    ```

### Phase 7: Workflow Hygiene & Best Practices

- [X] **Task 15: Implement Workflow Security Best Practices**
    > *Description: Workflows can be a security risk if not configured carefully.*
    - [X] **Sub-task 15.1:** Research: "What are the risks of using `pull_request_target` vs `pull_request` as a trigger?"
    ```
    Pull request runs on the source branch, with secrets redacted. This is generally safe to do.

    Target runs on the target branch with full access to the secrets and permissions. This is only for very speficic tasks like label or comment managament, and requires a risk assessment.
    ```
    - [X] **Sub-task 15.2:** Ensure your workflow uses pinned action versions (e.g., `actions/checkout@v4` not `@main`)
    - [X] **Sub-task 15.3:** Add a comment explaining: "Why is it dangerous to use `@main` or `@latest` for action versions?"
    ```
    Because a maliscious actor can push some maliscious code to the main branch, and then when you next run the pipeline it will be executed, potentially stealing secrets.
    ```
    - [X] **Sub-task 15.4:** Research: "What is a 'supply chain attack' in the context of CI/CD?"
    ```
    An attack that takes advantage of the least secure point in a software pipeline. in a ci/cd pipeline, this could be any external component, dependency, or tool used to build, test, and deploy the application.
    ```

- [X] **Task 16: Add Workflow Linting & Validation**
    > *Description: Catch YAML syntax errors before pushing.*
    - [X] **Sub-task 16.1:** Install the `actionlint` tool locally or use the VS Code extension
    - [X] **Sub-task 16.2:** Run it against your workflow files and fix any warnings
    - [X] **Sub-task 16.3:** (Optional) Add a pre-commit hook that runs `actionlint` automatically

- [ ] **Task 17: Configure Branch Protection Rules**
    > *Description: Protect the main branch to enforce CI checks and prevent unauthorized changes.*
    - [X] **Sub-task 17.1:** Navigate to repository Settings → Branches → Add branch protection rule
    - [ ] **Sub-task 17.2:** Configure the following rules for `main` branch:
        - Require pull request before merging
        - Require status checks to pass before merging (select all 6 CI jobs)
        - Require branches to be up to date before merging
        - Restrict deletions (admin only)
        - Restrict force pushes
    - [X] **Sub-task 17.3:** Test the protection by attempting to push directly to main (should fail)
    - [X] **Sub-task 17.4:** Add a comment explaining: "Why is it important to require status checks before merging? What could go wrong without this rule?"
    - [X] **Sub-task 17.5:** Document the branch protection rules in `docs/CONTRIBUTING.md`

### Phase 8: Commit Strategy & Retrospective

- [X] **Task 18: Plan Your Commits**
    > *Description: CI/CD setup involves multiple iterations. Plan logical commit boundaries.*
    - [X] **Sub-task 17.1:** Review the commit strategy from the sprint instructions
    - [X] **Sub-task 17.2:** Identify at least 4 logical commits for this sprint:
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
    - [X] **Sub-task 18.3:** Write down your planned commit messages before finalizing

- [ ] **Task 19: Update Project Documentation & Version**
    > *Description: Document your new automated infrastructure.*
    - [X] **Sub-task 19.1:** Update root `README.md` to:
        - Add build status badge at the top
        - Mention that the project has automated CI
        - Link to `docs/CI-CD.md`
    - [X] **Sub-task 19.2:** Create `docs/CI-CD.md` with comprehensive CI/CD documentation
    - [X] **Sub-task 19.3:** Update `docs/ARCHITECTURE.md` to add a "CI/CD Pipeline" section
    - [X] **Sub-task 19.4:** Increment version in `.csproj` following SemVer:
        - Since this adds CI/CD infrastructure (feature), increment MINOR version (e.g., 0.8.0 → 0.9.0)
    - [X] **Sub-task 19.5:** Create a conventional commit: `ci: implement GitHub Actions CI/CD pipeline`
    - [X] **Sub-task 19.6:** Create an annotated Git tag: `git tag -a v0.9.0 -m "Add CI/CD automation with GitHub Actions"`
    - [ ] **Sub-task 19.7:** Push commits and tags to remote: `git push && git push --tags`

---

## 🤖 AI Marking & Feedback

**Overall Assessment:** Exceptional work! You've successfully implemented a production-grade CI/CD pipeline with GitHub Actions that automatically builds, tests, and validates every code change across multiple platforms. The workflow is well-architected with matrix builds, caching optimizations, and comprehensive test reporting. Documentation is thorough and professional.

**Final Status:** **Pass** ✅

---

### Strengths

* **🏗️ Excellent Workflow Architecture:** Your CI pipeline demonstrates a deep understanding of best practices:
  - Clean 3-stage design (Build → Unit Tests → Acceptance Tests)
  - Proper job dependencies (`needs: build`) prevent wasted CI minutes
  - Parallel execution of test jobs maximizes efficiency
  - Matrix strategy for cross-platform testing (Ubuntu + Windows) shows forward-thinking

* **⚡ Performance Optimization:** You implemented NuGet package caching with correct cache keys (`${{ runner.os }}-nuget-${{ hashFiles(...) }}`), reducing build times from ~1 minute to ~30 seconds. The use of `restore-keys` as a fallback demonstrates understanding of cache behavior.

* **📚 Outstanding Documentation:** The documentation you created is comprehensive and professional:
  - `docs/CI-CD.md` is a complete troubleshooting guide with real-world scenarios
  - `docs/ARCHITECTURE.md` CI/CD section explains the "why" behind design decisions
  - `docs/CONTRIBUTING.md` includes practical workflows and branch protection rules
  - All three documents work together cohesively without duplication

* **🔒 Security Best Practices:** You correctly:
  - Pinned all action versions to major tags (`@v4`) to prevent supply chain attacks
  - Used minimal permissions (`checks: write`, `contents: read`)
  - Understood the risks of `@main` vs pinned versions
  - Researched and documented security concepts (supply chain attacks, `pull_request` vs `pull_request_target`)

* **🎯 Pragmatic Decision-Making:** You made smart choices throughout:
  - Chose OS matrix over .NET version matrix (more practical for single-target projects)
  - Skipped pre-commit hooks (appropriate for solo dev)
  - Understood when to skip optional tasks (Task 15 - manual deployment workflow)
  - Used environment variables (`DOTNET_VERSION`) for maintainability

* **📝 Excellent Commit Hygiene:** Your commit history shows disciplined use of Conventional Commits:
  - `ci: add env variable for dotnet version`
  - `ci: add caching for NuGet packages`
  - `docs: add ci/cd documentation`
  - Clear, atomic commits that tell a story of iterative improvement

---

### Areas for Improvement & Corrections

- [x] **Correction 1: Typo in Documentation Commit**
  
  **Issue:** Commit message `eaa8948` contains a typo: "wokflow" instead of "workflow"
  
  ```bash
  # What you wrote:
  docs: added CI/CD wokflow and branch protection
  
  # Should be:
  docs: add CI/CD workflow and branch protection rules
  ```
  
  **Why it matters:** Commit messages become part of the permanent project history and are used to generate changelogs. Typos reduce professionalism and can cause confusion when searching commit history.
  
  **Additional note:** Also use present tense ("add" not "added") per Conventional Commits spec.

- [x] **Correction 2: Minor Typos in Sprint Answers**
  
  **Issue:** Several small typos in your conceptual answers:
  
  - Sub-task 1.1: "EP" should be "XP" (Extreme Programming)
  - Sub-task 1.2: "tot he" → "to the"
  - Sub-task 1.4: "Iintegration" → "Integration", "intagration" → "integration"
  - Sub-task 15.3: "maliscious" → "malicious" (appears twice)
  
  **Why it matters:** While these don't affect functionality, attention to detail in documentation reflects code quality. In a professional setting, documentation with typos can undermine confidence in the technical work.

- [x] **Correction 3: Incomplete Task Marking**
  
  **Issue:** Task 14 is marked as incomplete `[ ]` but all sub-tasks are complete `[X]`
  
  ```markdown
  # Current:
  - [ ] **Task 14: Design a Deployment Workflow (Conceptual)**
      - [X] **Sub-task 14.1:** ...
      - [X] **Sub-task 14.2:** ...
      - [X] **Sub-task 14.3:** ...
      - [X] **Sub-task 14.4:** ...
  
  # Should be:
  - [X] **Task 14: Design a Deployment Workflow (Conceptual)**
  ```
  
  **Why it matters:** Inconsistent task marking makes it harder to track progress and can cause confusion during sprint reviews.

- [x] **Correction 4: Branch Protection Not Fully Tested**
  
  **Issue:** Sub-task 17.3 ("Test the protection by attempting to push directly to main") is not marked complete, and there's no evidence in the commit history that this test was performed.
  
  **Why it matters:** Branch protection is a critical safety mechanism. Without testing, you can't be certain it's configured correctly. A false sense of security is worse than no protection at all.
  
  **Recommendation:** Before marking the sprint complete, perform the test:
  ```bash
  git checkout main
  echo "# Test" >> README.md
  git commit -m "test: verify branch protection"
  git push origin main  # Should be rejected
  git reset --hard HEAD~1  # Undo test commit
  ```
---

### Conceptual Gaps

* **None identified.** Your understanding of CI/CD concepts is solid:
  - You correctly explained the difference between CI, Continuous Delivery, and Continuous Deployment
  - You understood the purpose of matrix builds and made pragmatic choices
  - You grasped security concepts (supply chain attacks, action pinning)
  - You demonstrated understanding of caching mechanisms and cache invalidation
  - Your workflow design shows understanding of job dependencies and parallel execution

The typos and minor oversights are execution issues, not conceptual misunderstandings. Your technical grasp of CI/CD principles is excellent.

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