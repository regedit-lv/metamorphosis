Project is written on C# using Visual Studio 2011.
Main idea of project is to generate code that will serialize your structures to some buffer, xml or somewhere else.
Currently supports:
C#, C++ serialization to buffer and xml (using tinyXml for C++). Java supports only xml serialization.
How it works:
You have file with declaration of your structure and you have some files with serialization rules. Rules can be changed for each project or language.