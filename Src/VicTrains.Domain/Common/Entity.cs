namespace VicTrains.Domain.Common
{
    public abstract class Entity
    {
        // defining identity for domain objets
        public int Id { get; protected set; }
    }
}
