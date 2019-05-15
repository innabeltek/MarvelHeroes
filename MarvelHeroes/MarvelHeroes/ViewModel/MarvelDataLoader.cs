using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using MarvelHeroes.Models;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MarvelHeroes
{
   public class MarvelDataLoader
    {
        private const string PrivateKey = "1b782b3780b90820f41c63a89cfb76d7ac3dcecd";
        private const string PublicKey = "6ed8f5fbd412907a2ead7d16c9ca3e93";

        public  async Task<List<Character>> GetMarvelCharactersAsync( int offset, int limit)
        {
            var characterDataWrapper = await GetCharacterDataWrapperAsync(offset, limit);
            var characters = characterDataWrapper.data.results;
            List<Character> marvelCharacters = new List<Character>();

           foreach (var character in characters)
            {
                if (character.thumbnail != null && character.thumbnail.path != String.Empty)                  
                {
                    character.thumbnail.small = String.Format("{0}/landscape_amazing.{1}",
                        character.thumbnail.path, character.thumbnail.extension);

                    character.thumbnail.large = String.Format("{0}/portrait_xlarge.{1}",
                        character.thumbnail.path, character.thumbnail.extension);
                    
                    marvelCharacters.Add(character);
                }
            }
            
            return marvelCharacters;
        }

        private static async Task<CharacterDataWrapper> GetCharacterDataWrapperAsync(int offset, int limit)
        {
          
            var timeStamp = DateTime.Now.Ticks.ToString(); 

            var hash = GenerateHash(timeStamp);
            string path = String.Format("https://gateway.marvel.com:443/v1/public/characters?limit={0}&offset={1}&apikey={2}&ts={3}&hash={4}", limit, offset, PublicKey, timeStamp, hash);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(path);
            var response = await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            JObject o = JObject.Parse(content);
              
            CharacterDataWrapper deserializedUser = new CharacterDataWrapper();
            deserializedUser = JsonConvert.DeserializeObject<CharacterDataWrapper>(o.ToString());

            return deserializedUser;
        }

        private static string GenerateHash(string timeStamp)
        {
            var toHash = timeStamp + PrivateKey + PublicKey;
          
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(toHash);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

