# Use the official .NET 6 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY ["CommissionX.Api/CommissionX.Api.csproj", "CommissionX.Api/"]
COPY ["CommissionX.Core/CommissionX.Core.csproj", "CommissionX.Core/"]
COPY ["CommissionX.Application/CommissionX.Application.csproj", "CommissionX.Application/"]
COPY ["CommissionX.Infrastructure/CommissionX.Infrastructure.csproj", "CommissionX.Infrastructure/"]

RUN dotnet restore "CommissionX.Api/CommissionX.Api.csproj"

# Copy the rest of the solution files
COPY . .

# Build the application
RUN dotnet publish "CommissionX.Api/CommissionX.Api.csproj" -c Release -o /app/out

# Use the official .NET 6 runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out .

# Expose the port on which the app will run
EXPOSE 80

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "CommissionX.Api.dll"]
