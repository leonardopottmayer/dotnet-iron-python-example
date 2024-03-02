using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.Dynamic;

class Program
{
    static void Main()
    {
        ScriptEngine engine = Python.CreateEngine();

        // Load the python script
        var scriptPath = "S:\\API\\IronPythonExample\\IronPythonExample\\script.py";
        var source = engine.CreateScriptSourceFromFile(scriptPath);

        ScriptScope scope = engine.CreateScope();
        source.Execute(scope);

        HelloWorld(scope);
        DoMagic(scope);
        GetListLength(scope);
        GetAmountOfKeysInObject(scope);
    }

    static void HelloWorld(ScriptScope scope)
    {
        // Gets the 'get_hello_world' function in script.py file
        dynamic getHelloWorld = scope.GetVariable("get_hello_world");

        // Executes the 'get_hello_world' function
        string helloWorld = getHelloWorld();

        Console.WriteLine(helloWorld);
    }

    static void DoMagic(ScriptScope scope)
    {
        // Gets the 'do_magic' function in script.py file
        dynamic doMagic = scope.GetVariable("do_magic");

        // Defines a parameter, wich will be passed to the 'do_magic' function
        int doMagicParam = 5;

        // Executes the 'do_magic' function and gets the result, mapping it to a list
        List<int> doMagicResult = ((PythonList)doMagic(doMagicParam)).Cast<int>().ToList();

        string str = "";

        foreach (var item in doMagicResult)
        {
            str += item.ToString() + ", ";
        }

        Console.WriteLine("Do magic result: " + str);
    }

    static void GetListLength(ScriptScope scope)
    {
        // Gets the 'get_list_length' function in script.py file
        dynamic getListLength = scope.GetVariable("get_list_length");

        // Executes the 'get_list_length' function
        var usersList = new List<string>() { "User 1", "User 2", "User 3" };
        int listLength = (int)getListLength(usersList);

        Console.WriteLine("List length: " + listLength);
    }

    static void GetAmountOfKeysInObject(ScriptScope scope)
    {
        // Gets the 'get_amount_of_keys_in_object' function in script.py file
        dynamic getAmountOfKeysInObject = scope.GetVariable("get_amount_of_keys_in_object");

        // Creates an object and passes it to the 'get_amount_of_keys_in_object' function
        var testObject = new MyClass() { Name = "Leonardo", Age = 21 };

        // Converts the object to a dynamic ExpandoObject
        dynamic dynamicObject = ConvertToExpando(testObject);

        // Executes the 'get_amount_of_keys_in_object' function with the dynamic object
        int getKeysResult = (int)getAmountOfKeysInObject(dynamicObject);

        Console.WriteLine("Amount of keys in object: " + getKeysResult);
    }

    // Helper method to convert an object to an ExpandoObject
    static dynamic ConvertToExpando(object obj)
    {
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (var property in obj.GetType().GetProperties())
        {
            expando[property.Name] = property.GetValue(obj);
        }
        return expando;
    }
}

// Example class
class MyClass
{
    public string Name { get; set; }
    public int Age { get; set; }
}