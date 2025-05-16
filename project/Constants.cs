namespace AMAPP.API
{
    public static class Constants
    {
        // Order Status
        public enum OrderStatus
        {
            Created,
            Confirmed,
            Processing,
            Ready,
            Delivered,
            Cancelled
        }

        // Payment Method
        public enum PaymentMethod
        {
            Cash,
            BankTransfer,
            CreditCard,
            DebitCard,
            MBWay,
            Other
        }

        // Payment Mode
        public enum PaymentMode
        {
            Full,
            Installment
        }

        // Payment Status
        public enum PaymentStatus
        {
            Pending,
            Processing,
            Completed,
            Failed,
            Refunded,
            Cancelled
        }

        // Delivery Status
        public enum DeliveryStatus
        {
            Scheduled,
            InTransit,
            Delivered,
            Failed,
            Cancelled
        }

        // Resource Status (used for various entities)
        public enum ResourceStatus
        {
            Active,
            Inactive,
            Deleted
        }

        public enum DeliveryUnit { Unit, Kg, Grams, Liters }


        // File size limits
        public const int MaxPhotoSizeInBytes = 5 * 1024 * 1024; // 5 MB

        // Valid photo formats
        public static readonly string[] ValidPhotoFormats = { ".jpg", ".jpeg", ".png" };
    }
    
    public enum SubscriptionDuration    {  Trimestral, Semestral    }
    public enum ResourceStatus    {  Ativo,     Inativo}
    
    public static class SubscriptionDurationExtensions
    {
        public static readonly Dictionary<SubscriptionDuration, int> DurationDays = new()
        {
            { SubscriptionDuration.Trimestral, 90 },
            { SubscriptionDuration.Semestral, 180 }
        };

        public static int GetDurationDays(this SubscriptionDuration duration)
        {
            return DurationDays[duration];
        }
    }
}
