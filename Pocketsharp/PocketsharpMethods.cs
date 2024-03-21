/*
 * Important:
 * Setup the base url for your HttpClient-Instance before passing it as a parameter - otherwise null get returned.
 */

using PocketsharpObjects;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Pocketsharp
{
    public class PocketsharpMethods
    {
        /// <summary>
        /// Register a new user and return an AuthRecord object on success
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
        public static async Task<AuthRecord?> RegisterWithPasswordAsync(HttpClient client, AuthRecord record, string password, string passwordConfirm)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return null;
                if (string.IsNullOrEmpty(record.Email)) return null;
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm)) return null;

                string apiEndpoint = "/api/collections/users/records";

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

                var response = await client.PostAsJsonAsync(apiEndpoint, requestbody);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<AuthRecord>();

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Authenticate a user and return an AuthResonse object on success
        /// </summary>
        /// <param name="client"> base url is mandatory</param>
        /// <param name="identity"> is mandatory</param>
        /// <param name="password"> is mandatory</param>
        /// <returns></returns>
        public static async Task<AuthResponse?> LoginWithPasswordAsync(HttpClient client, string identity, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return null;
                if (string.IsNullOrEmpty(identity) || string.IsNullOrEmpty(password)) return null;

                string apiEndpoint = "/api/collections/users/auth-with-password";

                var requestBody = new
                {
                    identity,
                    password
                };

                var response = await client.PostAsJsonAsync(apiEndpoint, requestBody);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<AuthResponse>();

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Update a user's information and return an updated AuthRecord object on success
        /// </summary>
        /// <param name="client">The HttpClient instance with the base URL set</param>
        /// <param name="record">The updated user record</param>
        /// <param name="password">User's current password (required only if changing password)</param>
        /// <param name="newPassword">User's new password (required only if changing password)</param>
        /// <param name="passwordConfirm">Confirmation of user's new password (required only if changing password)</param>
        /// <returns>An AuthRecord object representing the updated user</returns>
        public static async Task<AuthRecord?> UpdateUserAsync(HttpClient client, AuthResponse authResponse, string ? oldPaddword = null, string? password = null, string? passwordConfirm = null)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return null;
                if (authResponse == null) return null;

                string apiEndpoint = $"/api/collections/users/records/{authResponse.Record!.Id}";

                var requestBody = new
                {
                    authResponse.Record.Username,
                    authResponse.Record.Email,
                    authResponse.Record.EmailVisibility,
                    authResponse.Record.Name,
                    authResponse.Record.Avatar,
                    oldPaddword,
                    password,
                    passwordConfirm
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                var response = await client.PatchAsJsonAsync(apiEndpoint, requestBody);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<AuthRecord>();

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Delete a user record by ID
        /// </summary>
        /// <param name="client">The HttpClient instance with the base URL set</param>
        /// <param name="recordId">The ID of the user record to delete</param>
        /// <returns>True if deletion is successful, otherwise false</returns>
        public static async Task<bool> DeleteUserAsync(HttpClient client,string recordId, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return false;
                string apiEndpoint = $"/api/collections/users/records/{recordId}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync(apiEndpoint);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
