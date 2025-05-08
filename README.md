SilentEscape
Overview

Silent Escape is a virtual reality horror escape room game where players navigate total darkness using only sound. Equipped with Oculus hand controllers, the player emits sonar pulses to visualize their surroundings, interact with puzzles, and avoid a stealth-based monster that hunts sound. The game blends immersive VR design with puzzle-solving, ambient audio, and real-time AI response, delivering a unique horror experience built entirely within Unity. Every mechanic from light-triggered visuals to spatial audio feedback is designed to reinforce fear, challenge, and immersion.

## 🧠 Key Features

### ✅ Sonar System (Player)
- Triggered by VR controller (right/left hand) or head movement
- Emits a glowing wave (SonarWave prefab)
- Detects monsters and highlights interactables in range

### ✅ Monster AI
- Patrols randomly or chases sonar sound origin
- Reacts to ambient sonar light (SonarLightSpawner)
- Breathes audibly while moving using looping audio

### ✅ Interactable Objects
- Use `SonarInteractable.cs` to glow temporarily when pinged
- Emissive material setup with URP + Bloom

### ✅ Puzzles
- Built with `PuzzleBase.cs`
- Custom scripts like `SafePuzzle.cs` allow logic-specific puzzles
- Tracks completion using `PuzzleManager.cs`
- Status lights and sounds on solve

---

## 🎮 Controls

| Action | Input |
|--------|-------|
| Emit Sonar | **Trigger button** (right/left hand) |
| (Optional) Emit Sonar | Quick **head turn** |
| Grab/Interact | XR Grab system (for puzzle buttons, levers, etc.) |

---

## 🔊 Audio System

- **monster_breathing.wav** plays when monster moves
- **sonar_ping.wav** plays when player triggers sonar
- **SonarLight.prefab** optionally emits ping + glow

---

## ⚙️ Unity Setup

### ✅ Required Unity Version:
`2023.1.11f1 (URP enabled)`

### ✅ Package Dependencies:
- XR Plugin Management (Oculus or OpenXR)
- Input System (New or Both)
- Universal RP (URP)
- Post-Processing (for Bloom)

### ✅ URP Bloom Setup:
1. Create a **Global Volume**
2. Add **Bloom override**
3. Set:
   - Threshold: `1.0`
   - Intensity: `1.5`
   - Scatter: `0.7`
4. Ensure Emission is enabled on materials

---

## 🧪 Testing Checklist

| Feature | Status |
|--------|--------|
| 🎮 Controller input triggers sonar | ✅  
| 🌀 SonarWave visual spawns | ✅  
| 👁️ Interactables glow on sonar hit | ✅  
| 👹 Monster reacts to sonar + light | ✅  
| 💨 Breathing audio plays when monster moves | ✅  
| 💡 Bloom glow visible in dark rooms | ✅  
| 🎧 Sound sources 3D spatialized | ✅  
| 🔁 Cooldown prevents sonar spam | ✅  

---

## 🔧 Future Enhancements (Optional)

- Haptic feedback on sonar
- Dynamic sonar cooldown linked to tension level
- Monster reaction variety (ambush vs hunt)
- Puzzle-triggered sonar flashes
- More monster types and room variations

---

## 🧩 Credits

**Team Glaze:**
- David, Kelvin, Kenny, Ryan, Tejindra (TJ)


## 📬 Contact

For bugs, feedback, or contributions:
- 🔗 GitHub: https://github.com/MrHappySunface/SilentEscape
- 📧 tej.kcp@gmail.com, 7BDJ04@gmail.com, KennyP1003@gmail.com, nonamesremain71@gmail.com, Ryan_Cayton@student.uml.edu
- 🎮 Discord: `https://discord.com/channels/1326956702838427740/1334645417367044177


**University of Massachusetts Lowell**  
COMP 3500 – Special Topics in Game Design  
Spring 2025 Final Project

> “Silence is no longer safe.”