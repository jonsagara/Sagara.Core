@echo off

REM Generate markdown docs from the generated XML documentation file.
REM Mad props to DefaultDocumentation, which we have installed as a local tool.
REM See: https://github.com/Doraku/DefaultDocumentation

REM Sagara.Core
echo [Sagara.Core] Generating docs...
dotnet defaultDocumentation -a "src\Sagara.Core\bin\Debug\net8.0\Sagara.Core.dll" -o "src\Sagara.Core\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"
echo [Sagara.Core] Done.

REM Sagara.Core.AspNetCore
echo [Sagara.Core.AspNetCore] Generating docs...
dotnet defaultDocumentation -a "src\Sagara.Core.AspNetCore\bin\Debug\net8.0\Sagara.Core.AspNetCore.dll" -o "src\Sagara.Core.AspNetCore\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"
echo [Sagara.Core.AspNetCore] Done.

REM Sagara.Core.Caching
echo [Sagara.Core.Caching] Generating docs...
dotnet defaultDocumentation -a "src\Sagara.Core.Caching\bin\Debug\net8.0\Sagara.Core.Caching.dll" -o "src\Sagara.Core.Caching\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"
echo [Sagara.Core.Caching] Done.

REM Sagara.Core.Data
echo [Sagara.Core.Data] Generating docs...
dotnet defaultDocumentation -a "src\Sagara.Core.Data\bin\Debug\net8.0\Sagara.Core.Data.dll" -o "src\Sagara.Core.Data\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"
echo [Sagara.Core.Data] Done.

REM Sagara.Core.Logging.Serilog
echo [Sagara.Core.Logging.Serilog] Generating docs...
dotnet defaultDocumentation -a "src\Sagara.Core.Logging.Serilog\bin\Debug\net8.0\Sagara.Core.Logging.Serilog.dll" -o "src\Sagara.Core.Logging.Serilog\docs" --ConfigurationFilePath ".\DefaultDocumentation.json"
echo [Sagara.Core.Logging.Serilog] Done.
