namespace Common.DataAccess
{
    public interface IEntity<K>
    {
        K? Id { get; set; }
    }
}