# Bowling2

A simple bowling game made with Unity.

## How to Run

1. Open the project in Unity (2022.3 or later recommended).
2. Open the `MainScene` scene from `Assets/Scenes/`.
3. Press Play in the editor.
4. Press the assigned input action (default: spacebar or left mouse button) to roll the ball.
5. Watch the bottles fall!

## Class Relations
```
GameManager
├─ LevelManager (loads levels, manages bottles)
├─ UIManager (displays score and messages)
└─ BowlingLane (spawns/reuses ball)
└─ BallController (detects when ball stops)

LevelManager
└─ BottleManager (tracks bottles)
└─ Bottle[] (individual bottles, checks if fallen)
```
## UML

```mermaid
# Bowling2

A simple bowling game made with Unity.

## How to Run

1. Open the project in Unity (2022.3 or later recommended).
2. Open the `MainScene` scene from `Assets/Scenes/`.
3. Press Play in the editor.
4. Press the assigned input action (default: spacebar or left mouse button) to roll the ball.
5. Watch the bottles fall!

## Class Relations
```
GameManager
├─ LevelManager (loads levels, manages bottles)
├─ UIManager (displays score and messages)
└─ BowlingLane (spawns/reuses ball)
└─ BallController (detects when ball stops)

LevelManager
└─ BottleManager (tracks bottles)
└─ Bottle[] (individual bottles, checks if fallen)
```
## UML



**Key Flow:**
1. GameManager starts a level via LevelManager
2. BowlingLane spawns the ball
3. Player rolls via BallController
4. Ball stops → BallController fires OnBallStopped event
5. GameManager calls BottleManager.CountFallen()
6. UIManager displays the result

## Architecture Assumptions

- **Ball reuse:** The ball is reused between rolls (repositioned) instead of destroyed/respawned to avoid hiccups.
- **Single BottleManager per level:** Each level layout has one BottleManager that finds all Bottle children via `GetComponentsInChildren()`.
- **Stop detection:** Ball is considered stopped when it moves very little (`stationaryTime >= 0.25s` and `velocity < 0.15`) for at least 1 second after rolling.
- **Fallen criteria:** A bottle is fallen if its Y position drops below 1.35 OR it tips over (angle > 45°).
- **UI updates on stop:** Score only updates when the ball physically stops, not before.

## Notes

- Ensure all bottles are **children** of the BottleManager GameObject in the hierarchy.
- Each bottle must have the `Bottle` component attached.
- Adjust `velocityThreshold` and `minStationaryTime` in BallController if the ball stops too early/late.

```


**Key Flow:**
1. GameManager starts a level via LevelManager
2. BowlingLane spawns the ball
3. Player rolls via BallController
4. Ball stops → BallController fires OnBallStopped event
5. GameManager calls BottleManager.CountFallen()
6. UIManager displays the result

## Architecture Assumptions

- **Ball reuse:** The ball is reused between rolls (repositioned) instead of destroyed/respawned to avoid hiccups.
- **Single BottleManager per level:** Each level layout has one BottleManager that finds all Bottle children via `GetComponentsInChildren()`.
- **Stop detection:** Ball is considered stopped when it moves very little (`stationaryTime >= 0.25s` and `velocity < 0.15`) for at least 1 second after rolling.
- **Fallen criteria:** A bottle is fallen if its Y position drops below 1.35 OR it tips over (angle > 45°).
- **UI updates on stop:** Score only updates when the ball physically stops, not before.

## Notes

- Ensure all bottles are **children** of the BottleManager GameObject in the hierarchy.
- Each bottle must have the `Bottle` component attached.
- Adjust `velocityThreshold` and `minStationaryTime` in BallController if the ball stops too early/late.