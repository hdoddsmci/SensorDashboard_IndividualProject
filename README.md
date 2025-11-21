# Sensor Dashboard Individual Project

## Part 1: Learning Evidence
The evidence for the LinkedIn Learning courses (Git and xUnit) can be found in my separate learning repository:
**[Click here to view Part 1 Evidence (Notes & Exercises)](https://github.com/hdoddsmci/D202_Learning_Evidence.git)**

This repository contains:
* Detailed notes for all chapters.
* `xUnit_Labs` project with TDD examples (Calculator).
* Evidence of branching, merging, and conflict resolution.

---

## Part 2: Virtual Temperature Sensor App

### Project Overview
This project is a **.NET Console Application** that simulates a virtual temperature sensor for a Data Centre environment. It is designed to generate realistic temperature readings, validate data integrity, detect anomalies (spikes), and simulate hardware faults (cooling failures).

The system uses **Object-Oriented Programming (OOP)** principles and follows **Test-Driven Development (TDD)** using xUnit.

### Features
#### Core Features
* **Data Simulation:** Generates random temperature readings within a configurable range (e.g., 21°C - 25°C).
* **Data Validation:** Ensures readings are within physical sensor limits.
* **History Storage:** Maintains a memory of recent readings for analysis.
* **Data Smoothing:** Uses a **Moving Average** algorithm (last 3 readings) to reduce noise.
* **Anomaly Detection:** Flags sudden temperature spikes (> 5°C change) as anomalies.
* **Console Logging:** Real-time status updates with color-coded warnings (Green=OK, Yellow=Warning, Red=Critical).

#### Advanced Features
* **Fault Injection:** Simulates a "Cooling Unit Failure," causing temperatures to rise exponentially to >45°C.
* **Threshold Alerts:** Triggers a **CRITICAL** alert if the temperature exceeds the safe maximum.
* **CI/CD Pipeline:** GitHub Actions workflow automatically runs unit tests on every push.

### Setup & Usage
#### Prerequisites
* Visual Studio 2022
* .NET 8.0 SDK

#### How to Run
1.  Clone the repository.
2.  Open `Sensors.sln` or the folder in Visual Studio.
3.  Set `VirtualSensor` as the Startup Project.
4.  Press **F5** or click **Run**.

#### How to Use
* The application runs automatically in a loop.
* **Watch the Console:**
    * **Normal:** Green text, stable temperatures.
    * **Fault:** After approx. 10 seconds, a "Hardware Failure" is injected.
    * **Critical:** Red text indicates the sensor has overheated.
* **Stop:** Press `Ctrl + C` to exit.

### Testing
This project uses **xUnit** for comprehensive unit testing.
* **Test Project:** `SensorTests`
* **Coverage:** Includes tests for Validation, Simulation, Smoothing, History, and Fault Injection.

#### Running Tests
1.  Open Visual Studio.
2.  Go to **Test > Run All Tests**.
3.  Verify that all 11 tests pass (Green checkmarks).
<img width="623" height="727" alt="Screenshot 2025-11-22 040350" src="https://github.com/user-attachments/assets/f572da2e-f7ac-4db8-bd1e-9dfdc04040b5" />
