@echo off
"..\src\Tools\AddCodeFileHeader\bin\Release\AddCodeFileHeader" "..\src"
echo AddCodeFileHeader DONE
"..\src\Tools\MetadataGenerator\bin\Release\MetadataGenerator" "..\src"
echo MetadataGenerator DONE
"..\src\Tools\CodeGenerator\bin\Release\CodeGenerator" "..\src"
echo CodeGenerator DONE
pause