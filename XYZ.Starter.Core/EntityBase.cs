using System.ComponentModel.DataAnnotations;
using XYZ.Starter.Core.Interfaces.Classes;

namespace XYZ.Starter.Core
{
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        public int Id { get ; set; }
    }
}
