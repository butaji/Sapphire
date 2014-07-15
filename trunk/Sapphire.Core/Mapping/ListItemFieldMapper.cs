using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Sapphire.Core.Mapping
{
  [SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
  [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
  public class ListItemFieldMapper<TEntity> where TEntity : new()
  {
    readonly Collection<FieldToEntityPropertyMapping> _fieldMappings = new Collection<FieldToEntityPropertyMapping>();

    /// <summary>
    /// Create an entity, and fill the mapped properties from the specified <see cref="SPListItem"/>.
    /// </summary>
    /// <param name="item">The listitem to use to fill the entities properties. </param>
    /// <returns>The created and populated entity. </returns>
    [SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
    [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
    public TEntity CreateEntity(SPListItem item)
    {
      TEntity entity = new TEntity();
      Type entityType = typeof(TEntity);

      foreach (FieldToEntityPropertyMapping fieldmapping in this._fieldMappings)
      {
        fieldmapping.Map(item, entityType, ref entity);
      }
      return entity;
    }

    /// <summary>
    /// The list of field mappings that are used by the ListItemFieldMapper class.
    /// </summary>
    public Collection<FieldToEntityPropertyMapping> FieldMappings
    {
      get { return this._fieldMappings; }
    }

    /// <summary>
    /// Fill a SPListItem's properties based on the values in an entity.
    /// </summary>
    /// <param name="spListItem"></param>
    /// <param name="entity"></param>
    [SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
    [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
    public void FillSPListItemFromEntity(SPListItem spListItem, TEntity entity)
    {
      Type entityType = typeof(TEntity);
      foreach (FieldToEntityPropertyMapping fieldmapping in this._fieldMappings)
      {
        fieldmapping.Map(entityType, entity, ref spListItem);
      }
    }

    /// <summary>
    /// Add a mapping between a field in an SPListItem and a property in the entity.
    /// </summary>
    /// <param name="fieldId"></param>
    /// <param name="expression"></param>
    public void AddMapping<TValue>(Guid fieldId, Expression<Func<TEntity, TValue>> expression)
    {
      this._fieldMappings.Add(new FieldToEntityPropertyMapping
      {
        EntityPropertyName = ((MemberExpression)expression.Body).Member.Name,
        ListFieldId = fieldId
      });
    }

    /// <summary>
    /// Add a mapping between a field in an SPListItem and a property in the entity with converter.
    /// </summary>
    /// <param name="fieldId"></param>
    /// <param name="expression"></param>
    /// <param name="convertToProperty"></param>
    public void AddMapping<TField, TProperty>(Guid fieldId, Expression<Func<TEntity, TProperty>> expression, Expression<Func<TField, TProperty>> convertToProperty)
    {
      _fieldMappings.Add(
        new FieldToEntityPropertyWithConvertMapping<TField, TProperty>
        {
          EntityPropertyName = ((MemberExpression)expression.Body).Member.Name,
          ListFieldId = fieldId,
          ConvertToProperty = convertToProperty
        });
    }

    /// <summary>
    /// Add a mapping between a field in an SPListItem and a property in the entity with bi converters.
    /// </summary>
    /// <param name="fieldId"></param>
    /// <param name="expression"></param>
    /// <param name="convertToProperty"></param>
    /// <param name="convertToField"></param>
    public void AddMapping<TProperty, TField>(Guid fieldId, Expression<Func<TEntity, TProperty>> expression, Expression<Func<TField, TProperty>> convertToProperty, Expression<Func<TProperty, TField>> convertToField)
    {
      _fieldMappings.Add(
        new FieldToEntityPropertyWithBiConvertMapping<TField, TProperty>
        {
          EntityPropertyName = ((MemberExpression)expression.Body).Member.Name,
          ListFieldId = fieldId,
          ConvertToProperty = convertToProperty,
          ConvertToField = convertToField
        });
    }
  }

  public class FieldToEntityPropertyMapping
  {
    /// <summary>
    /// The guid that corresponds to the Id of the field.
    /// </summary>
    public Guid ListFieldId { get; set; }

    /// <summary>
    /// The name of the property on the entity.
    /// </summary>
    public string EntityPropertyName { get; set; }

    /// <summary>
    /// Map SPListItem field to entity property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <param name="entityType"></param>
    /// <param name="entity"></param>
    internal virtual void Map<T>(SPListItem item, Type entityType, ref T entity)
    {
      PropertyInfo propertyInfo = GetPropertyInfo(item, entityType);
      EnsureListFieldId(item, entityType);
      object val = item[this.ListFieldId];
      propertyInfo.SetValue(entity, val, null);
    }

    /// <summary>
    /// Map entity property to SPListItem field
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entityType"></param>
    /// <param name="entity"></param>
    /// <param name="item"></param>
    internal virtual void Map<T>(Type entityType, T entity, ref SPListItem item)
    {
      PropertyInfo propertyInfo = GetPropertyInfo(item, entityType);
      EnsureListFieldId(item, entityType);
      if (!item.Fields[ListFieldId].ReadOnlyField)
        item[this.ListFieldId] = propertyInfo.GetValue(entity, null);
    }

    /// <exception cref="ListItemFieldMappingException"><c>ListItemFieldMappingException</c>.</exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
    protected PropertyInfo GetPropertyInfo(SPListItem item, Type entityType)
    {
      PropertyInfo propertyInfo = entityType.GetProperty(this.EntityPropertyName);
      if (propertyInfo == null)
      {
        string errorMessage = string.Format(CultureInfo.CurrentCulture, "Type '{0}' does not have a property '{1}' which was mapped to FieldID: '{2}' for SPListItem '{3}'."
                                            , entityType.FullName
                                            , this.EntityPropertyName
                                            , this.ListFieldId
                                            , item.Name);
        throw new ListItemFieldMappingException(errorMessage);
      }
      return propertyInfo;
    }

    /// <exception cref="ListItemFieldMappingException"><c>ListItemFieldMappingException</c>.</exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ensuredField"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
    protected void EnsureListFieldId(SPListItem item, Type entityType)
    {
      try
      {
        var ensuredField = item.Fields[ListFieldId];
      }
      catch (ArgumentException argumentException)
      {
        string errorMessage = string.Format(CultureInfo.CurrentCulture
                                            , "SPListItem '{0}' does not have a field with Id '{1}' which was mapped to property: '{2}' for entity '{3}'."
                                            , item.Name
                                            , ListFieldId
                                            , EntityPropertyName
                                            , entityType.FullName);

        throw new ListItemFieldMappingException(errorMessage, argumentException);
      }
    }
  }

  internal class FieldToEntityPropertyWithConvertMapping<TField, TProperty> : FieldToEntityPropertyMapping
  {
    /// <summary>
    /// The lambda what convert SPListItem field to Entity property.
    /// </summary>
    internal Expression<Func<TField, TProperty>> ConvertToProperty { get; set; }

    internal override void Map<T>(SPListItem item, Type entityType, ref T entity)
    {
      PropertyInfo propertyInfo = GetPropertyInfo(item, entityType);
      EnsureListFieldId(item, entityType);
      Func<TField, TProperty> convertFunction = ConvertToProperty.Compile();
      object val = convertFunction((TField)item[ListFieldId]);
      propertyInfo.SetValue(entity, val, null);
    }
  }

  internal class FieldToEntityPropertyWithBiConvertMapping<TField, TProperty> : FieldToEntityPropertyWithConvertMapping<TField, TProperty>
  {
    /// <summary>
    /// The lambda what convert Entity property to SPListItem field.
    /// </summary>
    internal Expression<Func<TProperty, TField>> ConvertToField { get; set; }

    internal override void Map<T>(Type entityType, T entity, ref SPListItem item)
    {
      PropertyInfo propertyInfo = GetPropertyInfo(item, entityType);
      EnsureListFieldId(item, entityType);
      Func<TProperty, TField> convertFunction = ConvertToField.Compile();
      item[ListFieldId] = convertFunction((TProperty)propertyInfo.GetValue(entity, null));
    }
  }

  /// <summary>
  /// Exception that can occur when mapping a field of an SPListItem to a property of a business entity.
  /// </summary>
  [Serializable]
  public class ListItemFieldMappingException : Exception
  {
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    /// <summary>
    /// Initializes a new instance of the <see cref="ListItemFieldMappingException"/> class.
    /// </summary>
    public ListItemFieldMappingException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListItemFieldMappingException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public ListItemFieldMappingException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListItemFieldMappingException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner exception.</param>
    public ListItemFieldMappingException(string message, Exception inner)
      : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListItemFieldMappingException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
    protected ListItemFieldMappingException(
        SerializationInfo info,
        StreamingContext context)
      : base(info, context)
    {
    }
  }

}