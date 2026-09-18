# ASP.NET Core SignalR Group Chat Application

A real-time, multi-room group chat web application built with **ASP.NET Core MVC** and **SignalR**. This project demonstrates real-time bidirectional communication over WebSockets, room management, and server-to-client event broadcasting.

---

## 🚀 Features

- **Real-Time Group Messaging:** Send and receive messages instantly within specific chat rooms using SignalR groups.
- **Dynamic Room Joining & Leaving:** Users can join new rooms or switch between rooms on the fly.
- **System Notifications:** Automatic notifications broadcasted to room members when users join or leave.
- **Targeted Broadcasting:** Messages are isolated to room members only—clients outside the room do not receive the traffic.
- **Clean Responsive UI:** Built with Bootstrap 5 for a clean desktop and mobile experience.

---

## 🛠️ Tech Stack

- **Backend:** C#, .NET 8.0, ASP.NET Core MVC
- **Real-Time Middleware:** ASP.NET Core SignalR
- **Frontend:** HTML5, JavaScript (ES6+), Bootstrap 5
- **Client Library:** `@microsoft/signalr` JavaScript Client (v8.0)

---

## 📁 Project Structure

```text
SignalIRChat/
├── Hubs/
│   └── ChatHub.cs            # SignalR Hub managing connection lifecycle & room groups
├── Controllers/
│   └── HomeController.cs     # MVC Controller serving the main UI
├── Views/
│   └── Home/
│       └── Index.cshtml      # Chat UI layout & SignalR JavaScript client integration
├── Program.cs                # App startup, SignalR service registration & hub mapping
└── README.md                 # Project documentation
```

---

## 📋 Prerequisites

Make sure you have the following installed on your machine:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher
- A modern web browser (Chrome, Edge, Firefox, Safari)

---

## 🚦 Getting Started

### 1. Clone or Download the Repository

```bash
git clone https://github.com/your-username/SignalIRChat.git
cd SignalIRChat
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run the Application

```bash
dotnet run
```

The app will start up locally. Look for output similar to:
```text
Now listening on: https://localhost:7198
```

### 4. Test Real-Time Communication

1. Open your browser and navigate to `https://localhost:7198` (replace with your local port).
2. Open a **second tab** or an **incognito browser window** to the same URL.
3. In **Window 1**: Enter Username `Alice` and Room `DevTeam`, then click **Join Room**.
4. In **Window 2**: Enter Username `Bob` and Room `DevTeam`, then click **Join Room**.
5. Type messages from either window to observe instant bi-directional chat updates!

---

## ⚡ How It Works

### SignalR Hub (`ChatHub.cs`)

The server-side `ChatHub` class manages connection IDs and handles client requests to join, leave, or send messages to groups.

```csharp
public class ChatHub : Hub
{
    public async Task JoinRoom(string roomName, string userName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("ReceiveSystemMessage", $"{userName} has joined the room.");
    }

    public async Task LeaveRoom(string roomName, string userName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName).SendAsync("ReceiveSystemMessage", $"{userName} has left the room.");
    }

    public async Task SendMessage(string roomName, string userName, string message)
    {
        await Clients.Group(roomName).SendAsync("ReceiveGroupMessage", userName, message);
    }
}
```

### JavaScript Client (`Index.cshtml`)

The client initializes a connection to `/chatHub` and registers event listeners corresponding to backend hub events (`ReceiveGroupMessage` and `ReceiveSystemMessage`).

```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

// Listen for messages broadcasted to the group
connection.on("ReceiveGroupMessage", (user, message) => {
    // Render user message to UI
});

// Listen for join/leave notifications
connection.on("ReceiveSystemMessage", (message) => {
    // Render system notification to UI
});

connection.start();
```

---

## 📝 License

This project is open-source and available under the [MIT License](LICENSE).