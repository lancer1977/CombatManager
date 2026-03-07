# CombatManager Architecture

## WebSocket Server

### Location
- **Server:** `CombatManagerCore/LocalService/CombatManagerNotificationServer.cs`
- **Client:** `CombatManager.Api/NotificationApiChat.cs`
- **Message Model:** `CombatManagerCore/LocalService/LocalServiceMessage.cs`
- **User Model:** `CombatManagerCore/LocalService/User.cs`

### Protocol

#### Connection
- **Endpoint:** `ws://localhost:12457/api/notification/` (note trailing slash)
- **Technology:** EmbedIO WebSockets module (EmbedIO `WebServer` + `WebSocketModule`)
- **Port:** defaults to `12457` (`LocalCombatManagerService.DefaultPort`), configurable via `LocalCombatManagerService(…, port)`

#### Message Format
```json
{
  "Name": "Turn",        // Message type (Added, Removed, Turn, Users, etc.)
  "ID": 1,               // Auto-incrementing message ID
  "Data": { ... }        // Payload (varies by message type)
}
```

#### Message Types
| Name | When Sent | Data |
|------|-----------|------|
| `Added` | Character added to combat | Character object |
| `Removed` | Character removed | Character ID |
| `Turn` | Turn changed | Current character |
| `Users` | Client connected/disconnected | List of connected users |

#### Connection Lifecycle
1. Client connects to WebSocket endpoint
2. Server adds User to list, broadcasts `Users` message
3. Server broadcasts state changes (`Added`, `Removed`, `Turn`)
4. Client disconnects, server removes from user list (currently **no** `Users` broadcast on disconnect)

### Startup Requirements
- CombatManager must be running (server is embedded in main app via `LocalCombatManagerService.Start()`)
- Port 12457 must be available (or set a different port)
- Server binds to `http://*:{port}` (see `LocalCombatManagerService.Start`) and upgrades connections under `/api/notification/`
- No authentication required (local-only)

### Client Usage
```csharp
var chat = new NotificationApiChat("ws://localhost:12457/api/notification/");
chat.StateChanged += (sender, state) => Console.WriteLine(state.Name + ":" + state.Data);
await chat.StartConnection(cancellationToken);
```
