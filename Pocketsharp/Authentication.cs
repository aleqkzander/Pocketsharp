using Pocketsharp.Objects;
using Pocketsharp.Utility;
using System.Net.Http.Json;

namespace Pocketsharp
{
    public class Authentication
    {
        public class EmailAndPassword
        {
            /// <summary>
            /// Register and return an Record-JsonObject upon success
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


                    string apiEndpoint = "/api/collections/users/records";
                    var response = await client.PostAsync(apiEndpoint, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseString) == false) return responseString;
                    else throw new NotImplementedException($"LIBRARY INFO\n\n{"Registration gracefully failed"}");
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
                }
            }

            /// <summary>
            /// Login and return an Response-JsonObject upon success
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

                    string apiEndpoint = "/api/collections/users/auth-with-password";
                    var response = await client.PostAsJsonAsync(apiEndpoint, requestBody);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseString) == false) return responseString;
                    else throw new NotImplementedException($"LIBRARY INFO\n\n{"Login gracefully failed"}");
                }
                catch (Exception exception)
                {
                    throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
                }
            }
        }
    }
}