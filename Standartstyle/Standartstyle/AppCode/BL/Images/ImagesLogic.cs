﻿using Standartstyle.AppCode.DAL.Model;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.BL.Images
{
    public class ImagesLogic
    {
        #region Variables
        private static string _DASH = "-";
        private static string _UNDERSCORE = "_";
        private static string _SPACE = " ";
        #endregion

        #region Static methods
        public static ImageModel generateFileModel(string filename)
        {
            filename = filename.Replace(_SPACE, _UNDERSCORE);
            string filenameForSave = DateTime.Now.ToFileTimeUtc().ToString() + _DASH;
            var fileLexemes = filename.Split('.');
            int extIdx = fileLexemes.Length - 1;
            var extension = fileLexemes[extIdx];
            for (var idx = 0; idx < extIdx; idx++)
                filenameForSave += fileLexemes[idx];

            var model = new ImageModel
            {
                ImageCode = -1,
                Name = filenameForSave,
                Extension = extension,
                Path = Configuration.UploadDirectory
            };
            return model;
        }
        #endregion

        #region Private methods

        private String GenerateRelativePathForGoodImages(GoodModel goodModel)
        {
            var path = Configuration.FileDirectory + Path.AltDirectorySeparatorChar
                + goodModel.SelectedCategoryCode + Path.AltDirectorySeparatorChar
                + goodModel.GoodCode + Path.AltDirectorySeparatorChar;

            return path;
        }

        private String CreateLocationForGoodImages(String relativeLocation)
        {
            String absolutePath = String.Empty;
            absolutePath = System.Web.Hosting.HostingEnvironment.MapPath(relativeLocation);
            if (!Directory.Exists(absolutePath))
            {
                if (!Directory.CreateDirectory(absolutePath).Exists)
                {
                    absolutePath = String.Empty;
                }
            }

            return absolutePath;
        }

        private Boolean MoveGoodImageToFileLocation(IMAGE imageModel, ImageModel image)
        {
            var isMoved = false;
            var absoluteDestinationLocation = CreateLocationForGoodImages(imageModel.LOCATION);
            if (!absoluteDestinationLocation.Equals(String.Empty))
            {
                var absoluteSourceLocation = CreateLocationForGoodImages(Configuration.UploadDirectory);
                var sourceLocation = Path.Combine(absoluteSourceLocation, image.GetFullFilenameForTemporaryFile());
                var destinationLocation = Path.Combine(absoluteDestinationLocation, imageModel.IMAGECODE.ToString() + ImageModel._DOT + image.Extension);
                if (File.Exists(sourceLocation))
                {
                    File.Move(sourceLocation, destinationLocation);
                    isMoved = true;
                }
            }
            return isMoved;
        }
        #endregion

        #region CRUD logic
        public List<string> CreateNewImage(GoodModel goodModel)
        {
            List<string> uncopiedImages = new List<string>();
            var relativeLocation = GenerateRelativePathForGoodImages(goodModel);
            foreach (var image in goodModel.NewImages)
            {
                using (GeneralRepository repo = new GeneralRepository())
                {
                    IMAGE newImageModel = new IMAGE
                    {
                        GOODCODE = goodModel.GoodCode,
                        NAME = image.Name,
                        UPLOAD_DATE = DateTime.Now,
                        LOCATION = relativeLocation,
                        IS_MAIN = image.MainImageFlag,
                        EXTENSION = image.Extension
                    };

                    repo.ImageRepository.Create(newImageModel);
                    var isCopied = MoveGoodImageToFileLocation(newImageModel, image);
                    if (!isCopied)
                    {
                        uncopiedImages.Add(newImageModel.NAME);
                    }
                }
            }
            return uncopiedImages;
        }

        public GoodModel SelectImagesForModel(GoodModel goodModel)
        {
            List<ImageModel> images = new List<ImageModel>();
            using (GeneralRepository repo = new GeneralRepository())
            {
                var imagesFromDB = repo.ImageRepository.Get(image => image.GOODCODE == goodModel.GoodCode).ToList();
                foreach (var imageFromDB in imagesFromDB)
                {
                    var image = new ImageModel()
                    {
                        ImageCode = imageFromDB.IMAGECODE,
                        MainImageFlag = imageFromDB.IS_MAIN.Value,
                        Name = imageFromDB.NAME,
                        Extension = imageFromDB.EXTENSION,
                        Path = imageFromDB.LOCATION
                    };

                    images.Add(image);
                }

                ((List<ImageModel>)goodModel.Images).AddRange(images);
            }
            return goodModel;
        }
        #endregion

    }
}