# Technical Test - ENDROS

# Running Project
dotnet run
do

# Migration Add Field 
dotnet ef migrations add AddImagePathToProduct
dotnet ef database update

# ngrok expose port
ngrok http {port}