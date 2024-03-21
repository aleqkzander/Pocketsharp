# Pocketsharp
Pocketsharp is a C# library for Pocketbase integration.

<br>

# How to use Pocketsharp

- Import the Pocketsharp.dll reference
  - [Microsoft how to use reference manager](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022)

<br>

- Add the using directive
  - ```csharp
    using Pocketsharp
    ```
<br>

- Access the Pocketsharp objects
  - ```csharp
    PocketsharpObjects.AuthRecord authRecord = new();
    PocketsharpObjects.AuthResponse authResponse = new();
    ```
<br>

- Access the Authentication methods
  - ```csharp
    await PocketsharpAuthentication.RegisterWithPasswordAsync(HttpClient client, AuthRecord record, string password, string passwordConfirm);
    await PocketsharpAuthentication.LoginWithPasswordAsync(HttpClient client, string identity, string password);
    ```
