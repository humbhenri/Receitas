DOTNET=${HOME}/.dotnet/dotnet

.PHONY=test run

test:
	${DOTNET} test

run:
	${DOTNET} run --project Backend/MyRecipeBook.API/MyRecipeBookAPI.csproj
