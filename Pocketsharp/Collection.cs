using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Pocketsharp
{
    public class Collection
    {
        /// <summary>
        /// Submit a collection object to a designated target
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authToken"></param>
        /// <param name="collection"></param>
        /// <param name="collectionEntrys"></param>
        /// <returns></returns>
        public static async Task<string?> CreateEntry(HttpClient client, string authToken, string targetCollection, object collectionEntrys)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) 
                    throw new NotImplementedException("Setup the base address on the client");

                if (string.IsNullOrEmpty(authToken))
                    throw new NotImplementedException("Auth token is required");

                if (string.IsNullOrEmpty(targetCollection))
                    throw new NotImplementedException("A target collection is required");

                if (collectionEntrys == null)
                    throw new NotImplementedException("A valid object is required");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string apiEndpoint = $"/api/collections/{targetCollection}/records";
                var response = await client.PostAsJsonAsync(apiEndpoint, collectionEntrys);

                if (!response.IsSuccessStatusCode) 
                    throw new Exception(response.StatusCode.ToString());

                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) == false) return responseBody;
                else throw new NotImplementedException($"LIBRARY INFO\n\n{"Entry creation failed gracefully"}");
            }
            catch (Exception exception)
            {
                throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
            }
        }

        /// <summary>
        /// Retrieve all entries from a designated target as a JsonNode
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authToken"></param>
        /// <param name="targetCollection"></param>
        /// <returns></returns>
        public static async Task<JsonNode?> GetAllEntrysFromTarget(HttpClient client, string authToken, string targetCollection)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) 
                    throw new NotImplementedException("Setup the base address on the client");

                if (string.IsNullOrEmpty(authToken))
                    throw new NotImplementedException("Auth token is required");

                if (string.IsNullOrEmpty(targetCollection))
                    throw new NotImplementedException("Target collection is required");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string apiEndpoint = $"/api/collections/{targetCollection}/records";
                var response = await client.GetAsync(apiEndpoint);

                if (!response.IsSuccessStatusCode) 
                    throw new Exception(response.StatusCode.ToString());

                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) == false)
                {
                    JsonObject? jsonObject = JsonNode.Parse(responseBody) as JsonObject;
                    return jsonObject?["items"];
                }
                else
                {
                    throw new NotImplementedException($"LIBRARY INFO\n\n{"Getting entrys failed gracefully"}");
                }
            }
            catch (Exception exception)
            {
                throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
            }
        }

        /// <summary>
        /// Retrieve a specific collection entry
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authToken"></param>
        /// <param name="targetCollection"></param>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        public static async Task<string?> GetSpecificEntryFromTarget(HttpClient client, string authToken, string targetCollection, string entryId)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) 
                    throw new NotImplementedException("Setup the base address on the client");

                if (string.IsNullOrEmpty(authToken))
                    throw new NotImplementedException("Auth token is required");

                if (string.IsNullOrEmpty(targetCollection))
                    throw new NotImplementedException("Target collection is required");

                if (string.IsNullOrEmpty(entryId))
                    throw new NotImplementedException("Entry ID required");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string apiEndpoint = $"/api/collections/{targetCollection}/records/{entryId}";
                var response = await client.GetAsync(apiEndpoint);

                if (!response.IsSuccessStatusCode) 
                    throw new Exception(response.StatusCode.ToString());

                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) == false) return responseBody;
                else throw new NotImplementedException($"LIBRARY INFO\n\n{"Getting specific entry failed gracefully"}");
            }
            catch (Exception exception)
            {
                throw new NotImplementedException($"LIBRARY ERROR\n\n{exception}");
            }
        }
    }
}