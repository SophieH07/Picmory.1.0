using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestModels
{
    public class ResponsePicture
    {
        public ResponsePicture()
        {
        }

        public ResponsePicture(int Id, string Description, string FolderName, AccessType Access, DateTime UploadTime, List<string> Likes)
        {
            this.Id = Id;
            this.Description = Description;
            this.FolderName = FolderName;
            this.Access = Access;
            this.UploadTime = UploadTime;
            this.Likes = Likes;
        }


        public int Id { get; set; }
        public string Description { get; set; }
        public string FolderName { get; set; }
        public AccessType Access { get; set; }
        public DateTime UploadTime  { get; set; }
        public List<string> Likes { get; set; }
    }
}
