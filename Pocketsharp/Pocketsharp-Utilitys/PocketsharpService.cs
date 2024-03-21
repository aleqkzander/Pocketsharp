/*
 * Important:
 * Setup the base url for your HttpClient-Instance before passing it as a parameter - otherwise null get returned.
 */

using Pocketsharp.Pocketsharp_Utilitys.Objects;
using System.Net.Http.Json;

namespace Pocketsharp.Pocketsharp_Utilitys
{
    internal class PocketsharpService
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
        public static async Task<AuthRecord?> RegisterWithPasswordAsync(HttpClient client, string username, string email, bool emailvisibility, string name, byte[] avatar, string password, string passwordConfirm)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return null;
                if (string.IsNullOrEmpty(email)) return null;
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm)) return null;

                string apiEndpoint = "/api/collections/users/records";

                var requestbody = new
                {
                    username,
                    email,
                    emailvisibility,
                    name,
                    avatar,
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
    }
}
