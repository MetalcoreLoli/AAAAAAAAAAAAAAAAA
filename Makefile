build: 
	dotnet build

runapp: ./src/Labs.App/bin/Debug/net7.0/Labs.App ./data.json
	./src/Labs.App/bin/Debug/net7.0/Labs.App ./data.json

