namespace Shared.Core.Model;

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}
public interface IEntity : IVersion
{
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
