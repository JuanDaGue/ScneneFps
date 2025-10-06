# Weapon Pickup Test Scene

## Overview
This Unity test scene showcases a simple gameplay mechanic: the player explores, finds a weapon, and picks it up. The environment includes visual and particle effects for immersion and feedback.
![alt text](Weapon1.PNG)
## Features
- **Shaders:** Lava (emissive/animated) and Skybox (gradient or cubemap).  
- **Particles:** Smoke, Sparks, and Grab Burst on pickup.  
- **Weapon Status Rings:** Rotating rings showing weapon availability.  
- **Pickup Effects:** Sound, particles, and light burst when grabbing.  

## Requirements
- Unity 2021.3+ (URP recommended)  
- Basic Input System or legacy Input  
- Optional: Post-processing for bloom  

## Scene Layout
- Lava area using animated shader  
- Skybox matching lava tone  
- Weapon on pedestal with rotating rings  
- Smoke near lava, sparks around weapon  

## Controls
- Move: **WASD**  
- Look: **Mouse**  
- Pickup: **E**  

## How It Works
1. Approach the weapon pedestal.  
2. When close, press **E** to pick up.  
3. Rings fade out, particles trigger, and weapon attaches to player.  

## Folder Structure
