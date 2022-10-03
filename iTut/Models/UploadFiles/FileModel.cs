using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iTut.Models.UploadFiles
{
    public abstract class FileModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; } = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
