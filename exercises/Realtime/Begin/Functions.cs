using System;
using System.ComponentModel;

namespace Realtime;

public class MemoryContext(Func<string, Task> addMessage)
{
    List<string> memories = new List<string>();
    const string memoryPath = @"C:\temp\memories.txt";

    public async void TryLoadMemories()
    {
        try
        {
            var memoriesFromDisk = File.ReadAllLines(memoryPath);
            foreach (var mem in memoriesFromDisk)
            {
                memories.Add(mem.Trim());
            }
            await addMessage($"vvvvv Retrieved memories from {memoryPath}");
        }
        catch (Exception ex)
        {
            await addMessage($"xxxxx Error retrieving memories from {memoryPath}");
        }
    }

    [Description("Retrieves previously remembered notes and reminders about specific topic")]
    public async Task<string> RetrieveMemory(string topic)
    {
        // NOTE: the topic can be "everything" or "cars", and it won't match a note that contains "car".
        await addMessage($"Checking memory for {topic}");
        var relevant = memories.Where(n => n.Contains(topic));
        return string.Join(", ", relevant);
    }

    [Description("Adds reminder to take action at specific date")]
    public async Task SetReminder(string action, DateTime date)
    {
        await addMessage($"***** Added reminder {action}. [{date}]");
        AddMemory(action);
    }

    [Description("Take a note on specific topic")]
    public async Task AddNote(string topic)
    {
        await addMessage($"***** Added note {topic}.");
        AddMemory(topic);
    }

    private void AddMemory(string topic)
    {
        memories.Add(topic);
        File.WriteAllText(memoryPath, string.Join("\n", memories));
    }
}
