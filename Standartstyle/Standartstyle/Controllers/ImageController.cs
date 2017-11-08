using Standartstyle.AppCode.BL;
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
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
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
                            filenameForSave = Path.Combine(location, newImage.GetFullFilename());
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

        public ActionResult List(IEnumerable<ImageModel> images)
        {
            return PartialView("_List", images);
        }
    }
}