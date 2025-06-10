namespace AMAPP.API
{
    public static class Constants
    {
        // User Roles
        public enum UserRole
        {
            Producer = 1,
            CoProducer = 2,
            Administrator = 3,
            Amap = 4
        }

        // Role names mapping
        public static class RoleNames
        {
            public const string Producer = "PROD";
            public const string CoProducer = "COPR";
            public const string Administrator = "ADMIN";
            public const string Amap = "AMAP";
        }

        // Helper method to convert enum to role name
        public static string GetRoleName(UserRole role)
        {
            return role switch
            {
                UserRole.Producer => RoleNames.Producer,
                UserRole.CoProducer => RoleNames.CoProducer,
                UserRole.Administrator => RoleNames.Administrator,
                UserRole.Amap => RoleNames.Amap,
                _ => throw new ArgumentException("Invalid role")
            };
        }

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
            Scheduled = 0,
            InTransit = 1,
            Delivered = 2,
            Failed = 3,
            Cancelled = 4
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
