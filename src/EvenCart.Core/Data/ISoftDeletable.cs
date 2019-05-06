namespace EvenCart.Core.Data
{
    /// <summary>
    /// Interface to specify entities which are soft deletable
    /// </summary>
    public interface ISoftDeletable
    {
        bool Deleted { get; set; }
    }
}