# Color Catch

A small 2D arcade game made with Unity. Balls fall from the top of the screen. Switch your color to match them and catch them before they reach the bottom.

![Gameplay](Docs/gameplay.gif)

## How to Play

| Key | Action |
|---|---|
| `A` / `←` | Move left |
| `D` / `→` | Move right |
| `Space` | Switch color (red → blue → green) |

- Catch a ball **of your own color** to earn **+10 points**.
- Each ball that reaches the bottom costs **1 life**. You start with 3.
- Your highest score is saved between sessions.

## Built With

- Unity **6000.3.22f1** (URP 2D)
- Input System
- TextMeshPro

## Technical Highlights

- **Object pooling:** balls are reused with `ObjectPool<T>`, no `Instantiate`/`Destroy` during gameplay
- **Event-driven:** `Ball` and `Player` raise events, `GameManager` owns the rules, UI only listens
- **ScriptableObject** for UI sprites
- **Inspector-tunable** values (spawn rate, limits, lives, points)
- **PlayerPrefs** for saving the best score

## Project Structure

```
Assets/Scripts/
  Core/      GameManager, LevelManager, ScoreProgress
  Gameplay/  Player, Ball, BallSpawner, ColorMap
  Pooling/   BallPool, Pool (object pooling)
  UI/        Hud, LoseScreen
  Data/      SpritesCatalog (ScriptableObject)
```

## Running the Project

1. Clone the repository.
2. Open the folder with Unity Hub, using Unity 6000.3.22f1.
3. Open `Assets/Scenes/GameScene.unity` and press **Play**.

## AI Usage

**No AI tools** were used for implementing any part of this project. The project was developed entirely independently to improve my C# programming skills, Unity proficiency, and problem-solving abilities without relying on AI tools.

**Note:** AI was used to generate some UI sprites. All game logic, architecture, and implementation were completed without AI tools.

## Next Steps

- [ ] **Difficulty curve:** balls fall faster and spawn more often over time.
- [ ] **Wrong color penalty:** touching a ball of the wrong color costs a life or points.
- [ ] **Feedback:** sounds for catching and missing, plus a small particle effect.
- [ ] **Start screen:** a "Start" button so the game doesn't begin immediately.
- [ ] **Pause:** press `Esc` to pause.
