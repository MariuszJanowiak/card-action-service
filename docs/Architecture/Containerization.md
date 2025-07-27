# Containerization

- `Dockerfile` — Template for building a container image of the API.
- `compose.yaml` — Template for running the API container with Docker Compose.
- `.dockerignore` — Files and folders to exclude from the container build context.

Containerization allows the API to be run in isolated environments, making it ready for production, cloud, or CI/CD workflows.

> No complex Docker setup is required for this assignment.
>  
> The provided templates are ready for future extension.

**How to build and run (optional):**
```bash
docker build -t card-action-api .
docker run -p 5001:5001 card-action-api

