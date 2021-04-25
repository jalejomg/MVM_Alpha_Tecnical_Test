namespace Alpha.Web.API.Constants
{
    /// <summary>
    /// This class constant values, represents the state of a entity that will be saved in database.
    /// With this, we do not delete data but change its state
    /// </summary>
    public class EntityStatus
    {
        public const string Deleted = "Deleted";
        public const string Exists = "Persistent";
        public const bool DeletedValue = false;
        public const bool ExistsValue = true;
    }
}
