# Coin-Runner

A minigame where you collect coins and dodge obstacles in an infinite 3d runner.


## Project Status

**PROTOTYPE COMPLETE**

Prototype complete game that meets client requirements.


## Client Requirements

The project is commissioned with the following requirements:

### Gameplay:
 - A/D player controller.
 - 3 lanes.
 - Procedurally generated obstacle placement
 - Procedurally generated coins
 - Game Over on collision with obstacle
 - Infinite runner
 - Difficulty with length
 - Score based on distance and coins collected
 - Restart menu upon Game Over.

### Artistic:
 - At least 2 different obstacle models
 - Basic character model with a running and death animation
 - Materials coloring the entire scene
 - Low-Poly artistic approach


# Technical Design

## Systems

### GameManager

**Description:**

* Acts as the central hub for major game state and game-wide functions.
* Handles player input using Unity's Old Input System.
* Owns shared configuration values used by multiple systems.
* Owns the current game state.

**Variables:**

* `(float) laneSpacing`
* `(float) playerDistance`
* `(int) coinsCollected`
* `(bool) gameOver`

**Primary Methods:**

* `InitializeGame()`
* `GameOver()`
* `HandleInput()`

**State Ownership:**

* `laneSpacing`
* `playerDistance`
* `coinsCollected`
* Game-over state

---

### PlayerHandler

**Description:**

* Controls player movement and lane changes.
* Continuously moves the player forward while the game is active.
* Detects collisions/triggers with coins and obstacles.
* Reports gameplay events to GameManager.
* Uses GameManager's `laneSpacing` to determine lane positions.

**Variables:**

* `(float) laneSpacing` — provided by GameManager
* `(float) playerSpeed`
* `(int) currentLane`
* `(bool) alive`

**Primary Methods:**

* `MoveLeft()`
* `MoveRight()`
* `CoinTrigger()`
* `ObstacleTrigger()`

**State Ownership:**

* Player movement
* Current lane
* Alive/dead state

---

### MapHandler

**Description:**

* Generates and manages the playable map.
* Determines difficulty based on player distance.
* Generates obstacles according to the current difficulty.
* Generates coins across the three available lanes.
* Uses GameManager's `laneSpacing` to determine lane positions.
* Map generation begins from world position `(0, 0, 0)`.

**Variables:**

* `(float) difficulty`
* `(float) laneSpacing` — provided by GameManager

**Primary Methods:**

* `CalculateDifficulty()`
* `GenerateObstacle()`
* `GenerateCoin()`

**State Ownership:**

* Map generation state
* Current map difficulty

---

### ScoreHandler

**Description:**

* Calculates the player's score from game state provided by GameManager.
* Updates the scoreboard UI.
* Does not directly modify GameManager's gameplay state.

**Variables:**

* `(float) playerDistance` — provided by GameManager
* `(int) coinsCollected` — provided by GameManager
* `(float) score`

**Primary Methods:**

* `CalculateScore()`
* `UpdateUI()`

**State Ownership:**

* Calculated score
* Scoreboard presentation

---

### CameraHandler

**Description:**

* Follows the player's forward movement.
* Updates its Z position based on the player's Z position.
* Maintains a fixed X and Y position relative to the initial camera position.

**Primary Methods:**

* `UpdatePosition()`

**State Ownership:**

* Camera position

---

## Interfaces

Interfaces describe the information or commands communicated between systems. Each interface identifies the direction of communication, what is communicated, and the purpose of the communication.

### GameManager → PlayerHandler

**Data:**

* `laneSpacing` — provides the shared distance between lanes.

**Commands:**

* `InitializeGame()` — establishes the player's initial game state and position.
* `MoveLeft()` — requests that the player move one lane left.
* `MoveRight()` — requests that the player move one lane right.

**Purpose:**

* Provides PlayerHandler with shared configuration and player movement commands.

---

### PlayerHandler → GameManager

**Events:**

* `CoinCollected()` — reports that the player has collected a coin.
* `ObstacleHit()` — reports that the player has collided with an obstacle.
* `DistanceUpdated()` — reports the player's current forward distance.

**Purpose:**

* Reports gameplay events and player state changes to GameManager.
* GameManager remains responsible for modifying game-wide state in response to these events.

---

### GameManager → MapHandler

**Data:**

* `laneSpacing` — provides the shared distance between lanes.
* `playerDistance` — provides the player's current distance.

**Purpose:**

* Allows MapHandler to align generated objects with the player and determine the current difficulty.

---

### GameManager → ScoreHandler

**Data:**

* `playerDistance` — provides the current distance traveled.
* `coinsCollected` — provides the current number of coins collected.

**Purpose:**

* Provides ScoreHandler with the game state required to calculate and display the player's score.

---

### PlayerHandler → CameraHandler

**Data:**

* `player.position.z` — provides the player's current forward position.

**Purpose:**

* Allows CameraHandler to follow the player's forward movement without coupling camera behavior to GameManager.

---

## Initial Architecture

The intended high-level communication flow is:

`GameManager → PlayerHandler`

`PlayerHandler → GameManager`

`GameManager → MapHandler`

`GameManager → ScoreHandler`

`PlayerHandler → CameraHandler`

GameManager owns game-wide state, while each supporting system owns its respective domain. Systems should report events to GameManager rather than directly modifying GameManager's state.

