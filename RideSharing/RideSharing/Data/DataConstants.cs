namespace RideSharing.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int FullNameMinLength = 4;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public class Vehicle
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 20;
            public const int YearMinValue = 1980;
            public const int YearMaxValue = 2030;
        }

        public class VehicleType
        {
            public const int TypeMaxLength = 25;
        }

        public class Driver
        {
            public const int NameMinLength = 4;
            public const int NameMaxLength = 20;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
    }
}
