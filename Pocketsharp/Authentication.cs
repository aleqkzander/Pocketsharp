using Pocketsharp.Objects;
using Pocketsharp.Utility;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Pocketsharp
{
    public class Authentication
    {
        public class EmailAndPassword
        {
            static readonly string registerApiEndpoint = "/api/collections/users/records";
            static readonly string loginApiEndpoint = "/api/collections/users/auth-with-password";

            /// <summary>
            /// Register a new user and return an AuthRecord object upon successful registration
            /// </summary>
            /// <param name="client"> base url is mandatory</param>
            /// <param name="username"></param>
            /// <param name="email"> is mandatory</param>
            /// <param name="emailvisibility"></param>
            /// <param name="name"></param>
            /// <param name="avatar"></param>
            /// <param name="password"> is mandatory</param>
            /// <param name="passwordConfirm"> is mandatory</param>
            /// <returns></returns>
            public static async Task<Record?> RegisterAsync(HttpClient client, Record record, string password, string passwordConfirm)
            {
                try
                {
                    if (!InputValidation.RegistrationInputIsValid(client, record, password, passwordConfirm))
                        throw new NotImplementedException($"LIBRARY ERROR\n{"Register input is not valid"}");

                    var requestbody = new
                    {
                        record.Username,
                        record.Email,
                        record.EmailVisibility,
                        record.Name,
                        record.Avatar,
                        password,
                        passwordConfirm
                    };

                    var response = await client.PostAsJsonAsync(registerApiEndpoint, requestbody);
                    return await response.Content.ReadFromJsonAsync<Record>();
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n{exception.Message}");
                }
            }

            /// <summary>
            /// Log in a user and return an AuthResponse object upon successful authentication
            /// </summary>
            /// <param name="client"> base url is mandatory</param>
            /// <param name="identity"> is mandatory</param>
            /// <param name="password"> is mandatory</param>
            /// <returns></returns>
            public static async Task<Response?> LoginWAsync(HttpClient client, string email, string password)
            {
                try
                {
                    if (!InputValidation.LoginInputIsValid(client, email, password))
                        throw new NotImplementedException($"LIBRARY ERROR\n{"Login input is not valid"}");

                    var requestBody = new
                    {
                        email,
                        password
                    };

                    var response = await client.PostAsJsonAsync(loginApiEndpoint, requestBody);
                    return await response.Content.ReadFromJsonAsync<Response>();
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n{exception.Message}");
                }
            }
        }

        public class User
        {
            /// <summary>
            /// Update a user's information and return an updated AuthRecord object upon successful completion
            /// </summary>
            /// <param name="client">The HttpClient instance with the base URL set</param>
            /// <param name="record">The updated user record</param>
            /// <param name="password">User's current password (required only if changing password)</param>
            /// <param name="newPassword">User's new password (required only if changing password)</param>
            /// <param name="passwordConfirm">Confirmation of user's new password (required only if changing password)</param>
            /// <returns>An AuthRecord object representing the updated user</returns>
            public static async Task<Record?> UpdateAsync(HttpClient client, Response authResponse, string? oldPaddword = null, string? newPassword = null, string? passwordConfirm = null)
            {
                try
                {
                    if (string.IsNullOrEmpty(client.BaseAddress?.ToString()))
                        throw new NotImplementedException("Setup the base address on the client");

                    string apiEndpoint = $"/api/collections/users/records/{authResponse.Record!.Id}";

                    var requestBody = new
                    {
                        authResponse.Record.Username,
                        authResponse.Record.Email,
                        authResponse.Record.EmailVisibility,
                        authResponse.Record.Name,
                        authResponse.Record.Avatar,
                        oldPaddword,
                        newPassword,
                        passwordConfirm
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
                    var response = await client.PatchAsJsonAsync(apiEndpoint, requestBody);

                    return await response.Content.ReadFromJsonAsync<Record>();
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n{exception.Message}");
                }
            }

            /// <summary>
            /// Delete a user record by its ID
            /// </summary>
            /// <param name="client">The HttpClient instance with the base URL set</param>
            /// <param name="recordId">The ID of the user record to delete</param>
            /// <returns>True if deletion is successful, otherwise false</returns>
            public static async Task<bool> DeleteAsync(HttpClient client, string recordId, string token)
            {
                try
                {
                    if (string.IsNullOrEmpty(client.BaseAddress?.ToString()))
                        throw new NotImplementedException("Setup the base address on the client");

                    string apiEndpoint = $"/api/collections/users/records/{recordId}";

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.DeleteAsync(apiEndpoint);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n{exception.Message}");
                }
            }
        }
    }
}