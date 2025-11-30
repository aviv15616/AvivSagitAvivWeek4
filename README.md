# Bowling Challenge 🎳  
A physics-based bowling mini-game created for a Game Development course.

---

## 🎮 Overview

Bowling Challenge is a compact bowling game built in Unity.  
It includes realistic physics for both the ball and the pins, multi-attempt gameplay per level, and UI feedback for score, attempts, and level progression.

The project demonstrates:
- RigidBody physics  
- Mouse-based aiming and power charging  
- Level logic (two throws per level)  
- Object management (ball, pins, lane)  
- Modular UI system  
- Smart reset between throws  

---

## 🕹️ How to Play

1. Aim the ball by moving the mouse.  
2. Hold **SPACE** to charge the throw power.  
3. Release **SPACE** to roll the ball.  
4. You have *two attempts* to knock down all the pins.  
5. If all pins are cleared → the game loads the next level.  
6. Clear all levels → **YOU WIN!**

---

## 🧩 Project Structure

### **GameManager**
- Controls the main flow of the game  
- Spawns the ball at the start of each level  
- Listens for the “ball launched” event  
- Detects when the ball stops  
- Decides whether to retry, reset, or advance to the next level  

### **LevelManager**
- Loads level data  
- Tracks number of throws  
- Knows when the level is complete or failed  

### **BottleManager (Pins)**
- Finds pins in the scene  
- Detects fallen pins based on angle and position  
- Removes fallen pins  
- Resets standing pins between throws to avoid late collisions  

### **BowlingLane**
- Spawns the ball at a defined location  
- Resets the ball between attempts  

### **UIManager**
- Displays the number of fallen pins  
- Shows the current level  
- Displays messages and instructions  
- Shows the final “YOU WON!” message  

---

## 🚀 Running the Game

1. Open Unity (2021 or later recommended).  
2. Load the project folder.  
3. Open the scene **MainScene**.  
4. Press **Play**.

---

## 📁 Important Files

- `GameManager.cs`  
- `LevelManager.cs`  
- `BottleManager.cs`  
- `BowlingLane.cs`  
- `BallController.cs`  
- `UIManager.cs`

---

## 🎯 Assignment Requirements

This project includes:
- Power-based ball launch system  
- Two attempts per level  
- Correct level logic  
- Full pin physics  
- UI synchronization  
- Automatic cleanup and reset of pins between attempts  
- Final win screen  

---
## ITCH.IO

https://gamedevteamx.itch.io/avivsagitavivweek4

