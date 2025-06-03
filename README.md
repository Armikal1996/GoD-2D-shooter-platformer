# 🔫 Unity RPG Test Task Project

This Unity project was developed as a technical test assignment to demonstrate architectural design, UI integration, character and monster behavior, and persistent data handling. It serves as a foundational base for future gameplay expansion.

---

## 📌 Features & Functionality

### 🎮 Character & Input System
- **Joystick-based movement** using the free [Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631) from the Unity Asset Store.
- **Fire Button**: Allows the character to shoot a bullet. Guns shoot at the current direction of the joystick.
- **Health Bar**: The player has a visible health bar. When HP reaches 0, the character dies.
- **Restart Button**: Allows the player to start over, keeping all of his items from pervious sessions.

### 🗺️ Environment
- **Tilemap system**: Custom-designed tilemap used as the terrain for character and enemy movement.
- **Camera Control**: Smooth camera follow with X/Y clamping using customizable boundaries.

### 🎒 Inventory System
- **Backpack UI**: Top-right UI panel that shows the player's collected items.
- **Stacking Items**: If stackable, shows item quantity; otherwise, only the icon.
- **Delete Button**: When clicking on an item slot, a delete button appears. Clicking it removes the item from inventory permanently.

### 👾 Monsters
- **Spawn System**: Three monsters spawn at random positions when the game starts. When all dead spawns new ones.
- **AI**:
  - Monsters detect the player in range and follow them.
  - Attack when within range.
  - Each has its own health bar and dies upon reaching 0 HP, dropping some Ammo.
- **Loot Drop**: An Ammo item drops when a monster dies, which the player can collect by getting close.

### 💾 Persistence
- **Data Saving**: All game data (inventory, health, etc.) is stored between sessions using a custom save system.
- ⚠️ *No use of `PlayerPrefs`; data is serialized and saved to disk.*

---

## 🧱 Architecture Notes

- **Modular components**:
  - `PlayerCombat`, `InventoryManager`, `Monster`, `CameraController`, and `InventoryItemSaveSystem` are isolated for reusability.
- **Scriptable Objects**: Used for defining item types and properties.
- **Event-driven design**: Player-monster interaction, UI updates, and animation event triggers use decoupled event logic.

---

## ⏱️ Development Time

> Total time to complete the task: **~12 hours**

- Base project setup, architecture, tilemap: **1h**
- Player + joystick movement & fire mechanics: **2.5h**
- Monster AI and damage system: **2h**
- Inventory + delete system: **1.5h**
- Camera control & UI integration: **2.5h**
- Data saving/load system: **2h**
- Testing & polishing: **1h**

---

## ✅ Requirements

- Unity version: **2021.3 LTS or later**
- Packages:
  - Joystick Pack (Free): https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631

---

## 🧪 Test Instructions

1. Launch the game.
2. Move the character using the joystick.
3. Approach a monster to trigger its AI.
4. Defeat it using the Fire button.
5. Collect dropped item by walking close to it.
6. Open backpack and remove item using the delete button.

---

## 📌 Additional Notes

- This project avoids tightly coupling systems.
- It is extendable for multiplayer, more enemy types, crafting, or quests.
