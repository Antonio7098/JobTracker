# 🎯 Sprint 11: Cloud Deployment - AWS & Docker

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** Docker, Containerization, Container Orchestration, AWS (ECS/Fargate, RDS, ECR), Infrastructure as Code, Cloud Networking (VPC, Security Groups), Environment Variables, Secrets Management, Multi-Stage Builds, Container Registries, Load Balancing, HTTPS/SSL, Domain Configuration
* **Primary Goal (Must-Have):** By the end, I must be able to **containerize the Job Tracker API using Docker, push the image to AWS ECR, deploy it to AWS ECS/Fargate with a managed MySQL RDS instance, and make it publicly accessible via a load balancer with HTTPS.**
* **Secondary Goals:**
    * Understand what containers are and why they solve the "works on my machine" problem
    * Write production-ready Dockerfiles with multi-stage builds
    * Understand AWS core services (ECS, ECR, RDS, VPC, ALB)
    * Implement proper secrets management (AWS Secrets Manager)
    * Configure health checks and auto-scaling
    * Set up CI/CD to automatically deploy on push to main (extending Sprint 9)
    * Understand the cost implications of cloud resources

---

## ✅ Task List

### Phase 1: Understanding Containerization & Docker

- [ ] **Task 1: Research the History & Purpose of Containers**
    > *Description: Containers revolutionized deployment. Understand why they exist and what problems they solve.*
    - [ ] **Sub-task 1.1:** Research the evolution: Bare Metal → Virtual Machines → Containers
    - [ ] **Sub-task 1.2:** Read about the "works on my machine" problem and how containers solve it
    - [ ] **Sub-task 1.3:** Write a comment explaining: "What's the difference between a Virtual Machine and a Container? Why are containers lighter?"
    - [ ] **Sub-task 1.4:** Research: "What is a container image? What is a container runtime (like Docker Engine)?"
    - [ ] **Sub-task 1.5:** Understand Docker's layer architecture and how image caching works
    - [ ] **Sub-task 1.6:** Add a comment explaining: "If I have 10 containers running from the same base image, does Docker store 10 copies of that base image?"

- [ ] **Task 2: Install Docker and Understand the Architecture**
    > *Description: Get Docker running locally and understand its components.*
    - [ ] **Sub-task 2.1:** Install Docker Desktop for your OS
    - [ ] **Sub-task 2.2:** Run the hello-world container: `docker run hello-world`
    - [ ] **Sub-task 2.3:** Understand the Docker architecture: Client → Daemon → Registry
    - [ ] **Sub-task 2.4:** Research common Docker commands:
        - `docker build`: Build an image from a Dockerfile
        - `docker run`: Create and start a container
        - `docker ps`: List running containers
        - `docker images`: List images
        - `docker logs`: View container logs
        - `docker exec`: Execute a command in a running container
    - [ ] **Sub-task 2.5:** Add a comment explaining: "What's the difference between an image and a container?"

### Phase 2: Creating Your First Dockerfile

- [ ] **Task 3: Write a Basic Dockerfile**
    > *Description: Start simple - create a Dockerfile that runs your API.*
    - [ ] **Sub-task 3.1:** Create a `Dockerfile` in your API project root
    - [ ] **Sub-task 3.2:** Start with a basic single-stage Dockerfile:
        ```dockerfile
        FROM mcr.microsoft.com/dotnet/aspnet:8.0
        WORKDIR /app
        COPY . .
        EXPOSE 8080
        ENTRYPOINT ["dotnet", "JobTracker.Api.dll"]
        ```
    - [ ] **Sub-task 3.3:** Research each Dockerfile instruction: `FROM`, `WORKDIR`, `COPY`, `EXPOSE`, `ENTRYPOINT`, `CMD`
    - [ ] **Sub-task 3.4:** Add a comment explaining: "Why do we use `mcr.microsoft.com/dotnet/aspnet:8.0` instead of the SDK image?"
    - [ ] **Sub-task 3.5:** Attempt to build the image: `docker build -t jobtracker-api .`
    - [ ] **Sub-task 3.6:** This will fail because you need to build the project first - understand why

