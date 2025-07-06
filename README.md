Steps to deploy manually
1. dotnet build
2. dotnet publish -c Release -r linux-x64 --self-contained false -o publish;
3. cd publish
4. zip -r ../lambda.zip .
5. use handler as: GeoLinks.API
