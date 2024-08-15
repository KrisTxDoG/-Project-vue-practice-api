namespace vue_practice_api.DataBase.Dto
{
    public class FileUploadDto
    {
        public string FileName { get; set; }       // 文件名稱

        public string ContentType { get; set; }   // 文件類型 (MIME type)

        public string FileDate { get; set; }         // Base64 字符串
    }
}
