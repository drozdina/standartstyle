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
                        string filenameForSave = String.Empty;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            filenameForSave = ImagesLogic.generateFilename(fname);
                        }
                        else
                        {
                            filenameForSave = ImagesLogic.generateFilename(file.FileName);
                        }
                        var location = Server.MapPath(Configuration.UploadDirectory);
                        if (!Directory.Exists(location))
                        {
                            Directory.CreateDirectory(location);
                        }

                        ImageModel newImage = new ImageModel
                        {
                            ImageCode = -1,
                            Name = filenameForSave,
                            Path = Configuration.UploadDirectory
                        };
                        images.Add(newImage);

                        filenameForSave = Path.Combine(location, filenameForSave);
                        file.SaveAs(filenameForSave);
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