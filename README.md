### Description

Hexapod simulator is a piece of software written to simulate the movement of a 6 degree of freedom Stewart platform balancing a ball. The software handles inverse kinematic calculations to determine positions of 6 actuators based on the translation and tilt of the hexapod in real time.

### License

![GitHub](https://img.shields.io/github/license/Grahmification/hexapod-simulator) Hexapod Simulator is available for free under the MIT license.

### Projects

1. [Hexapod Simulator](Source/Hexapod%20Simulator) - A GUI for simulating the hexapod written using winforms and OpenTK.
1. [Hexapod Simulator.Helix](Source/Hexapod%20Simulator.Helix) - An improved GUI written using WPF and HelixToolKit. 
1. [Hexapod Simulator.Shared](Source/Hexapod%20Simulator.Shared) - Shared lower level components handling all of the math which are used by each GUI.

<p align="center">
  <img src="./Docs/Hexapod Simulator Helix GUI Overview.png" alt="Hexpod Simulator.Helix GUI" height="200">
  <img src="./Docs/Hexapod Simulator OpenTK GUI Overview.png" alt="Hexpod Simulator GUI" height="200">
</p>

<p align="center">
  <img src="./Docs/Hexapod Simulator Helix Demo.gif" alt="Hexpod Simulator.Helix Demo" width="750">
</p>

### Dependencies

- Hexapod Simulator utilizes the OpenTK Library under the MIT License. Copyright (c) 2006-2019 Stefanos Apostolopoulos for the Open Toolkit project. https://github.com/opentk/opentk

- Hexapod Simulator.Helix utilizes the HelixToolKit Library under the MIT License. Copyright (c) 2019 Helix Toolkit contributors. https://github.com/helix-toolkit/helix-toolkit

- Hexapod Simulator.Helix utilizes the Fody Library under the MIT License. Copyright (c) Simon Cropp. https://github.com/Fody/Fody

- Hexapod Simulator.Helix utilizes the Fody.PropertyChanged Library under the MIT License. Copyright (c) 2012 Simon Cropp and contributors. https://github.com/Fody/PropertyChanged

- All projects utilize the MathNet.Numerics Library under the MIT License. Copyright (c) 2002-2021 Math.NET. https://github.com/mathnet/mathnet-numerics

- All projects utilize the GFunctions Library under the MIT License. Copyright (c) 2019 Graham Kerr. https://github.com/Grahmification/GFunctions

### Getting Started

1. Compile the code in Visual Studio.
1. Run the desired executable file (Hexapod Simulator.exe or Hexapod Simulator.Helix.exe).
1. The 3D view of the hexapod can be moved/rotated/zoomed by dragging with the mouse buttons and scrolling.
1. The geometry of the hexapod can be setup using the configuration tab at the top left.
1. The position of the hexapod can be moved around with the manual control sliders. If the simulation is not running, the position will be updated instantly. 
1. Click "Start Simulation" to start the realtime simulation. The ball's position will now respond to tilting the hexapod.
1. Checking the "Servo Active" button will activate automatic hexapod servoing. The hexapod will automatically tilt to keep the ball centered on itself. Adjust the XY positition and watch the hexapod re-center the ball.
1. Actuator positions are calculated after each hexapod movement. Actuators that cannot obtain a valid solution within their travel range get highlighted in red.
    * Currently actuators are defined by having a vertical linear travel range with pivoting connecting link. Code is also in place to simulate actuators using an arm extending from a rotary motor, but it is currently not possible to change from the GUI.

### Screenshots

  Hexapod Simulator HelixToolKit GUI

<p align="center">
  <img src="./Docs/Hexapod Simulator Helix GUI Overview.png" alt="Hexpod Simulator.Helix GUI" width="750">
</p>

  Hexapod Simulator OpenTK GUI

<p align="center">
  <img src="./Docs/Hexapod Simulator OpenTK GUI Overview.png" alt="Hexpod Simulator GUI" width="750">
</p>

### Implementation
* Ball physics are calculated by a very simplistic model which integrates the platform's XY normal force components based on tilt to get linear XY velocity and acceleration. This assumes no friction, rolling, inertia, etc. The true model of the ball including friction and inertia is extremely complicated and would take a lot of work to implement. 
* Z position of the ball is calculated to make the ball tangent to the hexapod platform at it's given XY position. 
* During the simulation, hexapod translation and rotation is linked to the manual control sliders by 6 seperate PID controllers with a large integral term to create smooth movement. A better future implementation would be to control each DOF with a trapezoidal trajectory that respectly velocity/acceleration limits. 
* There are two PID controllers running to keep the ball centered on the hexapod. One PID controller eliminates error in the X direction by controlling pitch rotation of the platform. The second PID controller eliminates error in the Y direction by controlling roll rotation of the platform.
* Actuator positions are calculated iteratively to try and maintain a distance equal to the connecting link between the tip of the actuator and the corresponding node on the platform. This is somewhat resource intensive but works very well.

### Changes

See the [changelog](CHANGELOG.md) for changes.
