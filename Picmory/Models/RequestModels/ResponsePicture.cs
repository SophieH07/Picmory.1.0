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

        public ResponsePicture(int Id, string Description, string FolderName, AccessType Access, byte[] UploadTime)
        {
            this.Id = Id;
            this.Description = Description;
            this.FolderName = FolderName;
            this.Access = Access;
            this.UploadTime = UploadTime;
        }


        public int Id { get; set; }
        public string Description { get; set; }
        public string FolderName { get; set; }
        public AccessType Access { get; set; }
        public byte[] UploadTime  { get; set; }
    }
}
