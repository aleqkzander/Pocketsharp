# Pocketsharp - Alpha 1.0.0.0
Pocketsharp is a C# library for Pocketbase integration.

<br>

# How to use Pocketsharp

- Import the Pocketsharp.dll reference
  - [Microsoft how to use the reference manager](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022)

<br>

- Add the using directive
  - ```csharp
    using Pocketsharp
    ```
<br>

- Access the objects
  - ```csharp
    PocketsharpObjects.AuthRecord authRecord = new();
    PocketsharpObjects.AuthResponse authResponse = new();
    ```
<br>

- Access the methods
  - ```csharp
    PocketsharpMethods.RegisterWithPasswordAsync(HttpClient client, AuthRecord record, string password, string passwordConfirm)
    PocketsharpMethods.LoginWithPasswordAsync(HttpClient client, string identity, string password)
    PocketsharpMethods.UpdateUserAsync(HttpClient client, AuthResponse authResponse, string ? oldPaddword = null, string? password = null, string? passwordConfirm = null)
    PocketsharpMethods.DeleteUserAsync(HttpClient client,string recordId, string token)
    ```

<br>

# Example

```csharp
    // create a client instance
    HttpClient client = new()
    {
        BaseAddress = new Uri("your_baseAdress")
    };


    // create an auth record
    PocketsharpObjects.AuthRecord? authRecord = new()
    {
        Username = "username",
        EmailVisibility = true,
        Email = "youremail@test.com",
        Name = "name",
        Avatar = []
    };


    // register and access the returned authRecord
    PocketsharpObjects.AuthRecord? authRecordResponse = await PocketsharpMethods.RegisterWithPasswordAsync(client, authRecord, password, passwordConfirm);


    // login and acess the returned authResponse
    PocketsharpObjects.AuthResponse? authResponse = await PocketsharpMethods.LoginWithPasswordAsync(client, username, password);

    
    // update user information and access the updated authRecord
    mysavedResponseObject.Record.Name = "yournewname"; // we change our name for example
    PocketsharpObjects.AuthRecord? updatedAuthRecordResponse = await PocketsharpMethods.UpdateUserAsync(client, mysavedResponseObject);


    // access user id and token from your auth-response-object
    bool isDeleted = await PocketsharpMethods.DeleteUserAsync(client, savedAuthResponse.record.id, savedAuthResponse.token);
```

<br>

# Object references
## AuthResponse
```csharp
    public class AuthResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("record")]
        public AuthRecord? Record { get; set; }
    }
```
## AuthRecord
```csharp
    public class AuthRecord
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("collectionId")]
        public string? CollectionId { get; set; }

        [JsonPropertyName("collectionName")]
        public string? CollectionName { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("verified")]
        public bool? Verified { get; set; }

        [JsonPropertyName("emailVisibility")]
        public bool? EmailVisibility { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("created")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonPropertyName("updated")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? Updated { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar")]
        public byte[]? Avatar { get; set; }
    }

    // make sure date time get converted correctly
    internal class JsonDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String && DateTime.TryParse(reader.GetString(), out var dateTime))
            {
                return dateTime;
            }

            throw new JsonException($"Unable to parse '{reader.GetString()}' as DateTime.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
```
