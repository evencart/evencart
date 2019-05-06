namespace EvenCart.Services.Tokens
{
    public interface ITokenGenerator
    {
        string MakeToken(TemplateToken templateToken);
    }
}