build: 
	dotnet build

runapp: ./src/Labs.App/bin/Release/net7.0/Labs.App ./data.json
	./src/Labs.App/bin/Release/net7.0/Labs.App ./data.json

