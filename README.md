# About Crypto
The Web application was created for testing purposes. It consist of React application and .Net WebApi.

## How to build
* [Install](https://dotnet.microsoft.com/en-us/download#/current) the latest .NET 8.0 SDK to run the WebApi
* Install NPM to run the React project

## Testing
Crypto has 2 types of tests:
* Unit test with 100% line and branch coverage
[<img src="https://github.com/PetarIlievDev/Crypto/blob/master/ReadMeImg/CryptoCodeCoverage.jpg">]

* Mutation test with approx 88% mutation score
[<img src="https://github.com/PetarIlievDev/Crypto/blob/master/ReadMeImg/MutationTestsCoverage.jpg">]

### Set up the testing
*For setting up the unit test you have to load runsettings file, then run the tests. Would recommend to install [Fine Code Coverage](https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage2022) extention, to be sure that the coverage format of the runsettings file fits to the Visualization of unit test code coverage.

*For running the mutation testing:
  *Open `Comand prompt` and navigate to project(solution) directory.
  *Run command `dotnet tool restore`. Tool `dotnet-stryker` should be restored.
  *Run command `dotnet stryker`.
  *When it's completed, the reports could be found in `{Solution directory}\StrykerOutput`.