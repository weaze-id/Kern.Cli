echo "Cleaning up code..."

dotnet clean
dotnet restore

dotnet jb cleanupcode Kern.Cli.sln --verbosity=OFF --exclude=lib/**/*
echo "Done!"