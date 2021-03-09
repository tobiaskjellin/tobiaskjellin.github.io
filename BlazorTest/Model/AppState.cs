using System;
using System.Collections.Generic;

public class AppState
{
    public HashSet<JsonObject> JsonObjects { get; set; } = new HashSet<JsonObject>();

    public event Action OnChange;

    public void SetProperty(HashSet<JsonObject> value)
    {
        foreach (var item in value)
            JsonObjects.Add(item);

        foreach (var item in JsonObjects)
            item.IsExpanded = false;

        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
