using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestModels
{
    public class PictureChange
    {
        public PictureChange()
        { }

        public PictureChange(int Id, string Description, AccessType Access, string FolderName, User user)
        {
            this.Id = Id;
            this.Description = Description;
            this.Access = Access;
            this.FolderName = FolderName;
            this.Owner = user;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public AccessType? Access { get; set; }
        public string FolderName { get; set; }
        public User Owner { get; set; }
    }
}