- [ ] **Task 4: Implement Multi-Stage Dockerfile**
    > *Description: Create a production-ready Dockerfile that builds the app inside the container.*
    - [ ] **Sub-task 4.1:** Rewrite your Dockerfile with multi-stage builds:
        - Stage 1: Use SDK image to build the application
        - Stage 2: Use runtime image and copy only the published output
    - [ ] **Sub-task 4.2:** Example structure:
        ```dockerfile
        # Build stage
        FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
        WORKDIR /src
        COPY ["JobTracker.Api/JobTracker.Api.csproj", "JobTracker.Api/"]
        RUN dotnet restore "JobTracker.Api/JobTracker.Api.csproj"
        COPY . .
        WORKDIR "/src/JobTracker.Api"
        RUN dotnet build -c Release -o /app/build
        RUN dotnet publish -c Release -o /app/publish

        # Runtime stage
        FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
        WORKDIR /app
        COPY --from=build /app/publish .
        EXPOSE 8080
        ENTRYPOINT ["dotnet", "JobTracker.Api.dll"]
        ```
    - [ ] **Sub-task 4.3:** Build the image: `docker build -t jobtracker-api:v1 .`
    - [ ] **Sub-task 4.4:** Run the container: `docker run -p 8080:8080 jobtracker-api:v1`
    - [ ] **Sub-task 4.5:** Test the API at `http://localhost:8080/swagger`
    - [ ] **Sub-task 4.6:** Add a comment explaining: "Why is the multi-stage build smaller than a single-stage build? Compare the image sizes."

- [ ] **Task 5: Add .dockerignore File**
    > *Description: Optimize build speed and security by excluding unnecessary files.*
    - [ ] **Sub-task 5.1:** Create a `.dockerignore` file in the project root
    - [ ] **Sub-task 5.2:** Exclude common files:
        ```
        **/.git
        **/.vs
        **/.vscode
        **/bin
        **/obj
        **/.dockerignore
        **/Dockerfile
        **/.env
        **/*.md
        ```
    - [ ] **Sub-task 5.3:** Research: "Why is .dockerignore important for security?"
    - [ ] **Sub-task 5.4:** Rebuild and notice the faster build time

### Phase 3: Local Docker Compose for Development

- [ ] **Task 6: Create docker-compose.yml**
    > *Description: Define your application stack (API + MySQL) as code.*
    - [ ] **Sub-task 6.1:** Create `docker-compose.yml` in the solution root
    - [ ] **Sub-task 6.2:** Define two services:
        - `api`: Your containerized API
        - `mysql`: MySQL database
    - [ ] **Sub-task 6.3:** Configure the API to use the MySQL container (via environment variables)
    - [ ] **Sub-task 6.4:** Add volume mounts for MySQL data persistence
    - [ ] **Sub-task 6.5:** Example structure:
        ```yaml
        version: '3.8'
        services:
          mysql:
            image: mysql:8.0
            environment:
              MYSQL_ROOT_PASSWORD: rootpassword
              MYSQL_DATABASE: JobTrackerDb
              MYSQL_USER: user
              MYSQL_PASSWORD: password
            ports:
              - "3306:3306"
            volumes:
              - mysql-data:/var/lib/mysql

          api:
            build:
              context: .
              dockerfile: JobTracker.Api/Dockerfile
            ports:
              - "8080:8080"
            environment:
              - ConnectionStrings__DefaultConnection=Server=mysql;Database=JobTrackerDb;User=user;Password=password;
            depends_on:
              - mysql

        volumes:
          mysql-data:
        ```
    - [ ] **Sub-task 6.6:** Run the stack: `docker-compose up`
    - [ ] **Sub-task 6.7:** Run migrations: `docker-compose exec api dotnet ef database update`
    - [ ] **Sub-task 6.8:** Add a comment explaining: "Why do we use 'mysql' as the hostname instead of 'localhost'?"

