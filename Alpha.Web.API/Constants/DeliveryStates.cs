namespace Alpha.Web.API.Constants
{
    /// <summary>
    /// This class constant values represent the message status, being delivered or pending, and its
    /// respective codes to be saved in database
    /// </summary>
    public class DeliveryStates
    {
        public const string Delivered = "Delivered";
        public const string Pending = "Pending";
        public const short DeliveredCode = 0;
        public const short PendingCode = 1;
    }
}
