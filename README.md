# Pocketsharp - Alpha 1.0.0.0
Pocketsharp is a C# library designed to easily connect with Pocketbase and is compatible with .NET 8.0 applications

<br>

# How to Use Pocketsharp <a name="how-to-use-pocketsharp"></a>
- Import the Pocketsharp.dll reference [Microsoft how to use the reference manager](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022)

<br>

- Add the using directive (optional)
```csharp
    using Pocketsharp
```

<br>

- Access the methods
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

- Access the objects
```csharp
    Pocketsharp.Objects.Record record = new();
    Pocketsharp.Objects.Response response = new();
```

<br>

- Access the json converter
```csharp
    string jsonRecord = Pocketsharp.Utility.JsonUtility.SerializeRecordToJson(recordObject);
    Pocketsharp.Objects.Record recordFromJson = Pocketsharp.Utility.JsonUtility.DeserializeJsonToRecord(jsonRecord);

    string jsonResponse = Pocketsharp.Utility.JsonUtility.SerializeResponseToJson(responseObject);
    Pocketsharp.Objects.Response = Pocketsharp.Utility.JsonUtility.DeserializeJsonToResponse(jsonResponse);
```

<br>

# 2. Example project <a name="example-project"></a>
Check out this Windows Forms project to see a demonstration of Pocketsharp in action
- https://github.com/aleqkzander/Pocketsharp-Desktop
