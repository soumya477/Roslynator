@echo off
"..\src\Tools\MetadataGenerator\bin\Debug\MetadataGenerator" "..\src"
echo MetadataGenerator DONE
"..\src\Tools\CodeGenerator\bin\Debug\CodeGenerator" "..\src"
echo CodeGenerator DONE
pause