- [ ] **Task 7: Handle Database Initialization**
    > *Description: Automatically run migrations when the container starts.*
    - [ ] **Sub-task 7.1:** Research: "Should you run migrations in the container startup script or separately?"
    - [ ] **Sub-task 7.2:** Add code to `Program.cs` to automatically apply migrations on startup (with a flag to disable in production):
        ```csharp
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<JobTrackerDbContext>();
            db.Database.Migrate();
        }
        ```
    - [ ] **Sub-task 7.3:** Add a comment explaining: "Why might auto-migrating in production be dangerous?"

### Phase 4: AWS Account Setup & Core Concepts

- [ ] **Task 8: Create AWS Account and Understand Pricing**
    > *Description: Set up your AWS environment and understand cost implications.*
    - [ ] **Sub-task 8.1:** Create an AWS account (Free Tier eligible)
    - [ ] **Sub-task 8.2:** Set up billing alerts to avoid surprise charges
    - [ ] **Sub-task 8.3:** Research AWS Free Tier limits for:
        - ECS Fargate (limited free hours)
        - RDS (750 hours/month for t2.micro/t3.micro)
        - ECR (500 MB storage)
        - Data transfer (15 GB/month)
    - [ ] **Sub-task 8.4:** Add a comment explaining: "What costs money in AWS? What's free tier vs pay-as-you-go?"
    - [ ] **Sub-task 8.5:** Research: "What happens if I forget to delete resources after the sprint?"

- [ ] **Task 9: Understand AWS Core Networking (VPC, Subnets, Security Groups)**
    > *Description: AWS networking is complex. Understand the fundamentals before deploying.*
    - [ ] **Sub-task 9.1:** Research: "What is a VPC (Virtual Private Cloud)? Why does every AWS resource need to be in one?"
    - [ ] **Sub-task 9.2:** Understand the difference between:
        - Public subnet (has internet access via Internet Gateway)
        - Private subnet (no direct internet access)
    - [ ] **Sub-task 9.3:** Research: "What is a Security Group? How is it different from a firewall?"
    - [ ] **Sub-task 9.4:** Draw a diagram of your planned architecture:
        - VPC with 2 public subnets (for load balancer)
        - 2 private subnets (for ECS tasks and RDS)
        - Internet Gateway
        - NAT Gateway (for private subnets to reach the internet)
    - [ ] **Sub-task 9.5:** Add a comment explaining: "Why do we put the database in a private subnet?"

### Phase 5: Pushing Images to AWS ECR

- [ ] **Task 10: Create ECR Repository**
    > *Description: ECR (Elastic Container Registry) is AWS's Docker registry.*
    - [ ] **Sub-task 10.1:** Install AWS CLI: `aws configure` with your credentials
    - [ ] **Sub-task 10.2:** Create an ECR repository: 
        ```bash
        aws ecr create-repository --repository-name jobtracker-api --region us-east-1
        ```
    - [ ] **Sub-task 10.3:** Research: "What's the difference between ECR and Docker Hub?"

- [ ] **Task 11: Push Docker Image to ECR**
    > *Description: Tag and push your local image to AWS.*
    - [ ] **Sub-task 11.1:** Authenticate Docker to ECR:
        ```bash
        aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin <account-id>.dkr.ecr.us-east-1.amazonaws.com
        ```
    - [ ] **Sub-task 11.2:** Tag your image:
        ```bash
        docker tag jobtracker-api:v1 <account-id>.dkr.ecr.us-east-1.amazonaws.com/jobtracker-api:latest
        ```
    - [ ] **Sub-task 11.3:** Push the image:
        ```bash
        docker push <account-id>.dkr.ecr.us-east-1.amazonaws.com/jobtracker-api:latest
        ```
    - [ ] **Sub-task 11.4:** Verify in the AWS Console that the image appears in ECR
    - [ ] **Sub-task 11.5:** Add a comment explaining: "Why do we need to authenticate Docker to ECR?"

