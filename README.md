# Engie coding game

A brief description of your project goes here.

## Getting Started

These instructions will help you set up and run the project on your local machine.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.X.X)
- [Docker](https://www.docker.com/get-started) (optional)

### Build and Run

**Using .NET SDK:**

```bash
# Clone the repository
git clone https://github.com/Varsium/powerplant-coding-challenge.git

# Navigate to the project directory
cd powerplant-coding-challenge

# Build the project
dotnet build

# Run the API
dotnet run --project Engie.Api
```
**Using Docker (Optional):**

# Clone the repository
git clone https://github.com/Varsium/powerplant-coding-challenge.git

# Navigate to the project directory
cd powerplant-coding-challenge

# Build the Docker image
docker build -t engie-api-leandro ./Engie.Api

# Run the Docker container
docker run -p 8888:8888 engie-api-leandro

