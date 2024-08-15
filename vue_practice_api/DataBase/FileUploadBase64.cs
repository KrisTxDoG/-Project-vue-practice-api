namespace vue_practice_api.DataBase
{
    public class FileUploadBase64
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