### Phase 6: Setting Up AWS RDS for MySQL

- [ ] **Task 12: Create RDS MySQL Instance**
    > *Description: Provision a managed MySQL database in AWS.*
    - [ ] **Sub-task 12.1:** In AWS Console, navigate to RDS
    - [ ] **Sub-task 12.2:** Create a new MySQL database:
        - Engine: MySQL 8.0
        - Template: Free Tier (db.t3.micro or db.t2.micro)
        - Storage: 20 GB
        - Multi-AZ: Disabled (for cost savings)
    - [ ] **Sub-task 12.3:** Configure security group to allow inbound traffic on port 3306 from your ECS security group
    - [ ] **Sub-task 12.4:** Note the endpoint URL (e.g., `jobtracker-db.xxxx.us-east-1.rds.amazonaws.com`)
    - [ ] **Sub-task 12.5:** Research: "What's the difference between RDS and running MySQL in an EC2 instance?"

- [ ] **Task 13: Store Database Credentials in AWS Secrets Manager**
    > *Description: Never hardcode passwords. Use Secrets Manager.*
    - [ ] **Sub-task 13.1:** In AWS Console, navigate to Secrets Manager
    - [ ] **Sub-task 13.2:** Create a new secret (type: Credentials for RDS database)
    - [ ] **Sub-task 13.3:** Store username, password, host, port, database name
    - [ ] **Sub-task 13.4:** Note the secret ARN
    - [ ] **Sub-task 13.5:** Research: "What's the difference between AWS Secrets Manager and AWS Systems Manager Parameter Store?"

### Phase 7: Deploying to AWS ECS with Fargate

- [ ] **Task 14: Create ECS Cluster**
    > *Description: ECS (Elastic Container Service) orchestrates your containers.*
    - [ ] **Sub-task 14.1:** In AWS Console, navigate to ECS
    - [ ] **Sub-task 14.2:** Create a new cluster:
        - Name: `jobtracker-cluster`
        - Infrastructure: AWS Fargate (serverless)
    - [ ] **Sub-task 14.3:** Research: "What's the difference between ECS with EC2 vs ECS with Fargate?"
    - [ ] **Sub-task 14.4:** Add a comment explaining: "Why choose Fargate over managing EC2 instances yourself?"

- [ ] **Task 15: Create ECS Task Definition**
    > *Description: A task definition is a blueprint for your container.*
    - [ ] **Sub-task 15.1:** In ECS, create a new Task Definition:
        - Launch type: Fargate
        - Task memory: 512 MB
        - Task CPU: 0.25 vCPU
    - [ ] **Sub-task 15.2:** Add a container definition:
        - Image URI: Your ECR image URL
        - Port mappings: 8080
        - Environment variables: Connection string (reference Secrets Manager)
    - [ ] **Sub-task 15.3:** Configure task execution role with permissions to:
        - Pull images from ECR
        - Read secrets from Secrets Manager
        - Write logs to CloudWatch
    - [ ] **Sub-task 15.4:** Research: "What's the difference between 'task role' and 'task execution role'?"

- [ ] **Task 16: Create Application Load Balancer (ALB)**
    > *Description: The load balancer makes your API publicly accessible.*
    - [ ] **Sub-task 16.1:** In EC2 console, create an Application Load Balancer:
        - Scheme: Internet-facing
        - Subnets: Select at least 2 public subnets in different AZs
    - [ ] **Sub-task 16.2:** Create a target group:
        - Target type: IP (for Fargate)
        - Protocol: HTTP
        - Port: 8080
        - Health check path: `/health` (ensure this endpoint exists)
    - [ ] **Sub-task 16.3:** Configure security groups:
        - ALB: Allow inbound HTTP (80) and HTTPS (443) from anywhere
        - ECS: Allow inbound 8080 from ALB security group only
    - [ ] **Sub-task 16.4:** Research: "What's the difference between an Application Load Balancer (ALB) and a Network Load Balancer (NLB)?"

