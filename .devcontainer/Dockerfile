FROM mcr.microsoft.com/dotnet/sdk:9.0


RUN apt-get update && apt-get install -y \
    git \
    curl \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /workspace
