namespace EvenCart.Services.Dashboard
{
    public interface IDashboardService
    {
        void GetNumbers(out int totalUsers, out int totalOrders, out int pendingOrders, out decimal totalPayments);


    }
}