using System.Web;
using System.Web.Optimization;

namespace Standartstyle
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                "~/Scripts/fileinput.js",
                "~/Scripts/locales/ru.js",
                "~/Scripts/plugins/piexif.js",
                "~/Scripts/plugins/purify.js",
                "~/Scripts/plugins/sortable.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/Admin.css"));

            bundles.Add(new StyleBundle("~/Content/fileuploader").Include(
                "~/Content/bootstrap-fileinput/css/fileinput-rtl.css",
                "~/Content/bootstrap-fileinput/css/fileinput.css"
                ));
        }
    }
}
