using System.ComponentModel;

namespace Petronas.SampleApp.Common
{
    public static class EnumCommon
    {
        public enum SortDirection
        {
            Ascending,
            Descending
        }

        public enum ApplicationTypes
        {
            JavaScript = 0,
            NativeConfidential = 1
        }

        public enum EnumTimeFormat
        {
            [Description("12-hours (e.g. 9:00pm)")]
            TwelveHours = 12,

            [Description("24-hours (e.g. 21:00)")]
            TwentyFourHours = 24
        }

        public enum EnumTimeSlotInterval
        {
            [Description("5 Minutes")]
            FiveMinutes = 5,

            [Description("10 Minutes")]
            TenMinutes = 10,

            [Description("15 Minutes")]
            FifteenMinutes = 15,

            [Description("30 Minutes")]
            ThirtyMinutes = 30
        }

        public enum EnumDefaultCalendarViewType
        {
            [Description("Day")]
            Day = 1,

            [Description("Week")]
            Week = 7
        }

        public enum EnumWeekStartDay
        {
            [Description("Sunday")]
            Sunday = 1,

            [Description("Monday")]
            Monday,

            [Description("Tuesday")]
            Tuesday,

            [Description("Wednesday")]
            Wednesday,

            [Description("Thursday")]
            Thursday,

            [Description("Friday")]
            Friday,

            [Description("Saturday")]
            Saturday
        }

        public enum EnumDiscountType
        {
            [Description("% percentage")]
            Percentage = 0,

            [Description("amount")]
            Amount = 1,
        }

        public enum EnumSendNotificationBy
        {
            [Description("Don't send notifications")]
            DontSendNotifications = 0,

            [Description("Email")]
            Email = 1,
        }

        public enum EnumGender
        {
            [Description("Male")]
            Male = 1,

            [Description("Female")]
            Female = 2,
        }

        public enum EnumAppointmentStatus
        {
            New = 0,
            Confirmed,
            Started,
            Arrived,
            NoShow,
            Completed
        }

        public enum EnumInvoiceStatus
        {
            Unpaid = 0,
            Paid = 1
        }

        public enum EnumUserPermission
        {
            NoAccess = 0,
            Low = 1,
            Medium = 2,
            High = 3
        }

        public enum EnumExtraTimeType
        {
            NoExtraTime = 0,
            ProcessingTimeAfter = 1,
            BlockedTimeAfter = 2
        }

        public enum EnumServiceAvailableFor
        {
            Everyone = 0,
            MalesOnly = 1,
            FemalesOnly = 2
        }
    }
}
