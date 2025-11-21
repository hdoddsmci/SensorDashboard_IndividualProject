# Final Reflection Report

## Development Process
My approach to this project was to strictly follow **Test-Driven Development (TDD)** and **Object-Oriented Programming (OOP)** principles. I began by breaking down the requirements into core components: the Sensor logic, Data Simulation, and Validation.

I started by creating the `VirtualSensor` class and immediately wrote the corresponding xUnit tests in `SensorTests`. For example, before implementing the logic for `InitialiseSensor`, I wrote a test to ensure it correctly assigned properties and threw exceptions for invalid inputs (e.g., if `Min > Max`). This TDD approach ensured that every piece of logic was verified before moving to the next, significantly reducing debugging time later.

Once the core logic was solid, I implemented the **Data Simulation** using a random number generator. I then added the **Data Smoothing** feature using a moving average of the last three readings to make the data more realistic. Finally, I integrated the **Advanced Features** (Fault Injection and Threshold Alerts) to satisfy the higher grading criteria, ensuring the system could simulate "hardware failures" and flag critical temperature spikes.

## Git Usage & Workflow
I utilized a professional Git workflow throughout the project. I initialized the repository early and created a separate `develop` branch to isolate ongoing work from the stable `main` branch.
* **Branching:** I worked primarily on `develop` and used feature branches (e.g., `feature-config`) for implementing specific requirements like the JSON configuration and Fault Injection logic.
* **Commits:** I ensured commit messages were descriptive and followed a standard convention (e.g., `feat: Implemented shutdown logic`, `test: Added validation unit tests`).
* **Merging:** Once features were tested and verified, I merged `develop` into `main` for the final release.
* **Tags:** I used Git Tags (e.g., `v1.0-core-complete`) to mark significant milestones in the project history.

## Testing Practices
Testing was central to my development. I used the **xUnit framework** to create a comprehensive test suite.
* **Coverage:** I achieved high code coverage by writing tests for every method, including `ValidateData`, `StoreData`, and `DetectAnomaly`.
* **Edge Cases:** I specifically tested edge cases, such as initializing a sensor with empty names or invalid ranges, to ensure robustness.
* **Automation:** I went a step further by implementing a **CI/CD pipeline using GitHub Actions**. This automatically runs my entire test suite every time code is pushed to the repository, ensuring that new changes never break existing functionality.

## Challenges Faced
One challenge I faced was implementing the **Data Smoothing (Moving Average)** logic. Initially, I had issues handling cases where the history list had fewer than three items. I resolved this by adding logic to return the latest single value until enough history was built up. Another challenge was configuring the **JSON file loading**; I had to ensure the file was correctly copied to the output directory so the application could find it at runtime.

## Conclusion
This project successfully simulates a robust Data Centre sensor. By combining TDD, automated CI testing, and a structured Git workflow, I delivered a highly reliable application that meets all core and advanced requirements, including dynamic configuration and fault simulation.
