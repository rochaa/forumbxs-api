using System;

namespace ForumBXS.Shared
{
    public static class Settings
    {
        public static string ForumBXSDatabaseName = Environment.GetEnvironmentVariable("FORUMBXS_DATABASE_NAME");
    }
}