# Jet Engine Blade Swap Training

An interactive training simulation created in Unity 6 using **DoTween** to demonstrate how to perform a jet engine blade swap. The project includes animated step-by-step instructions, guided UI, and user interaction with the model.

## 🎮 Features

- Full training simulation with 8 steps
- Step-based DoTween animation sequences
- Interactive 3D jet engine model with rotation support (keys **Q** and **E**)
- In-game **Guide Menu** with step overview and navigation
- Animations repeatable from the current viewpoint
- End-of-training message with return to main menu
- Localized UI elements (optional)

## 🧩 Technologies

- Unity 6
- DoTween (for smooth animation sequencing)
- TextMeshPro (UI)
- Unity Input System

## 🗺️ Steps

1. Welcome

2. Training Description

3. Step 1: Validate the engine is off by checking the control unit.

4. Step 2: Disconnect the gas tank.

5. Step 3: Remove the blade cover.

6. Step 4: Remove the old blade.

7. Step 5: Insert the new blade.

8. Step 6: Mount the blade cover.

9. Step 7: Connect the gas tank.

10. Step 8: Start the engine and test that it is working.

11. Training Feedback

12. End

## 🕹️ Controls

| Action             | Key        |
|--------------------|------------|
| Rotate Left        | Q          |
| Rotate Right       | E          |
| Interact Buttons   | Mouse Click |


## 🚀 How It Works

1. **Main Menu**: Choose to start or exit the training.
2. **Training Start**: The first step plays automatically using DoTween animations.
3. **Step Interaction**:
   - View animation.
   - Rotate the model with Q and E.
   - Repeat animation from current angle.
   - Proceed to the next step with Continue.
4. **Guide Menu**: Lists all steps, lets users jump to a specific step or return to the main menu.
5. **Completion**: After the last step, a thank you message is displayed and the simulation resets.

## 📦 Setup Instructions

1. Clone this repository.
2. Open the project in Unity 6.
3. Ensure DoTween is imported via the Unity Package Manager or [Demigiant's website](http://dotween.demigiant.com/).
4. Press **Play** to start the training from the menu.

## 📄 License

This project is for educational and training purposes.

---