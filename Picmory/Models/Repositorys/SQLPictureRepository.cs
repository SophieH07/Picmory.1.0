using Microsoft.EntityFrameworkCore;
using Picmory.Models.RequestModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Picmory.Models.Repositorys
{
    public class SQLPictureRepository : IPictureRepository
    {
        private readonly PicmoryDbContext context;
        
        public SQLPictureRepository(PicmoryDbContext context)
        {
            this.context = context;
        }


        public bool SavePicturePath(int Id, string path)
        {
            Picture picture = context.Pictures.FirstOrDefault(item => item.Id == Id);
            if (picture != null)
            {
                picture.Path = path;
                context.SaveChanges();
                return true;
            }
            return false;
        }
       
        public Picture SavePicture(Picture picture)
        {
            context.Pictures.Add(picture);
            context.SaveChanges();
            return picture;
        }
       
        public string GetPictureType(int Id)
        {
            var picture = context.Pictures.FirstOrDefault(item => item.Id == Id);
            return picture?.Type;
        }

        public bool ChangePictureData(PictureChange changeData)
        {
            Picture picture = context.Pictures.Find(changeData.Id);
            if (picture == null) { return false; }
            else {
                if (changeData.FolderName != null)
                { picture.Folder = context.Folders.Where(a => a.Owner == changeData.Owner && a.FolderName == changeData.FolderName).SingleOrDefault(); }
                if (changeData.Access != null)
                { picture.Access = (AccessType)changeData.Access; }
                if (changeData.Description != null)
                { picture.Description = changeData.Description; }
                context.SaveChanges();
            }
            return true;
        }

        public Success DeletePicture(int pictureId)
        {
            Picture picture = context.Pictures.Find(pictureId);
            context.Pictures.Remove(picture);
            context.SaveChanges();
            return Success.Successfull;
        }



        public Picture GetPicture(int id)
        {
            return context.Pictures.Include(a => a.Folder).Include(a => a.Owner).Where(a => a.Id == id).FirstOrDefault();
        }

        public List<string> GetAllPictureIds(User user)
        {
            List<string> pictureIds = new List<string>();
            try
            {
                return pictureIds = context.Pictures
                    .Where(a => a.Owner == user)
                    .OrderBy(a => a.DateCreated).Select(a => String.Format("{0}.{1}",a.Id, a.Type.Substring(6)))
                    .ToList();
                
            }
            catch (Exception)
            {
                return pictureIds;
            }
        }

        public List<string> GetAllPictureIdsInFolder(User user, string folderName)
        {
            List<string> pictureIds = new List<string>();
            try
            {
                return pictureIds = context.Pictures
                    .Where(a => a.Owner == user && a.Folder.FolderName == folderName)
                    .Include(a => a.Folder)
                    .OrderBy(a => a.DateCreated).Select(a => String.Format("{0}.{1}", a.Id, a.Type.Substring(6)))
                    .ToList();

            }
            catch (Exception)
            {
                return pictureIds;
            }
        }



        public List<ResponsePicture> GetPicturesForMe(User user, int offset, string folderName)
        {
            List<ResponsePicture> pictures = new List<ResponsePicture>();
            try
            {
                if (folderName == null)
                {
                    return pictures = context.Pictures
                        .Where(a => a.Owner == user)
                        .Include(a => a.Folder)
                        .Select(a => new ResponsePicture { Id = a.Id, Description = a.Description, FolderName = a.Folder.FolderName, Access = a.Access, UploadTime = a.DateCreated})
                        .OrderBy(a => a.UploadTime)
                        .Take(offset + 10)
                        .Skip(offset)
                        .ToList();
                }
                else
                {
                    return pictures = context.Pictures
                       .Where(a => a.Owner == user && a.Folder.FolderName == folderName)
                       .Include(a => a.Folder)
                        .Select(a => new ResponsePicture { Id = a.Id, Description = a.Description, FolderName = a.Folder.FolderName, Access = a.Access, UploadTime = a.DateCreated })
                       .OrderBy(a => a.UploadTime)
                       .Take(offset + 10)
                       .Skip(offset)
                       .ToList();
                }
            }
            catch (Exception)
            {
                return pictures;
            }
        }

        public List<ResponsePicture> GetPicturesFromOther(User user, User otherUser, int offset, string folderName)
        {
            bool followed = null != context.Followers
                .Where(a => a.FollowerUser == user &&
                            a.Followed == otherUser &&
                            a.Accepted == true)
                .FirstOrDefault();
            List<ResponsePicture> pictures = new List<ResponsePicture>();
            if (followed)
            {
                try
                {
                    if (folderName == null)
                    {
                        return pictures = context.Pictures
                            .Where(a => a.Owner == otherUser &&
                                  (a.Access == AccessType.PublicForEveryone ||
                                   a.Access == AccessType.PublicForFollowers))
                            .Include(a => a.Folder)
                            .Select(a => new ResponsePicture { Id = a.Id, Description = a.Description, FolderName = a.Folder.FolderName, Access = a.Access, UploadTime = a.DateCreated })
                            .OrderBy(a => a.UploadTime)
                            .Take(offset + 10)
                            .Skip(offset)
                            .ToList();
                    }
                    else
                    {
                        return pictures = context.Pictures
                           .Where(a => a.Owner == otherUser &&
                                  a.Folder.FolderName == folderName &&
                                 (a.Folder.Access == AccessType.PublicForEveryone ||
                                  a.Folder.Access == AccessType.PublicForFollowers) &&
                                 (a.Access == AccessType.PublicForEveryone ||
                                  a.Access == AccessType.PublicForFollowers))
                           .Include(a => a.Folder)
                           .Select(a => new ResponsePicture { Id = a.Id, Description = a.Description, FolderName = a.Folder.FolderName, Access = a.Access, UploadTime = a.DateCreated })
                           .OrderBy(a => a.UploadTime)
                           .Take(offset + 10)
                           .Skip(offset)
                           .ToList();
                    }
                }

                catch (Exception)
                {
                    return pictures;
                }
            }
            else
            {
                try
                {
                    if (folderName == null)
                    {
                        return pictures = context.Pictures
                            .Where(a => a.Owner == otherUser &&
                                  (a.Access == AccessType.PublicForEveryone))
                            .Include(a => a.Folder)
                            .Select(a => new ResponsePicture { Id = a.Id, Description = a.Description, FolderName = a.Folder.FolderName, Access = a.Access, UploadTime = a.DateCreated })
                            .OrderBy(a => a.UploadTime)
                            .Take(offset + 10)
                            .Skip(offset)
                            .ToList();
                    }
                    else
                    {
                        return pictures = context.Pictures
                           .Where(a => a.Owner == otherUser &&
                                  a.Folder.FolderName == folderName &&
                                  a.Folder.Access == AccessType.PublicForEveryone &&
                                  a.Access == AccessType.PublicForEveryone )
                           .Include(a => a.Folder)
                           .Select(a => new ResponsePicture { Id = a.Id, Description = a.Description, FolderName = a.Folder.FolderName, Access = a.Access, UploadTime = a.DateCreated })
                           .OrderBy(a => a.UploadTime)
                           .Take(offset + 10)
                           .Skip(offset)
                           .ToList();
                    }
                }

                catch (Exception)
                {
                    return pictures;
                }
            }
        }
    }
}