- [ ] **Task 17: Create ECS Service**
    > *Description: An ECS Service ensures your tasks stay running.*
    - [ ] **Sub-task 17.1:** In your ECS cluster, create a new Service:
        - Launch type: Fargate
        - Task Definition: Select the one you created
        - Desired tasks: 2 (for high availability)
        - VPC: Your VPC
        - Subnets: Private subnets
    - [ ] **Sub-task 17.2:** Configure load balancing:
        - Load balancer type: Application Load Balancer
        - Target group: The one you created
    - [ ] **Sub-task 17.3:** Enable auto-scaling (optional):
        - Min tasks: 2
        - Max tasks: 10
        - Scaling metric: CPU > 70% or Request count
    - [ ] **Sub-task 17.4:** Wait for tasks to start and become healthy
    - [ ] **Sub-task 17.5:** Get the ALB DNS name and test: `http://<alb-dns-name>/swagger`

### Phase 8: HTTPS and Domain Configuration

- [ ] **Task 18: Request SSL Certificate from ACM**
    > *Description: Enable HTTPS for secure communication.*
    - [ ] **Sub-task 18.1:** (Optional) Register a domain name (e.g., via Route 53 or external registrar)
    - [ ] **Sub-task 18.2:** In AWS Certificate Manager (ACM), request a public certificate for your domain
    - [ ] **Sub-task 18.3:** Validate the certificate (DNS or email validation)
    - [ ] **Sub-task 18.4:** Research: "What is TLS/SSL? Why is HTTPS important for APIs?"

- [ ] **Task 19: Configure HTTPS Listener on ALB**
    > *Description: Update the load balancer to accept HTTPS traffic.*
    - [ ] **Sub-task 19.1:** Add a new listener to your ALB:
        - Protocol: HTTPS
        - Port: 443
        - Default action: Forward to target group
        - SSL certificate: Select the ACM certificate
    - [ ] **Sub-task 19.2:** Update the HTTP listener to redirect to HTTPS
    - [ ] **Sub-task 19.3:** If you have a domain, configure Route 53 to point to the ALB
    - [ ] **Sub-task 19.4:** Test: `https://yourdomain.com/swagger` or `https://<alb-dns-name>/swagger`

### Phase 9: Monitoring and Logging in Production

- [ ] **Task 20: Configure CloudWatch Logs**
    > *Description: Stream container logs to CloudWatch for troubleshooting.*
    - [ ] **Sub-task 20.1:** In your ECS task definition, configure log driver to `awslogs`
    - [ ] **Sub-task 20.2:** Create a CloudWatch log group: `/ecs/jobtracker-api`
    - [ ] **Sub-task 20.3:** View logs in CloudWatch console
    - [ ] **Sub-task 20.4:** Research: "How do CloudWatch Logs differ from logging to a file on disk?"

- [ ] **Task 21: Set Up CloudWatch Alarms**
    > *Description: Get notified when things go wrong.*
    - [ ] **Sub-task 21.1:** Create an alarm for ECS service:
        - Metric: Unhealthy host count > 0
        - Action: Send email via SNS (Simple Notification Service)
    - [ ] **Sub-task 21.2:** Create an alarm for RDS:
        - Metric: CPU > 80%
    - [ ] **Sub-task 21.3:** Test the alarm by intentionally crashing the API

### Phase 10: Automating Deployment with GitHub Actions

