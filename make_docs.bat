@echo off

REM Generate markdown docs from the generated XML documentation file.
REM Mad props to DefaultDocumentation, which we have installed as a local tool.
REM See: https://github.com/Doraku/DefaultDocumentation

REM Sagara.Core
dotnet defaultDocumentation -a "src\Sagara.Core\bin\Debug\net8.0\Sagara.Core.dll" -o "src\Sagara.Core\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"

REM Sagara.Core.Caching
dotnet defaultDocumentation -a "src\Sagara.Core.Caching\bin\Debug\net8.0\Sagara.Core.Caching.dll" -o "src\Sagara.Core.Caching\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"

REM Sagara.Core.Logging.Serilog
dotnet defaultDocumentation -a "src\Sagara.Core.Logging.Serilog\bin\Debug\net8.0\Sagara.Core.Logging.Serilog.dll" -o "src\Sagara.Core.Logging.Serilog\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"
