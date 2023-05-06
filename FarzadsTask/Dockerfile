

# # Set the base image to the official .NET 6 SDK image
# FROM mcr.microsoft.com/dotnet/sdk:6.0.408 AS build-env

# # Set the working directory
# WORKDIR /app

# # Copy the project file and restore dependencies
# COPY *.csproj ./
# RUN dotnet restore

# # Copy the rest of the application code
# COPY . ./



# # Build the application
# WORKDIR /app
# RUN dotnet publish -c Release -o out

# # Build the runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:6.0.4 AS runtime-env

# # Set the working directory
# WORKDIR /https

# # Copy the SSL certificate
# COPY aspnetapp.pfx .

# WORKDIR /app

# # Copy the published output from the build environment
# COPY --from=build-env /app/out .

# # Set the environment variables for HTTPS
# ENV ASPNETCORE_URLS=https://+:44342;http://+:80
# ENV ASPNETCORE_HTTPS_PORT=44342
# ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123456
# ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# # Expose the ports for HTTP and HTTPS
# EXPOSE 80
# EXPOSE 44342

# # Start the application
# ENTRYPOINT ["dotnet", "FarzadsTask.dll"]



# Set the base image to the official .NET 6 SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0.408 AS build-env

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.4 AS runtime-env

# Set the working directory
WORKDIR /app

# Copy the published output from the build environment
COPY --from=build-env /app/out .

# Set the environment variables for Swagger and HTTPS
# ENV ASPNETCORE_URLS=https://+:443;http://+:80
ENV ASPNETCORE_URLS=https://+:44342;http://+:80
ENV ASPNETCORE_HTTPS_PORT=44342
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123456
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Copy the SSL certificate
COPY aspnetapp.pfx /https/

# Expose the ports for HTTP and HTTPS
# EXPOSE 80
# EXPOSE 443
EXPOSE 80
EXPOSE 44342

# Start the application
ENTRYPOINT ["dotnet", "FarzadsTask.dll"]

# # 1---> dotnet dev-certs https -ep aspnetapp.pfx -p 123456

# # 3---> docker run -p 80:80 -p 44342:44342 -e ASPNETCORE_ENVIRONMENT=Production farzadstask:v1























