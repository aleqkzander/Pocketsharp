using Pocketsharp.PocketsharpObjects;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace Pocketsharp
{
    public class PocketsharpCollection
    {
        /// <summary>
        /// Post a collection object into a specific collection
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authToken"></param>
        /// <param name="collection"></param>
        /// <param name="collectionEntrys"></param>
        /// <returns></returns>
        public static async Task<string> CreateCollectionEntry(HttpClient client, string authToken, string targetCollection, object collectionEntrys)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return "Client has no base address";
                string apiEndpoint = $"/api/collections/{targetCollection}/records";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.PostAsJsonAsync(apiEndpoint, collectionEntrys);
                var responseMessage = await response.Content.ReadAsStringAsync();
                return responseMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Get a list of all collection entrys
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authToken"></param>
        /// <param name="targetCollection"></param>
        /// <returns></returns>
        public static async Task<CollectionResponse<object>?> GetAllCollectionEntrys(HttpClient client, string authToken, string targetCollection)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) throw new Exception("Client has no base address");
                string apiEndpoint = $"/api/collections/{targetCollection}/records";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.GetAsync(apiEndpoint);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<CollectionResponse<object>>(responseBody);
                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a specific collection entry
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authToken"></param>
        /// <param name="targetCollection"></param>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        public static async Task<string> GetSpecificCollectionEntry(HttpClient client, string authToken, string targetCollection, string collectionId)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) return "Client has no base address";
                string apiEndpoint = $"/api/collections/{targetCollection}/records/{collectionId}";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.GetAsync(apiEndpoint);
                var responseMessage = await response.Content.ReadAsStringAsync();
                return responseMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Get a specific propertie from a collection entry
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetCollectionValue<T>(string entry, string propertyName)
        {
            try
            {
                using JsonDocument collectionEntry = JsonDocument.Parse(entry);

                if (collectionEntry.RootElement.TryGetProperty(propertyName, out JsonElement entryValue))
                {
                    // string entryvalue = PocketsharpCollection.GetCollectionValue<string>(entry.ToString()!, "your desired field");
                    return JsonSerializer.Deserialize<string>(entryValue.GetRawText())!;
                }
                else
                {
                    return "entry is not present";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
