namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// The status of a refund request
    /// </summary>
    public enum ReturnRequestStatus
    {
        /// <summary>
        /// The return is pending
        /// </summary>
        Pending = 0,
        /// <summary>
        /// The return has been authorized
        /// </summary>
        Authorized = 10,
        /// <summary>
        /// The return process has been initiated
        /// </summary>
        Initiated = 20,
        /// <summary>
        /// The items have been scheduled for return pickup
        /// </summary>
        PickupScheduled = 30,
        /// <summary>
        /// The items have been picked up and are on way back
        /// </summary>
        InTransit = 40,
        /// <summary>
        /// The items have been received at the warehouse
        /// </summary>
        ReturnReceived = 50,
        /// <summary>
        /// The return is complete
        /// </summary>
        Complete = 60,
        /// <summary>
        /// The return has been cancelled
        /// </summary>
        Cancelled = 70,
        /// <summary>
        /// The return has been rejected
        /// </summary>
        Rejected = 80,
        /// <summary>
        /// The return item has been repaired
        /// </summary>
        Repaired = 90
    }
}