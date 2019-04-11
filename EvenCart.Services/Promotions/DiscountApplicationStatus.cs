namespace EvenCart.Services.Promotions
{
    public enum DiscountApplicationStatus
    {
        Success = 1,
        AlreadyApplied = 2,
        Expired = 3,
        NotEligibleForCart = 4,
        InvalidCode = 5
    }
}