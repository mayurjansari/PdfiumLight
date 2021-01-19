using PdfiumLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfiumLight
{
    public static class XPdfImage
    {
        public static IEnumerable<Image> GetImages(this PdfDocument doc, int dpi = 300)
        {
            var images = new List<Image>();
            for (int i = 0; i < doc.PageCount(); i++)
            {
                PdfPage page = doc.GetPage(i);
                images.Add(page.GetImage(dpi));
            }
            return images;
        }

        public static Image GetImage(this PdfDocument doc, int dpi = 300, int pageNumber = 0)
        {
            PdfPage page = doc.GetPage(pageNumber);
            return page.GetImage(dpi);
        }

        public static Image GetImage(this PdfPage page, int dpi = 300)
        {
            int width = (int)Math.Round(page.Width * dpi / 72f);
            int height = (int)Math.Round(page.Height * dpi / 72f);
            return page.Render(width, height);
        }
    }
}
public static partial class XPdfImage
{
    public static Image GetImage(string Pdf, string password = null, int dpi = 300, int pageNumber = 0)
    {
        using (PdfDocument doc = new PdfDocument(Pdf, password))
            return PdfiumLight.XPdfImage.GetImage(doc, dpi, pageNumber);
    }

    public static Image GetImage(Stream Pdf, string password = null, int dpi = 300, int pageNumber = 0)
    {
        using (PdfDocument doc = new PdfDocument(Pdf, password))
            return PdfiumLight.XPdfImage.GetImage(doc, dpi, pageNumber);
    }

    public static Image GetImage(byte[] Pdf, string password = null, int dpi = 300, int pageNumber = 0)
    {
        using (MemoryStream ms = new MemoryStream(Pdf))
            return GetImage(ms, password, dpi, pageNumber);
    }

    public static IEnumerable<Image> GetImages(string Pdf, string password = null, int dpi = 300)
    {
        using (PdfDocument doc = new PdfDocument(Pdf, password))
            return PdfiumLight.XPdfImage.GetImages(doc, dpi);
    }

    public static IEnumerable<Image> GetImages(Stream Pdf, string password = null, int dpi = 300)
    {
        using (PdfDocument doc = new PdfDocument(Pdf, password))
            return PdfiumLight.XPdfImage.GetImages(doc, dpi);
    }

    public static IEnumerable<Image> GetImages(byte[] Pdf, string password = null, int dpi = 300)
    {
        using (MemoryStream ms = new MemoryStream(Pdf))
            return GetImages(ms, password, dpi);
    }

    public static int MoveImageIndex(this IEnumerable<Image> list, int index, int direction)
    {
        if (list == null || list.Count() < index)
            return index;

        int newIndex = index + direction;

        if (newIndex < 0 || newIndex >= list.Count())
            return 0;

        return newIndex;
    }

    public static int NextImage(this IEnumerable<Image> list, int index)
    {
        return MoveImageIndex(list, index, 1);
    }

    public static int PreviousImage(this IEnumerable<Image> list, int index)
    {
        return MoveImageIndex(list, index, -1);
    }
}

