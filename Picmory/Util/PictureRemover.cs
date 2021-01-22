using Microsoft.AspNetCore.Hosting;
using Picmory.Models;
using Picmory.Models.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Util
{
    public class PictureRemover
    {
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IPictureRepository pictureRepository;

        public PictureRemover()
        {
        }
        public PictureRemover(IWebHostEnvironment hostEnvironment, IPictureRepository pictureRepository)
        {
            this.pictureRepository = pictureRepository;
            _hostEnv = hostEnvironment;
        }

        public bool DeletePicturesForFolder(User user, string folderName)
        {
            try
            {
                List<string> IdList = pictureRepository.GetAllPicturesInFolder(user, folderName);
                foreach (string Id in IdList)
                {
                    int.TryParse(Id.Split(".")[0], out int id);
                    pictureRepository.DeletePicture(id);
                    System.IO.File.Delete(_hostEnv.WebRootPath + "/" + Id.ToString());
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool DeletePictures (User user)
        {
            try
            {
                List<string> IdList = pictureRepository.GetAllPictures(user);
            foreach (string Id in IdList)
            {
                System.IO.File.Delete(_hostEnv.WebRootPath + "/" + Id.ToString());
            }
            return true;
            }
            catch (Exception) { return false; }
        }
    }
}
