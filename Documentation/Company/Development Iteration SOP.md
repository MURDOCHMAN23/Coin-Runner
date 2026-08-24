## Project Scenes

 - **Main**  
  A functional game build / scene layout.

 - **Test**  
  A test scene containing all functions working in cohesion.

 - **Developer**  
  A temporary scene created by developers to test their work between Test Branch Syncs. Only that developer is allowed to change that scene. 

## Branch Layout

 - **main**  
   Most recent build with an intact Main scene.

 - **test/main**  
   Most recent test iteration that was synced. Contains a test scene containing all features of the previous development iteration.

 - **developer/[developer name]**  
  The dedicated branch for each developer assigned to the project to use to complete their work. Used for each developer to commit their work and create a PR during each development iteration.

## Development Cycle

 - **Design**  
  Assign developers to project -> Design Meeting to ensure understanding of project scope and requirements -> Outline Milestone 1, along with potentially other milestones -> Technical Design Meeting to identify systems and interfaces -> Begin development.

 - **Developing / Iterations**  
   Update all developer branches to the current test/main branch -> Review Issues connected to current milestone in progress -> Assign sole developer to be allowed to change the Test Scene -> Assign developers to tasks/Issues -> Developers complete tasks -> Developers create install instruction documents for their added assets, if appropriate -> All developers push their PR's -> Merge -> Update repository documentation and information -> Repeat until development is completed.

 - **Main Build Updating**  
   Happens simultaneously with the Developing / Iterations process. Development Iteration is completed -> Project Lead merges **test/main** into **main**, ensuring that the Main scene is unchanged during the merge -> Project Lead updates the **Main** scene with components from the **Test** scene / new assets added from the merge -> Tests and reviews the current **Main** scene -> If bugs or issues are found, documents them and reverts the build. Otherwise, documents the changes in the new **Main** scene / **main** branch.

 - **Complete and Submit**  
   Test the final build -> Review documentation -> Finalize documentation -> Review documentation again -> Create Release -> Submit, if needed -> Conduct team AAR -> Celebrate a little huzzah.
