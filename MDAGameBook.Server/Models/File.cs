using System.Text.Json.Serialization;

namespace GameBookASP.Models {
    public class File {
        public Guid Id { get; set; }
        public required string FileName { get; set; } 
        public string? FileType { get; set; }
        public DateTime? UploadedAt { get; set; }
        [JsonIgnore]
        public required string FilePath { get; set; }
    }
}
