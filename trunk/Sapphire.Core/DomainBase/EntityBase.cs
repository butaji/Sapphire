using Sapphire.Core.Validation;

namespace Sapphire.Core.DomainBase
{
  public class EntityBase : IEntity
  {
    public string Title { get; set; }

    #region Overrides of Object

    public static bool operator ==(EntityBase base1,
                                   EntityBase base2)
    {
      if ((object)base1 == null && (object)base2 == null)
      {
        return true;
      }

      if ((object)base1 == null || (object)base2 == null)
      {
        return false;
      }

      return base1.Id == base2.Id;
    }

    public static bool operator !=(EntityBase base1,
                                   EntityBase base2)
    {
      return (!(base1 == base2));
    }

    public override bool Equals(object entity)
    {
      return entity != null
             && entity is EntityBase
             && this == (EntityBase)entity;
    }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }

    #endregion

    #region IEntity Members

    public int Id { get; set; }

    #endregion

    #region Validation

    public bool Validate(IEntityValidator<EntityBase> validator)
    {
      return validator.IsValid(this);
    }

    #endregion
  }
}