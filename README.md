# Technical Test - ENDROS


# Create Project
dotnet new mvc -n {Project} --framework net8.0

# Running Project
dotnet run
donet run wathch (hot reload)

# Migration Add Field 
dotnet ef migrations add AddImagePathToProduct
dotnet ef database update

# ngrok expose port
ngrok http {port}