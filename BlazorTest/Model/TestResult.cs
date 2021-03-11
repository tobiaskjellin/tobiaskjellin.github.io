using System.Collections.Generic;
using System.Text.Json;

public class TestResult
{
    public string Name { get; set; }
    public string Result { get; set; }

    public TestResult(string name, string value)
    {
        Name = name;
        Result = value;
    }
    public TestResult()
    {
        
    }
}