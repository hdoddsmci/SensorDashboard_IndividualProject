**Learning Git and GitHub**
**Chapter 1 – Version Control Overview**

This first chapter explained what version control is and why developers rely on it.
I learned that Git lets me track every change to my files, compare old versions, and go back if something breaks.
It also introduced the difference between centralized and distributed systems. Git being distributed means every copy of the project contains the full history, which adds flexibility and safety.

How I will apply this:
Going forward, I will treat Git as the foundation of my workflow.
Every feature or change in my project will be committed with clear, descriptive messages so I can see the full story of how my app developed.
If I make a mistake, I’ll be able to roll back easily.
It will also help demonstrate professional project management in my assessment.

**Chapter 2 – Getting Started with Git**

This chapter walked through setting up Git on a computer, creating a repository, and configuring the username and email.
It also covered adding files, committing, and pushing to GitHub.

How I will apply this:
I’ll make sure all future project changes start locally in a new branch and are pushed regularly to GitHub.
I’ll keep my repository organised and avoid adding unnecessary files by maintaining an updated .gitignore.
I’ll also use meaningful commit messages that describe what changed and why.

**Chapter 3 – Working with Git and GitHub**

This part focused on using GitHub for collaboration.
I learned about branching, merging, pull requests, and how to work on features without breaking the main version.
It also showed how to use GitHub Issues and Kanban boards to plan and track work.

How I will apply this:
I’ll create separate branches for new parts of my SensorDashboard project, such as adding new sensor features or user interface changes.
Once a branch is complete, I’ll open a pull request and review the differences before merging it into the main branch.
I’ll also use the Kanban board on GitHub to manage tasks clearly, showing what I plan to do, what I’m currently working on, and what’s finished.

**Chapter 4 – GitHub Social and Publishing Features**

This chapter explored how GitHub can also be used to share and publish work.
It covered features like wikis, gists, discussions, and GitHub Pages.
It also explained how documentation and Markdown files make a project easier for others to understand.

How I will apply this:
I’ll continue documenting my work using Markdown for clarity.
I’ll make sure each file in my project has a clear purpose, and I’ll update my README so anyone looking at the repository can see what the project is about, what tools were used, and how to run it.
Later, I might even experiment with GitHub Pages to publish my documentation in a cleaner format.

**C# Unit Testing with xUnit
Understanding Unit Testing and TDD**

This course explained how unit testing helps confirm that code behaves the way it should.
I learned about the Test-Driven Development (TDD) cycle: writing a failing test first, making it pass, and then refactoring the code.
That pattern forces me to think carefully about design before writing any real code.

How I will apply this:
I’ll follow the red-green-refactor cycle as I build out features in my project.
Before adding a new method or logic, I’ll write a small test describing the expected behaviour.
Once the test passes, I’ll tidy the code and repeat the process for the next feature.
This should keep my code clean and reliable.

**Writing Tests and Assertions**

I learned how to use [Fact] for simple tests and [Theory] with [InlineData] for running the same test with different inputs.
Assertions like Assert.Equal, Assert.True, and Assert.Throws are used to check the results of the code being tested.

How I will apply this:
I’ll create tests that cover both normal and edge cases for the SensorDashboard app.
For example, I’ll test temperature calculations and data validation to make sure readings outside expected ranges are handled correctly.
I’ll also use [Theory] to test multiple values without writing repeated code.

**Building and Refactoring through Testing**

The course built a small calculator to show how tests can guide development.
It was useful to see how writing tests first helps you write only the code you actually need and makes refactoring less risky.

How I will apply this:
When I expand my project with new sensor features, I’ll plan out the tests first.
This will help me identify the expected inputs, outputs, and potential errors.
If I later change something, I can rerun the tests to confirm nothing else broke.

**Managing Test Data and Cleanup**

The instructor demonstrated using the IDisposable interface to reset data between tests so that one test doesn’t affect another.
They also covered the Arrange-Act-Assert structure to make tests clear and consistent.

How I will apply this:
I’ll use Arrange-Act-Assert in all of my tests so they’re easy to follow.
If my tests ever use shared data, I’ll use cleanup methods to reset the state before the next test.
This will help me keep results consistent and professional.

**Reflection on the xUnit Course**

What stood out most was how testing changes the way I think about code.
Rather than writing everything and hoping it works, I’ll design my code around testable behaviours.
I found it interesting how xUnit makes testing feel like part of development rather than an afterthought.
It also showed me that automated testing is not just for big projects even small ones benefit from it.

**Summary**
From these two courses, I learned the value of having structure in both my coding and my workflow.
Git and GitHub will help me keep my project organised and demonstrate version control best practices.
xUnit and the TDD approach will help me produce reliable code that can be tested and improved over time.
Together, these lessons give me a stronger foundation for continuing my individual project and for future software development work.
