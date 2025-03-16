using System.ComponentModel;

namespace Realtime;

public class MemoryContext(Func<string, Task> addMessage)
{
    [Description("Determines whether system contains memory with a specified tag")]
    public async Task<bool> CheckMemory(string tags)
    {
        await addMessage($"Checking memory for {tags}");
        return false;
    }

    [Description("Adds memory with specific tag and content")]
    public async Task SetMemory(string tag, string content)
    {
        await addMessage($"***** Added memory {content}. [{tag}]");
    }
}
