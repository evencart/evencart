namespace EvenCart.Services.Pdf
{
    public interface IPdfService
    {
        byte[] GetPdfBytes(string html);
    }
}