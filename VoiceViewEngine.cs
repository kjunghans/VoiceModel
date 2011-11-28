using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace VoiceModel
{
    public class VoiceViewEngine : RazorViewEngine
    {
        public VoiceViewEngine()
        {
            ViewLocationFormats = new[] {
            "~/Views/{1}/{0}.aspx",
            "~/Views/{1}/{0}.ascx",
            "~/Views/Shared/{0}.aspx",
            "~/Views/Shared/{0}.ascx",
            "~/Views/{1}/{0}.cshtml",
            "~/Views/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml",
            "~/Views/Shared/{0}.vbhtml",
            "~/tmp/Views/{0}.cshtml",
            "~/tmp/Views/{0}.vbhtml"
        };
        PartialViewLocationFormats = ViewLocationFormats;

        DumpOutViews();
    }

        private static bool viewFileType(string fname)
        {
            return fname.EndsWith(".cshtml") || fname.EndsWith(".config");
        }

    private static void DumpOutViews()
    {
        IEnumerable<string> resources = typeof(VoiceViewEngine).Assembly.GetManifestResourceNames().Where(name => viewFileType(name));
        foreach (string res in resources) 
        { 
            DumpOutView(res); 
        }
    }

    private static void SaveStreamToFile(string fileFullPath, Stream stream)
    {
        if (stream.Length == 0) return;

        // Create a FileStream object to write a stream to a file
        using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
        {
            // Fill the bytes[] array with the stream data
            byte[] bytesInStream = new byte[stream.Length];
            stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

            // Use FileStream object to write to the specified file
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
        }
    }

    private static void DumpOutView(string res)
    {
        string rootPath = HttpContext.Current.Server.MapPath("~/tmp/Views/");
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        Stream resStream = typeof(VoiceViewEngine).Assembly.GetManifestResourceStream(res);
        int lastSeparatorIdx = res.LastIndexOf('.');
        string extension = res.Substring(lastSeparatorIdx + 1);
        res = res.Substring(0, lastSeparatorIdx);
        lastSeparatorIdx = res.LastIndexOf('.');
        string fileName = res.Substring(lastSeparatorIdx + 1);

        SaveStreamToFile(rootPath + fileName + "." + extension, resStream);
    }
    }
}
