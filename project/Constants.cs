namespace AMAPP.API
{
    public static class Constants
    {
        // Order Status
        public enum OrderStatus
        {
            Pending = 0,
            Confirmed = 1,
            Processing = 2,
            ReadyForDelivery = 3,
            Delivered = 4,
            Completed = 5,
            Cancelled = 6
        }

        public enum OrderItemStatus
        {
            Pending = 0,
            Confirmed = 1,
            Processing = 2,
            Ready = 3,
            Delivered = 4,
            Cancelled = 5
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

        public enum  DeliveryMethod
        {
            Pickup = 0,
            HomeDelivery = 1
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
