using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        string pdfFilePath = "D:\\01-opt.pdf";
        byte[] bytes = File.ReadAllBytes(pdfFilePath);

        MemoryStream ms = new MemoryStream(bytes);

        byte[] fileData = null;
        using (var binaryReader = new BinaryReader(ms))
        {
            fileData = binaryReader.ReadBytes(ms.Capacity);
        }
        ImageConverter imageConverter = new System.Drawing.ImageConverter();
        System.Drawing.Image image = imageConverter.ConvertFrom(fileData) as System.Drawing.Image;
        image.Save("D:\\test.png", System.Drawing.Imaging.ImageFormat.Png);
    }

    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        return ms.ToArray();
    }

    public Image byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Image returnImage = Image.FromStream(ms);
        return returnImage;
    }
}