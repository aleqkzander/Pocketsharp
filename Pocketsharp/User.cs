using Pocketsharp.Objects;
using Pocketsharp.Utility;
using System.Net.Http.Headers;

namespace Pocketsharp
{
    public class User
    {
        /// <summary>
        /// Return a users avatar as byte-array
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authResponse"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static async Task<byte[]> DownloadAvatar(HttpClient client, Response authResponse)
        {
            try
            {
                if (InputUtility.AvatarDownloadInputIsValid(client, authResponse) == false)
                    throw new NotImplementedException($"LIBRARY ERROR AVATAR\n\n{"Input is not valid"}");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                string apiEndpoint = $"/api/files/{authResponse.Record.CollectionId}/{authResponse.Record.Id}/{authResponse.Record.AvatarFilename}";
                var response = await client.GetAsync(apiEndpoint);

                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                if (imageBytes.Length != 0) return imageBytes;
                else throw new NotImplementedException($"LIBRARY INFO\n\n{"Avatar download gracefully failed"}");
            }
            catch (Exception exception)
            {
                throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
            }
        }

        /// <summary>
        /// Update and return an Record-JsonObject upon success
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
                if (InputUtility.UpdateInputIsValid(client, authResponse) == false)
                    throw new NotImplementedException($"LIBRARY ERROR UPDATE\n\n{"Input is not valid"}");

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

                if (authResponse.Record.AvatarFilename.Length != 0)
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                string apiEndpoint = $"/api/collections/users/records/{authResponse.Record!.Id}";
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
        /// Delete a user record by ID
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string apiEndpoint = $"/api/collections/users/records/{recordId}";
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