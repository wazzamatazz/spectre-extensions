# C# Repository Template

Repository template for a C# project.


# Getting Started

- Create a new repository on GitHub and choose this repository as the template, or click on the _"Use this template"_ button on the repository home page.
- Rename the solution file in the root of the repository ([RENAME-ME.sln](/RENAME-ME.sln)).
- Update [Directory.Build.props](/Directory.Build.props) in the root folder and replace the placeholder values in the shared project properties (e.g. `{{COPYRIGHT_START_YEAR}}`).
- Update [build.cake](/build.cake) in the root folder and replace the `DefaultSolutionFile` constant at the start of the file with the name of your solution file.
- Create new library and application projects in the `src` folder.
- Create test and benchmarking projects in the `test` folder.
- Create example projects that demonstrate the library and application projects in the `samples` folder.


# Building the Solution

The repository uses [Cake](https://cakebuild.net/) for cross-platform build automation. The build script allows for metadata such as a build counter to be specified when called by a continuous integration system such as TeamCity.

A build can be run from the command line using the [build.ps1](/build.ps1) PowerShell script or the [build.sh](/build.sh) Bash script. For documentation about the available build script parameters, see [build.cake](/build.cake).


# Software Bill of Materials

To generate a Software Bill of Materials (SBOM) for the repository in [CycloneDX](https://cyclonedx.org/) XML format, run [build.ps1](./build.ps1) or [build.sh](./build.sh) with the `--target BillOfMaterials` parameter.

The resulting SBOM is written to the `artifacts/bom` folder.
