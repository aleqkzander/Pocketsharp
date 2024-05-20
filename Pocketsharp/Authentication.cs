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
                    if (InputUtility.RegistrationInputIsValid(client, record, password, passwordConfirm) == false)
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
                        content.Add(new ByteArrayContent(record.AvatarByte ?? []), "avatar", $"{record.Id}_avatar.png");

                    var response = await client.PostAsync(registerApiEndpoint, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseString) == false) return responseString;
                    else throw new NotImplementedException($"LIBRARY INFO\n\n{"Something went wrong while processing"}");
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
            public static async Task<string?> LoginAsync(HttpClient client, string email, string password)
            {
                try
                {
                    if (InputUtility.LoginInputIsValid(client, email, password) == false)
                        throw new NotImplementedException($"LIBRARY ERROR\n\n{"Input is not valid"}");

                    var requestBody = new
                    {
                        identity = email,
                        password
                    };

                    var response = await client.PostAsJsonAsync(loginApiEndpoint, requestBody);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseString) == false) return responseString;
                    else throw new NotImplementedException($"LIBRARY INFO\n\n{"Something went wrong while processing"}");
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
            /// Return a users avatar as byte image
            /// </summary>
            /// <param name="client"></param>
            /// <param name="authResponse"></param>
            /// <param name="filename"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public static async Task<byte[]> DownloadAvatar(HttpClient client, Response authResponse, string filename)
            {
                try
                {
                    if (InputUtility.AvatarDownloadInputIsValid(client, authResponse, filename) == false)
                        throw new NotImplementedException($"LIBRARY ERROR\n\n{"Input is not valid"}");

                    string apiEndpoint = $"/api/files/{authResponse.Record.CollectionId}/{authResponse.Record.Id}/{filename}";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                    var response = await client.GetAsync(apiEndpoint);
                    return await response.Content.ReadAsByteArrayAsync();
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
                }
            }

            /// <summary>
            /// Update a user's information and return an updated AuthRecord object upon successful completion
            /// </summary>
            /// <param name="client">The HttpClient instance with the base URL set</param>
            /// <param name="record">The updated user record</param>
            /// <param name="password">User's current password (required only if changing password)</param>
            /// <param name="newPassword">User's new password (required only if changing password)</param>
            /// <param name="passwordConfirm">Confirmation of user's new password (required only if changing password)</param>
            /// <returns>An AuthRecord object representing the updated user</returns>
            public static async Task<string?> UpdateAsync(HttpClient client, Response authResponse, string? oldPassword = null, string? newPassword = null, string? passwordConfirm = null)
            {
                try
                {
                    if (string.IsNullOrEmpty(client.BaseAddress?.ToString()))
                        throw new NotImplementedException("Setup the base address on the client");

                    if (string.IsNullOrEmpty(authResponse.Record.Email))
                        throw new NotImplementedException("An email is always required as it is users identity");

                    string apiEndpoint = $"/api/collections/users/records/{authResponse.Record!.Id}";

                    using var content = new MultipartFormDataContent
                    {
                        { new StringContent(authResponse.Record.Username),
                            "username" },

                        { new StringContent(authResponse.Record.Email),
                            "email" },

                        { new StringContent(authResponse.Record.EmailVisibility.ToString()),
                            "emailVisibility" },

                        { new StringContent(authResponse.Record.Name),
                            "name"},
                    };

                    if (authResponse.Record.Avatar.Length != 0)
                        content.Add(new ByteArrayContent(authResponse.Record.AvatarByte ?? []), "avatar", $"{authResponse.Record.Id}_avatar.png");

                    if (oldPassword != null)
                    {
                        content.Add(new StringContent(oldPassword), "oldPassword");

                        if (string.IsNullOrEmpty(newPassword))
                            throw new NotImplementedException("LIBRARY ERROR:\n\nIf you wan't to change your password you need to provide a new password");
                        content.Add(new StringContent(newPassword!), "password");

                        if (string.IsNullOrEmpty(passwordConfirm) || passwordConfirm != newPassword)
                            throw new NotImplementedException("LIBRARY ERROR:\n\nYour conformation password doesn't match");
                        content.Add(new StringContent(passwordConfirm!), "passwordConfirm");
                    }

                    #region old stuff
                    //var requestBody = new
                    //{
                    //    authResponse.Record.Username,
                    //    authResponse.Record.Email,
                    //    authResponse.Record.EmailVisibility,
                    //    authResponse.Record.Name,
                    //    authResponse.Record.Avatar,
                    //    oldPaddword,
                    //    newPassword,
                    //    passwordConfirm
                    //};

                    //var response = await client.PatchAsJsonAsync(apiEndpoint, requestBody);
                    #endregion oldstuff

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
                    var response = await client.PatchAsync(apiEndpoint, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseString) == false) return responseString;
                    else throw new NotImplementedException($"LIBRARY INFO\n\n{"Something went wrong while processing"}");
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