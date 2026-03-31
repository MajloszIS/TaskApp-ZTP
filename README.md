# 🧠 TaskApp – Design Patterns Task Management System

## 📌 Description
TaskApp is a console-based application for managing notes, tasks, and folders, designed with a strong focus on software architecture and design patterns.

The main goal of this project was to implement and integrate multiple design patterns into a single, cohesive system, demonstrating clean architecture and separation of concerns.

The application supports user authentication, task management, hierarchical folder structures, sharing between users, and advanced features such as undo/redo and state restoration.

---

## 🚀 Features
- User authentication (register/login/logout)
- Notes and tasks management
- Folder hierarchy (nested structure)
- Sharing items between users
- Pinning important items
- Cloning items
- Undo / Redo system
- Backup & restore (state snapshots)
- Real-time notifications (observer system)

---

## 🧠 Design Patterns Used

### 🔐 Proxy (Access Control + Cache)
Controls access to items and centralizes permission checking without modifying core logic.  
Also introduces a simple caching mechanism. :contentReference[oaicite:0]{index=0}  

---

### 🎨 Decorator
Extends item functionality dynamically (e.g. pinned items) without modifying base classes.  
Allows adding new behaviors in a flexible and scalable way. :contentReference[oaicite:1]{index=1}  

---

### 🌳 Composite
Implements a tree structure for folders and items.  
Enables uniform handling of both single elements and groups. :contentReference[oaicite:2]{index=2}  

---

### 🧬 Prototype
Allows cloning of items and entire folder structures.  
Used also for creating snapshots of object state (undo/redo system). :contentReference[oaicite:3]{index=3}  

---

### 🧱 Facade
Provides a simplified interface (`TaskAppFacade`) for interacting with the system.  
Hides internal complexity from the UI layer. :contentReference[oaicite:4]{index=4}  

---

### ⚙️ Command
Encapsulates all operations (add, edit, delete, etc.) as objects.  
Enables implementation of undo/redo functionality. :contentReference[oaicite:5]{index=5}  

---

### 💾 Memento
Stores snapshots of object states for backup and restore functionality.  
Works together with Command to support undo/redo. :contentReference[oaicite:6]{index=6}  

---

### 🔔 Observer
Decouples business logic from UI and notifications.  
Automatically updates views and logs when changes occur. :contentReference[oaicite:7]{index=7}  

---

## ⚙️ Architecture Overview
The system is built around a layered architecture:

- **Facade (TaskAppFacade)** → main entry point
- **Services (ItemManager, AuthService)** → business logic
- **Commands & History** → operation handling + undo/redo
- **Observers** → UI updates and notifications
- **Models (IItem, Note, Tasky, ItemGroup)** → domain layer

---

## 🛠️ Tech Stack
- C#
- .NET (Console Application)
- OOP & Design Patterns
- LINQ
- Collections (List, Dictionary, HashSet)

---

## 🧠 What I learned
- Practical usage of design patterns in a real system
- Building scalable and maintainable architecture
- Separation of concerns and abstraction
- Implementing undo/redo using Command + Memento
- Designing extensible systems (Decorator, Composite)

---

## 🔄 Future improvements
- GUI (e.g. WPF or web frontend)
- Database integration (currently in-memory)
- Role-based access control
- Better error handling and logging
- API layer (REST)

---
