﻿using Standartstyle.AppCode.BL;
using Standartstyle.AppCode.BL.Images;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Controllers
{
    public class ImageController : Controller
    {

        #region Variables
        private ImagesLogic imagesLogic = new ImagesLogic();
        #endregion

        #region Variables
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<ImageModel> images = new List<ImageModel>();
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        ImageModel newImage = null;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(Path.DirectorySeparatorChar);
                            fname = testfiles[testfiles.Length - 1];
                            newImage = ImagesLogic.generateFileModel(fname);
                        }
                        else
                        {
                            newImage = ImagesLogic.generateFileModel(file.FileName);
                        }
                        if (newImage != null)
                        {
                            var location = Server.MapPath(newImage.Path);
                            if (!Directory.Exists(location))
                            {
                                Directory.CreateDirectory(location);
                            }
                            images.Add(newImage);
                            string filenameForSave = String.Empty;
                            filenameForSave = Path.Combine(location, newImage.GetFullFilenameWithName());
                            file.SaveAs(filenameForSave);
                        }
                    }
                    return Json(new
                    {
                        code = HttpStatusCode.OK,
                        message = "File Uploaded Successfully!",
                        files = images
                    });
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpPost]
        public JsonResult RemoveGoodImage(int imageCode)
        {
            var result = new List<object>();
            var isDeleted = imagesLogic.RemoveGoodImage(imageCode);
            if (isDeleted)
            {
                result.Add(new { status = true });
                result.Add(new { message = "Изображение удалено!" });
            }
            else
            {
                result.Add(new { status = false });
                result.Add(new { message = "Не удалось удалить изображение!" });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List(IEnumerable<ImageModel> images)
        {
            return PartialView("_List", images);
        }
        #endregion
    }
}