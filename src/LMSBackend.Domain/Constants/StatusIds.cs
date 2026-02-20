namespace LMSBackend.Domain.Constants
{
    public static class StatusIds
    {
        public static readonly Guid UNGENERATED =
            Guid.Parse("00000001-0000-0000-0000-000000000000");

        public static readonly Guid UNPOSTED =
            Guid.Parse("00000002-0000-0000-0000-000000000000");

        public static readonly Guid POSTED =
            Guid.Parse("00000003-0000-0000-0000-000000000000");

        public static readonly Guid HOLD =
            Guid.Parse("00000004-0000-0000-0000-000000000000");

        public static readonly Guid CANCELLED =
            Guid.Parse("00000005-0000-0000-0000-000000000000");
    }
}