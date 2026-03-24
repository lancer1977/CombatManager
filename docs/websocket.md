# WebSocket Server Documentation

The CombatManager WebSocket API provides real-time bidirectional communication for chat, notifications, and combat state updates.

## Architecture

### Components

- **CombatManager.Websocket.Console** - Console test client for WebSocket connections
- **CombatManager.Api.NotificationApiChat** - Client library for connecting to the WebSocket server
- **CombatManager.Api.Core.Data.RemoteServiceMessage** - Message schema for all communications

### Connection Endpoint

```
ws://localhost:12457/api/notification
```

## Connection Lifecycle

### 1. Connect

```csharp
var chat = new NotificationApiChat("ws://localhost:12457/api/notification");
chat.StateChanged += (sender, message) =>
{
    Console.WriteLine($"{message.Name}: {message.Data}");
};
await chat.StartConnection(cancellationToken);
```

### 2. Keep-Alive

The client automatically sends WebSocket ping frames to maintain the connection. If the connection is lost, `StartConnection` will attempt to reconnect automatically.

### 3. Disconnect

The connection closes when:
- The client sends a close frame (`Console.ReadLine()` returns null)
- The server closes the connection
- Network failure occurs (auto-reconnect kicks in)

## Message Format

All messages use JSON serialization via `Newtonsoft.Json`:

```csharp
public class RemoteServiceMessage
{
    public string Name { get; set; }  // Message type identifier
    public int ID { get; set; }       // Optional message ID
    public object Data { get; set; }  // Message payload (any serializable object)
}
```

### Example Messages

**Incoming (Server → Client):**
```json
{
    "Name": "ChatMessage",
    "ID": 123,
    "Data": { "user": "GM", "text": "Combat started!" }
}
```

**Outgoing (Client → Server):**
```json
{
    "Name": "SendChat",
    "ID": 456,
    "Data": { "text": "Roll initiative" }
}
```

## Usage Example

### Console Client

```csharp
using CombatManager.Api;

class Program
{
    static async Task Main(string[] args)
    {
        var chat = new NotificationApiChat("ws://localhost:12457/api/notification");
        chat.StateChanged += (sender, msg) =>
        {
            Console.WriteLine($"{msg.Name}: {msg.Data}");
        };
        
        var cts = new CancellationToken();
        await chat.StartConnection(cts);
    }
}
```

### Event Handling

Subscribe to `StateChanged` to receive all incoming messages:

```csharp
chat.StateChanged += (sender, message) =>
{
    switch (message.Name)
    {
        case "CombatUpdate":
            // Handle combat state update
            break;
        case "ChatMessage":
            // Handle incoming chat
            break;
    }
};
```

## Key Files

| File | Purpose |
|------|---------|
| `CombatManager.Api/NotificationApiChat.cs` | Main WebSocket client implementation |
| `CombatManager.Api.Core/Data/RemoteServiceMessage.cs` | Message schema definition |
| `CombatManager.Api/NetworkingExtensions.cs` | Network utilities |
| `CombatManager.Websocket.Console/Program.cs` | Test client example |

## Error Handling

The client handles connection errors by logging them and automatically attempting reconnection:

```csharp
try
{
    await Connect(cts);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);  // Log error
    // StartConnection will auto-reconnect
}
```

## Notes

- The WebSocket server runs on port `12457` by default
- Messages are JSON-encoded using Newtonsoft.Json
- The connection uses a ping/pong keep-alive mechanism
- Auto-reconnect is built into the `StartConnection` loop
