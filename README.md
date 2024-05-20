# Pocketsharp - Alpha 1.0.0.0
Pocketsharp is a C# library for Pocketbase integration.

<br>

# How to use Pocketsharp

- Import the Pocketsharp.dll reference [Microsoft how to use the reference manager](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022)

<br>

- Add using directive
```csharp
    using Pocketsharp
```

<br>

- Access methods
```csharp
    string response = Pocketsharp.Authentication.EmailAndPassword.RegisterAsync(HttpClient client, Record record, string password, string passwordConfirm);
    string response = Pocketsharp.Authentication.EmailAndPassword.LoginAsync(HttpClient client, string email, string password);

    byte[] response = Pocketsharp.User.DownloadAvatar(HttpClient client, Response authResponse);
    string response = Pocketsharp.User.UpdateAsync(HttpClient client, Response authResponse, string? oldPassword = null, string? newPassword = null, string? passwordConfirm = null);
    bool response = Pocketsharp.User.DeleteAsync(HttpClient client, string recordId, string token);

    string response = Pocketsharp.Collection.CreateEntry(HttpClient client, string authToken, string targetCollection, object userObject);
    JsonNode response = Pocketsharp.Collection.GetAllEntrysFromTarget(HttpClient client, string authToken, string targetCollection);
    string response = Pocketsharp.Collection.GetSpecificEntryFromTarget(HttpClient client, string authToken, string targetCollection, string entryId);
```

<br>

- Access objects
```csharp
    Pocketsharp.Objects.Record record = new();
    Pocketsharp.Objects.Response response = new();
```

<br>

- Access helper methods
```csharp
    public static string SerializeRecordToJson(Record? record);
    public static Record? DeserializeJsonToRecord(string json);

    public static string SerializeResponseToJson(Response? response);
    public static Response? DeserializeJsonToResponse(string json);
```

<br>

# Example