- [ ] **Task 22: Extend CI/CD Pipeline for Deployment**
    > *Description: Connect Sprint 9's CI pipeline to automatically deploy to AWS.*
    - [ ] **Sub-task 22.1:** Create a new workflow file: `.github/workflows/cd-deploy.yml`
    - [ ] **Sub-task 22.2:** Configure the workflow to:
        - Trigger on push to `main` branch (after tests pass)
        - Build and push Docker image to ECR
        - Update ECS service to use new image
    - [ ] **Sub-task 22.3:** Store AWS credentials in GitHub Secrets:
        - `AWS_ACCESS_KEY_ID`
        - `AWS_SECRET_ACCESS_KEY`
    - [ ] **Sub-task 22.4:** Use official AWS actions:
        - `aws-actions/configure-aws-credentials@v4`
        - `aws-actions/amazon-ecr-login@v2`
        - `aws-actions/amazon-ecs-deploy-task-definition@v1`
    - [ ] **Sub-task 22.5:** Test: Push a code change and watch it automatically deploy
    - [ ] **Sub-task 22.6:** Research: "What is 'blue-green deployment'? How could you implement it with ECS?"

### Phase 11: Cost Optimization & Cleanup

- [ ] **Task 23: Understand Your AWS Bill**
    > *Description: Monitor costs and optimize where possible.*
    - [ ] **Sub-task 23.1:** In AWS Console, navigate to Cost Explorer
    - [ ] **Sub-task 23.2:** Identify the most expensive services
    - [ ] **Sub-task 23.3:** Research cost optimization strategies:
        - Use Fargate Spot for non-critical workloads
        - Set up auto-scaling to reduce idle capacity
        - Delete unused load balancers and NAT gateways
        - Use reserved instances for predictable workloads
    - [ ] **Sub-task 23.4:** Add a comment explaining: "What AWS resources cost money even when idle?"

- [ ] **Task 24: Create Teardown Script**
    > *Description: Make it easy to clean up resources when the sprint is complete.*
    - [ ] **Sub-task 24.1:** Create `scripts/teardown-aws.sh` with commands to delete:
        - ECS Service
        - ECS Cluster
        - Load Balancer
        - Target Group
        - RDS Instance
        - ECR Repository
        - CloudWatch Log Groups
    - [ ] **Sub-task 24.2:** Document the order of deletion (some resources depend on others)
    - [ ] **Sub-task 24.3:** Add a comment: "Why must you delete the ECS service before deleting the cluster?"

### Phase 12: Documentation & Retrospective

- [ ] **Task 25: Plan Your Commits**
    > *Description: Deployment work spans multiple systems. Plan logical boundaries.*
    - [ ] **Sub-task 25.1:** Review the commit strategy from the sprint instructions
    - [ ] **Sub-task 25.2:** Identify at least 5 logical commits for this sprint:
        1. **chore(docker): add Dockerfile and docker-compose for local development**
           - Multi-stage Dockerfile, .dockerignore, docker-compose.yml
        2. **feat(deploy): configure AWS infrastructure (VPC, RDS, ECR)**
           - Document AWS setup steps, security group configuration
           - Use body to explain network architecture decisions
        3. **feat(deploy): create ECS task definition and service**
           - Task definition JSON, service configuration
           - Use body to explain Fargate vs EC2 choice
        4. **feat(deploy): add load balancer and HTTPS configuration**
           - ALB setup, target groups, SSL certificate
        5. **ci(deploy): automate deployment with GitHub Actions**
           - CD workflow, AWS credentials setup
           - Use body to explain blue-green deployment strategy (if implemented)
    - [ ] **Sub-task 25.3:** Write down your planned commit messages before finalizing

- [ ] **Task 26: Create Deployment Runbook**
    > *Description: Document every step so others (or future you) can reproduce the deployment.*
    - [ ] **Sub-task 26.1:** Create `docs/DEPLOYMENT.md` with:
        - Prerequisites (AWS account, CLI tools)
        - Step-by-step deployment instructions
        - Architecture diagram
        - Troubleshooting guide (e.g., "Tasks won't start - check security groups")
        - Cost estimates
        - Teardown instructions
    - [ ] **Sub-task 26.2:** Include screenshots of AWS console configurations
    - [ ] **Sub-task 26.3:** Add a "Common Issues" section based on problems you encountered

