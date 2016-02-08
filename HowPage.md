# Introduction #

If you want to serialize you data structure to some format. You can describe your structures in metamorphosis format and then generate methods that will serialize your data to anywhere.

# Where to use? #
Main situations where it can be used:
  * If you need to create communication protocol between A and B. A and B can be both C++, C#, Java or mixed.
  * To store your application settings in xml. Read and write with one line of code.
  * Import/Export data between applications.
You will have only one file with definition and **metamorphosis** will generate code that will serialize your structures to byte array, xml or somewhere else if you will need it.

# Example #
For exmple we need to store information about human (name, address). Let's define human structure and some extra additional stuff

```
Namespace = test.namespace

## generate enum for gender
enum Genre
{
    None = 0 : none, ## specify int value after '=' and description after ':'
    Male : male,
    Female : female,
}

## generate static method that will be use during xml serialization
## important: the name must be <EnumName>Helper. According to xml serialization rules. If you want different name you will be needed to change the rules
static GenreHelper : Genre
{
    bool initDone = false;
    Map Genre String valueToName;
    Map String Genre nameToValue;
} : getEnumName getEnumValue initEnum;

## structure to save address
struct Address
{
    String city;
    String country = "Latvia"; ## you can specify default value
    String street;
} : read write size toXml fromXml;

## main structure for human
struct Human
{
    Genre genre = Genre.None; ## default value for enum
    String name;
    Array String phones; ## assume that people can have more that one phone number
    Address address;
    Array Human childs; ## to save information about childs
} : read write size toXml fromXml;
```
after that you can generate code with one command line like this:
metamorphosis.exe human.met -ol c++ -df Cpp\all.def -op Generated -on Human
It will create cpp and h files inside "Generated" folder.
After that you can write something like:
```
Human h;
h.name = "John";
h.phones.push_back("+371 2222222");
h.address.city = "Riga";
....
and serialize it to somewhere. For example to xml:
std::string xml = h.toXml();
```
**Attention!**
You will need some extra files (some wrapper classes). For example for xml serialization you will need tinyXml. But all files are include into project. So you can easily find them inside.