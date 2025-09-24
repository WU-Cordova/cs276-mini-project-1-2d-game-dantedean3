# Sprite Flight 🚀


## Game Overview
Sprite Flight is a simple survival game where you control a rocket trying to avoid bouncing obstacles. The goal is to survive as long as possible while racking up points. Obstacles spawn with random sizes, directions, and speeds, creating a chaotic field that forces you to react quickly.

### Controls
- **Left Mouse Button (hold)**: Apply thrust toward the mouse cursor.  
- **Aim with Mouse**: The rocket always rotates to face your mouse position.  
- **Restart Button**: Appears after you die, lets you quickly start a new run.  

### How to Play
- Stay alive as long as possible.  
- Your score increases based on survival time.  
- Colliding with an obstacle ends the game in an explosion.  
- Press Restart to try again and beat your high score.  

---

## Base Game Implementation

### Completion Status
- [x] Player movement and thrust system  
- [x] Mouse aiming and rotation  
- [x] Obstacle spawning with randomized speed/size/direction  
- [x] Score tracking and display  
- [x] Explosion effect on game over  
- [x] Restart button  
- [x] UI Toolkit integration for score and buttons  
- [ ] No major missing features from the tutorial  

### Known Bugs
- Occasionally, an obstacle spawns partially outside the border.  
- Explosion effect sometimes looks clipped if the player is near the screen edge.  

### Limitations
- The game ends instantly on first collision — no “lives” system yet.  
- Obstacles keep bouncing forever until game over (though borders are removed on death).  

---

## Extensions Implemented

### 1. Destroy the Borders on Game Over (4 points)
**Implementation**: Added a reference to the Borders GameObject and disabled it in `OnCollisionEnter2D`.  
**Game Impact**: Instead of obstacles endlessly bouncing after the player explodes, they now fly off-screen. It makes the game feel cleaner.  
**Technical Details**: `borderParent.SetActive(false);` is called right before the restart button appears.  
**Known Issues**: Must remember to assign the Borders parent object in the Inspector.  

---

### 2. Ambient Background Particles (4 points)
**Implementation**: Created a looping particle system that simulates stars/dust moving in the background.  
**Game Impact**: Adds visual depth and atmosphere, making the game feel more alive.  
**Technical Details**: Configured emission area and lifetime to keep subtle star movement visible throughout gameplay.  
**Known Issues**: Very high emission rates can slow performance.  

---

### 3. Increase Difficulty Over Time (5 points)
**Implementation**: Obstacles use a slightly bouncy physics material (>1.0), so each bounce speeds them up. Their velocity is also clamped to avoid infinite acceleration.  
**Game Impact**: The longer you survive, the more chaotic the game becomes. It pushes players to react faster and raises the tension.  
**Technical Details**: Implemented in `Obstacle.cs` by checking velocity and scaling particle bounce effects.  
**Known Issues**: Rarely, two fast obstacles may “clip” through each other at extreme speeds.  

---

### 4. Animated Booster Flame with Sound (6 points)
**Implementation**: Added a BoosterFlame GameObject as a child of the player. It toggles on/off based on mouse button input, and loops a thruster sound.  
**Game Impact**: Provides satisfying feedback when thrusting — you see and hear the rocket fire. Makes controls feel much more responsive.  
**Technical Details**: Uses `boosterFlame.SetActive(true/false)` inside `Update()`. Flame is tied to player input events.  
**Known Issues**: Booster sound continues briefly if you die while holding thrust.  

---

## Credits
- Explosion and thruster sound effects from free Unity Asset Store packs.  
- Ambient music from freesound.org (credited under CC license).  
- Particle textures and flame sprite from Unity built-in resources.  

---

## Reflection
**Total Points Claimed**: Base (80%) + Extensions (19%) = **99%**  

**Challenges**:  
- Getting the borders to disable correctly without null reference errors.  
- Keeping particle effects lightweight enough not to tank performance.  
- Debugging why the rocket was moving so slowly until I adjusted thrust values.  

**Learning Outcomes**:  
- How to use `Time.deltaTime` for frame-independent scoring.  
- How to expose variables in the Inspector to make tweaking gameplay easier.  
- The basics of Unity’s particle system and how physics materials affect gameplay.  
- A better understanding of organizing scripts (splitting Update into helper methods).  

---

## Development Notes
- All work was committed through GitHub with regular commits.  
- A proper Unity `.gitignore` file is included to avoid pushing large temp/build files.  
- Game was built and tested in WebGL before publishing to Unity Play.  
