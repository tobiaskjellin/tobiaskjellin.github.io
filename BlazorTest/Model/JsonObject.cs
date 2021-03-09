using System.Collections.Generic;
using System.Text.Json;

public class JsonObject
{
    public bool IsExpanded { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string Bla { get { return string.Format("{0}{1}", Name, HasChild ? "" : string.Format(" : {0}", Value)); } }
    public JsonValueKind ValueKind { get; set; }
    public HashSet<JsonObject> Children { get; set; }
    public bool HasChild { get { return Children.Count > 0; } }

    public JsonObject(string name, string value, JsonValueKind valueKind)
    {
        Name = name;
        Value = value;
        ValueKind = valueKind;
        Children = new HashSet<JsonObject>();
    }

    public static void IterateJsonDocument(HashSet<JsonObject> list, JsonObject parent, JsonElement element, string name)
    {
        if (element.ValueKind is JsonValueKind.Object)
        {
            var jsonObject = new JsonObject(name, string.Empty, element.ValueKind);
            if (parent != null)
                parent.Children.Add(jsonObject);

            parent = jsonObject;

            if (list.Count == 0)
            {
                list.Add(parent);
            }

            var objProperties = element.EnumerateObject();
            foreach (var property in objProperties)
            {
                IterateJsonDocument(list, parent, property.Value, property.Name);
            }
        }
        else if (element.ValueKind is JsonValueKind.Array)
        {
            var jsonObject = new JsonObject(name, string.Empty, element.ValueKind);
            parent.Children.Add(jsonObject);
            parent = jsonObject;

            var elementArray = element.EnumerateArray();
            foreach (var item in elementArray)
            {
                IterateJsonDocument(list, parent, item, parent.Children.Count.ToString());
            }
        }
        else
        {
            parent.Children.Add(new JsonObject(name, element.ToString(), element.ValueKind));
        }
    }
}