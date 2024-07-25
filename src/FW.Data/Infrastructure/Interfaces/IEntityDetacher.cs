namespace FW.Data.Infrastructure.Interfaces
{
    public interface IEntityDetacher<T>
    {
        void Detach(T entity);
    }
}
