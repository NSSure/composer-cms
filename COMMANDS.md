- Command to initialize the migrations from the web project and target the output to the core project.
dotnet ef migrations add DatabaseV1 --project ../ComposerCMS.Core -o CoreSystem/DAL/Migrations