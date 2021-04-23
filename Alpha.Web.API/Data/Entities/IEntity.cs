namespace Alpha.Web.API.Data.Entities
{
    public interface IEntity<Type>
    {
        Type Id { get; set; }
    }
}
