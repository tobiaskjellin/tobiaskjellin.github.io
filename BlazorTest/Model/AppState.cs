using System;
using System.Collections.Generic;

public class AppState
{
    public HashSet<JsonObject> JsonObjects { get; set; } = new HashSet<JsonObject>();

    public List<TestResult> TestResult { get; set; } = new List<TestResult>();
    public string FileContent { get; set; }

    public event Action OnChange;

    public void SetProperty(HashSet<JsonObject> value)
    {
        foreach (var item in value)
            JsonObjects.Add(item);

        foreach (var item in JsonObjects)
            item.IsExpanded = false;

        NotifyStateChanged();
    }
    public void SetContent(string value)
    {
        FileContent = value;
        NotifyStateChanged();
    }
    public void SetResult(List<TestResult> testResult)
    {
        TestResult = testResult;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
