@echo off
"..\src\Tools\AddCodeFileHeader\bin\Release\AddCodeFileHeader" "..\src"
echo AddCodeFileHeader DONE
dotnet "..\src\Tools\MetadataGenerator\bin\Release\netcoreapp2.0\MetadataGenerator.dll" "..\src"
echo MetadataGenerator DONE
dotnet "..\src\Tools\CodeGenerator\bin\Release\netcoreapp2.0\CodeGenerator.dll" "..\src"
echo CodeGenerator DONE
pause