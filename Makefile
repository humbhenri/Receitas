DOTNET=${HOME}/.dotnet/dotnet

test:
	${DOTNET} test

run:
	${DOTNET} run --project Backend/MyRecipeBook.API/MyRecipeBookAPI.csproj
