# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Schedule-2** is a Unity 6 (6000.1.0f1) sandbox/simulation game — a first-person life-sim combining farming, NPC interaction, dialogue, quests, and a day-night cycle with trading mechanics.

## Build & Development

This is a Unity project. There are no custom build scripts or CLI commands — use the Unity Editor directly:
- Open in Unity Hub with Unity 6000.1.0f1
- Main scene: `Assets/Scenes/City.unity`
- Test scene: `Assets/Scenes/Test.unity`

The project uses Unity's new **Input System** (v1.14.2). If you edit `Assets/Settings/PlayerInput.inputactions`, regenerate `PlayerInputActions.cs` via the Input Actions editor in Unity.

## Key Third-Party Packages

- **ECM2 (Easy Character Movement 2)** v1.4.2 — kinematic character controller; player scripts inherit from `ECM2.Character`
- **Easy Weapon** — weapon/projectile system integrated with both player and NPC
- **HeneGames Dialogue System** — the dialogue framework in `Assets/Scripts/DialogueSystem/`
- **URP** v17.1.0 — rendering pipeline
- **AI Navigation** v2.0.7 — NavMesh for NPC pathfinding

## Architecture

### Input & Player
- `Player.cs` — singleton (`Player.Instance`); owns and initializes `PlayerInputActions`, holds global game state
- `PlayerInputActions.cs` — auto-generated wrapper for the Input Actions asset; do not edit by hand
- `FirstPersonCharacter.cs` / `FirstPersonCharacterInput.cs` — ECM2 integration layer for movement and input
- `FirstPersonController.cs` — camera look, FOV zoom, sprint with stamina, headbob, crosshair rendering

### NPC / AI
- `NPCController.cs` — all NPC behavior: NavMesh pathfinding, two modes (Peaceful = waypoint patrol, Aggressive = pursue/shoot player), alert radius, ragdoll on death
- `Respawner.cs` — deactivates dead NPCs, respawns after 10 seconds
- `RoadWaypoint.cs` — defines pedestrian waypoint routes for peaceful NPCs
- NPCs use `Awaitable` (Unity 6 async) for death delays

### Time / Day-Night
- `TimeManager.cs` (`Assets/Scripts/UI/`) — central clock; configurable `secondsPerHour`, fires events at specific times (music, plant growth), tracks days/hours/minutes
- `ClockTime` struct — custom value type with operator overloads (`>`, `<`, `==`, `+`) for time comparisons
- `SunManager.cs` — rotates directional light based on `TimeManager` period

### Dialogue
- `DialogueManager.cs` / `DialogueUI.cs` — sentence-by-sentence flow, audio playback, UnityEvent hooks for start/next/end
- `DialogueTrigger.cs` — collision-based or input-based trigger to start dialogue sequences

### Quest System (`Assets/Scripts/QuestSystem/`)
- `Quest.cs` / `QuestManager.cs` — condition-based state machine (Ready → Active → Complete)
- `QuestActivator.cs` — shows/hides GameObjects based on quest conditions
- `QuestProgressor.cs` — advances quest when conditions are met
- Conditions extend `ConditionBase` (serializable, extensible)

### Farming
- `Plant.cs` — grows from scale 0.1→1.0 over configurable time (driven by `TimeManager`); harvests via click when mature
- `Pot.cs` — holds one plant, highlights green when selectable
- `Tool.cs` — screen-center raycast interaction; right-click to select, left-click to act on plants/pots

### Inventory & Trading (`Assets/Scripts/UI/`)
- `Inventory.cs` / `Shop.cs` — dynamic slot UI, buy/sell modes, token system
- `ItemData.cs` — ScriptableObject defining items (price, amount, sprite); fires change events
- `Storage.cs` — ScriptableObject-based persistent item containers

### Health / Damage
- `StatsHandler.cs` — `OnDeath` and `OnHealthChanged` C# events; clamps health to `[0, maxHealth]`
- `PlayerUIHandler.cs` — subscribes to `StatsHandler` events to update health slider UI

### Teleportation
- `Teleporter.cs` — trigger zone + E-key activates teleport to a target transform

## Code Patterns

- **Singleton:** `Player.cs` uses a static `Instance`
- **Event-driven:** Health, dialogue, and quest systems communicate via C# events — prefer adding event subscribers over polling
- **ScriptableObjects for data:** `ItemData`, `Storage`, and seed definitions are ScriptableObject assets
- **Raycast interaction:** Tool/object interaction uses screen-center raycasts, not collider callbacks
- **Async:** Use Unity 6's `Awaitable` (not `Task`) for coroutine-style async in MonoBehaviours (see `NPCController.cs`)
