To add new migration you need to run this command in the terminal from root directory.

dotnet ef migrations add initial --project src/Infrastructure/Ethereum.Connector.Infrastructure --startup-project src/Web/Ethereum.Connector.API --output-dir Persistence/Migrations