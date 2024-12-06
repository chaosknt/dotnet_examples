using IronBarCode;

namespace BarCode
{
    public static class BarCode_Sample
    {
        public static byte[] Create(string text, BarcodeWriterEncoding type = BarcodeWriterEncoding.Code128)
        {
            var myBarcode = BarcodeWriter.CreateBarcode(text, type, 300, 50);
            return myBarcode.ToJpegBinaryData();
        } 
    }
}