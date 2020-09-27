# Chat
A simple chat application using WPF, Material Design, RabbitMq, and a .net core API

## Quickstart
1. Install RabbitMq https://www.rabbitmq.com/download.html
2. Create Server's local database by applying EF Core migrations
   - Set Api as startup project
   - Open Package Manager Console
   - Run command `Update-Database`
3. Start debugging!

## Features
- Register user
- Update user profile
- Create or delete a chat room
- Add or remove users from a chat room
- Send messages
- Edit messages
- Delete messages