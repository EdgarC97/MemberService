# Use the official .NET SDK 8.0 image as the build environment.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Set the working directory inside the build container.
WORKDIR /app

# Copy the project file(s) into the container.
# This allows for caching of the restore step if the csproj hasn't changed.
COPY *.csproj ./
# Restore dependencies for the project, using --no-cache to ensure a clean restore.
RUN dotnet restore --no-cache

# Copy the rest of the application's source code into the container.
COPY . ./
# Publish the application in Release configuration to the 'out' folder.
RUN dotnet publish -c Release -o out 

# Use the official ASP.NET Core 8.0 runtime image for the execution environment.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Set the working directory in the runtime container.
WORKDIR /app
# Copy the published output from the build container to the runtime container.
COPY --from=build /app/out .
# Expose port 8080 so the application can be accessed from outside the container.
EXPOSE 8080

# Note: The .env file is not copied to the production image.
# Environment variables are passed to the container at runtime.

# Configure the application to listen on port 8080.
ENV ASPNETCORE_URLS=http://*:8080

# Set the entry point to run the application.
ENTRYPOINT ["dotnet", "MemberService.dll"]