- [ ] **Task 27: Update Project Documentation & Version**
    > *Description: Reflect the new production deployment capability.*
    - [ ] **Sub-task 27.1:** Update root `README.md` to:
        - Add a "Live Demo" link (your ALB URL or domain)
        - Mention cloud deployment
        - Link to deployment documentation
    - [ ] **Sub-task 27.2:** Update `docs/ARCHITECTURE.md` to include:
        - AWS infrastructure diagram
        - Description of cloud architecture
    - [ ] **Sub-task 27.3:** Increment version in `.csproj` following SemVer:
        - Since this adds production deployment (major feature), increment MINOR version (e.g., 0.10.0 → 0.11.0)
        - OR: If you consider this production-ready, jump to 1.0.0 (first stable release)
    - [ ] **Sub-task 27.4:** Create a conventional commit: `feat: deploy API to AWS with ECS, RDS, and ALB`
    - [ ] **Sub-task 27.5:** Create an annotated Git tag: `git tag -a v1.0.0 -m "First production release - deployed to AWS"`
    - [ ] **Sub-task 27.6:** Push commits and tags to remote: `git push && git push --tags`

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

1.  What is the difference between a **Virtual Machine** and a **Container**? Why are containers lighter and faster to start?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  Explain the difference between a **Docker image** and a **Docker container**. Use an analogy (e.g., class vs object).
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  What is a **multi-stage Dockerfile**? Why is it better than a single-stage Dockerfile for production deployments?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  What is **AWS ECS**? What is the difference between ECS with EC2 launch type vs ECS with Fargate launch type?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  What is an **Application Load Balancer (ALB)**? Why do you need one in front of your ECS tasks?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

6.  What is **AWS RDS**? How does it differ from running MySQL in a Docker container on an EC2 instance?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

7.  What is the purpose of **AWS Secrets Manager**? Why shouldn't you store database passwords in environment variables or hardcode them?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** You were instructed to deploy your API using AWS Fargate (serverless containers) instead of managing EC2 instances yourself. What are the trade-offs?
    - Cost: Is Fargate more expensive than EC2 for the same workload?
    - Control: What do you lose by using Fargate instead of EC2?
    - Scalability: How does auto-scaling differ between the two?
    - Maintenance: Who manages security patches?
    
    When would you choose EC2 over Fargate?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  **The Database Dilemma:** You deployed your MySQL database using AWS RDS. What would happen if:
    - The RDS instance crashes?
    - The database runs out of storage?
    - You need to restore to yesterday's data?
    
    How does RDS handle these scenarios differently than a self-managed database? What is a "maintenance window"?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  **Security in Depth:** Your architecture has multiple security layers:
    - Security groups on the ALB (allow 80/443 from anywhere)
    - Security groups on ECS tasks (allow 8080 from ALB only)
    - Security groups on RDS (allow 3306 from ECS only)
    - Database credentials in Secrets Manager
    
    Walk through what happens if a hacker tries to:
    - (A) Access the database directly from the internet
    - (B) Access the API tasks directly, bypassing the load balancer
    - (C) Extract database credentials from the ECS task environment
    
    Which layer blocks each attack?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  **The Blue-Green Deployment Challenge:** Imagine you push a code change with a critical bug. The new version deploys to ECS and starts serving traffic. Users immediately report errors.
    - How quickly can you rollback to the previous version?
    - What is "blue-green deployment"? How does it enable zero-downtime rollbacks?
    - How would you implement blue-green deployment with ECS? (Hint: use target groups)
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  **Cost Analysis:** You're running:
    - 2 Fargate tasks (0.25 vCP