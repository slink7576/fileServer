using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class FileData : BaseEntity
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public long Size { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
    }
}
