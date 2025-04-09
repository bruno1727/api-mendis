namespace ApiMendis
{
    public static class RedisIdMapper
    {
        public static string Map(object entity)
        {
            if (entity.GetType() == typeof(User.User)) return ((User.User)entity).Email;

            return "";
        }
    }
}
