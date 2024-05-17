/*
 * Use multipart/form-data as it's the only form of uploading files
 */

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
            public static async Task<string?> RegisterAsync(HttpClient client, Record record, string password, string passwordConfirm)
            {
                try
                {
                    if (InputValidation.RegistrationInputIsValid(client, record, password, passwordConfirm) == false)
                        throw new NotImplementedException($"LIBRARY ERROR\n\n{"Input is not valid"}");

                    using var content = new MultipartFormDataContent
                    {
                        { new StringContent(record.Username),
                            "username" },

                        { new StringContent(record.Email),                      
                            "email" },

                        { new StringContent(record.EmailVisibility.ToString()), 
                            "emailVisibility" },

                        { new StringContent(record.Name),                       
                            "name"},

                        { new StringContent(password),                          
                            "password" },

                        { new StringContent(passwordConfirm),
                            "passwordConfirm" },
                    };

                    if (record.Avatar.Length != 0)
                        content.Add(new ByteArrayContent(record.Avatar ?? []), "avatar", $"{record.Id}_avatar.png");

                    var response = await client.PostAsync(registerApiEndpoint, content);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
                }
            }

            /// <summary>
            /// Log in a user and return an AuthResponse object upon successful authentication
            /// </summary>
            /// <param name="client"> base url is mandatory</param>
            /// <param name="identity"> is mandatory</param>
            /// <param name="password"> is mandatory</param>
            /// <returns></returns>
            public static async Task<string?> LoginWAsync(HttpClient client, string email, string password)
            {
                try
                {
                    if (InputValidation.LoginInputIsValid(client, email, password) == false)
                        throw new NotImplementedException($"LIBRARY ERROR\n\n{"Input is not valid"}");

                    var requestBody = new
                    {
                        email,
                        password
                    };

                    var response = await client.PostAsJsonAsync(loginApiEndpoint, requestBody);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
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
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
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
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
                }
            }
        }
    }
}