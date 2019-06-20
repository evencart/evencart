namespace EvenCart.Infrastructure.Bundle
{
    public interface IMinifier
    {
        string MinifyHtml(string html);
    }
}