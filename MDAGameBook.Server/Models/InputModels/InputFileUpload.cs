namespace GameBookASP.Models.InputModels {
    public class InputFilesUpload {
        public ICollection<IFormFile> Files { get; set; }
    }
    public class InputFileUpload {
        public IFormFile File { get; set; }
    }
}
