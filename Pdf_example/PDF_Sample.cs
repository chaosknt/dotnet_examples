using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDF_sample
{
    public static class PDF_Sample
    {
        public static MemoryStream CreatePdf(byte[] imageBytes, string code)
        {
            var fontSizeBulto = 15;
            MemoryStream ms = new MemoryStream();
            PageSize pageSize = PageSize.A5;
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document doc = new Document(pdfDoc, pageSize);

            Image image = new Image(ImageDataFactory.Create(imageBytes));
            image.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            doc.Add(image);
            Paragraph codeParagraph = new Paragraph(code).SetFontSize(fontSizeBulto).SetTextAlignment(TextAlignment.CENTER);
            doc.Add(codeParagraph);

            doc.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return ms;
        }
    }
}
