using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChuckNorrisImport.Model
{
    public class Fact
    {
        public int Id { get; set; }

        [JsonPropertyName("id")]
        [MaxLength(40)]
        public string ChuckNorrisId { get; set; }

        [JsonPropertyName("url")]
        [MaxLength(1024)]
        public string Url { get; set; }

        [JsonPropertyName("value")]
        public string Joke { get; set; }
    }
}