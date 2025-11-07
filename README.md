# Unity Leaderboard Popup (Test Task)

**Unity:** 2022.3.62f2 LTS
**UI:** uGUI + Addressables + TextMeshPro

---

## Overview
Simple popup system with an async leaderboard:
- Loads data from `Resources/Leaderboard.json`
- Sorts players by score
- Loads avatars async with caching
- Applies color & size based on player type
- Popup opened via `PopupServices.Manager.OpenPopup("LeaderboardPopup")`

---

## Structure
```Assets/
├── Scripts/
│ └── SimplePopupManager/
│ ├── Common/
│ ├── Leaderboard/
│ │ ├── Controller/
│ │ ├── Model/
│ │ └── View/
│ ├── PopupInitialization/
│ └── Services/
├── Resources/Leaderboard.json
```
---

## How to Run
1. Open project in **Unity 2022.3.62f2 LTS**
2. Open scene **Leaderboard**
3. Press **Open Leaderboard** button → popup loads asynchronously

---

## Notes
- Popup scales for all screen sizes
- Uses `PopupNames.Leaderboard` for key
- Avatars cached in-memory
- Clean async, no editor-only code
