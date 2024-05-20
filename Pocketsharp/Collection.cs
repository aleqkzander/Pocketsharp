﻿using System.Net.Http.Headers;
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string apiEndpoint = $"/api/collections/{targetCollection}/records";
                var response = await client.PostAsJsonAsync(apiEndpoint, collectionEntrys);

                if (!response.IsSuccessStatusCode) 
                    throw new Exception(response.StatusCode.ToString());

                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch
            {
                return null;
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string apiEndpoint = $"/api/collections/{targetCollection}/records";
                var response = await client.GetAsync(apiEndpoint);

                if (!response.IsSuccessStatusCode) 
                    throw new Exception(response.StatusCode.ToString());

                var responseBody = await response.Content.ReadAsStringAsync();
                JsonObject? jsonObject = JsonNode.Parse(responseBody) as JsonObject;

                return jsonObject?["items"];
            }
            catch
            {
                return null;
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
        public static async Task<string?> GetSpecificEntryFromTarget(HttpClient client, string authToken, string targetCollection, string collectionId)
        {
            try
            {
                if (string.IsNullOrEmpty(client.BaseAddress?.ToString())) 
                    throw new NotImplementedException("Setup the base address on the client");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string apiEndpoint = $"/api/collections/{targetCollection}/records/{collectionId}";
                var response = await client.GetAsync(apiEndpoint);

                if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}