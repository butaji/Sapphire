using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Sapphire.Core.Fluent.Interfaces;

namespace Sapphire.Core.Fluent
{
  public class CAML
  {
    private readonly IList<CAMLFilter> _filters = new List<CAMLFilter>();

    #region LifeCycle

    private CAML()
    {
    }

    public static CAML Create()
    {
      return new CAML();
    }

    /// <summary>
    /// Build a CAML Query string, based on the filter expressions that have been added to it.
    /// </summary>
    /// <returns></returns>
    [SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
    [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
    public string Build()
    {
      StringBuilder queryBuilder = new StringBuilder("<Where>");

      if (_filters.Count > 1)
      {
        queryBuilder.Append("<And>");
      }

      foreach (CAMLFilter filter in _filters)
      {
        queryBuilder.Append(filter.FilterExpression);
      }

      if (_filters.Count > 1)
      {
        queryBuilder.Append("</And>");
      }

      queryBuilder.Append("</Where>");

      return queryBuilder.ToString();
    }

    #endregion

    #region CommonAdds

    public void AddFilter(CAMLFilter camlFilter)
    {
      _filters.Add(camlFilter);
    }

    public CAML AddPredicate(Predicates predicate, Guid fieldId, SPFieldType fieldType, object value)
    {
      string filterExpression = string.Format(CultureInfo.InvariantCulture,
                                              "<{0}><FieldRef ID='{1}'/><Value Type='{2}'>{3}</Value></{0}>"
                                              , predicate, fieldId, fieldType, FormatValue(value));

      AddFilter(new CAMLFilter { FilterExpression = filterExpression });
      return this;
    }

    public CAML AddPredicateWithAutoFieldTypeCasting(Predicates predicate, Guid fieldId, object value)
    {
      SPFieldType fieldType = SPFieldType.Text;
      if (value is DateTime)
        fieldType = SPFieldType.DateTime;
      if (value is int)
        fieldType = SPFieldType.Integer;

      return AddPredicate(predicate, fieldId, fieldType, value);
    }

    /// <summary>
    /// Add a filter expression to filter by content type.
    /// </summary>
    /// <param name="contentTypeName">The contenttype to filter by.</param>
    public CAML FilterByContentType(string contentTypeName)
    {
      return AddPredicate(Predicates.Eq, SPBuiltInFieldId.ContentType, SPFieldType.Text, contentTypeName);
    }

    internal CAML AddDateTimeVarPredicate(Predicates predicate, Guid fieldId, DateTimeVariables variable)
    {
      string xmlVariable = string.Format("<{0}/>", variable);
      return AddPredicate(predicate, fieldId, SPFieldType.DateTime, xmlVariable);
    }

    #endregion

    #region Utils

    private static object FormatValue(object value)
    {
      if (value is DateTime)
        return SPUtility.CreateISO8601DateTimeFromSystemDateTime((DateTime)value);

      return value;
    }

    #endregion

    #region Null

    public CAML AddIsNull(Guid fieldId)
    {
      string filterExpression = string.Format(CultureInfo.InvariantCulture,
                                              "<IsNull><FieldRef ID='{0}'></FieldRef></IsNull>", fieldId);
      AddFilter(new CAMLFilter { FilterExpression = filterExpression });
      return this;
    }

    public CAML AddIsNotNull(Guid fieldId)
    {
      string filterExpression = string.Format(CultureInfo.InvariantCulture,
                                              "<IsNotNull><FieldRef ID='{0}'></FieldRef></IsNotNull>", fieldId);
      AddFilter(new CAMLFilter { FilterExpression = filterExpression });
      return this;
    }

    #endregion

    #region IBeginsWithAndContains

    public IBeginsWithAndContains AddBeginsWith(Guid fieldId, string value)
    {
      return new BeginsWithAndContains(this, Predicates.BeginsWith, fieldId, value);
    }

    public IBeginsWithAndContains AddContains(Guid fieldId, string value)
    {
      return new BeginsWithAndContains(this, Predicates.Contains, fieldId, value);
    }

    #endregion

    #region Equals

    public CAML AddEqual(Guid fieldId, SPFieldType fieldType, object value)
    {
      return AddPredicate(Predicates.Eq, fieldId, fieldType, value);
    }

    public CAML AddEqual(Guid fieldId, object value)
    {
      return AddPredicateWithAutoFieldTypeCasting(Predicates.Eq, fieldId, value);
    }

    public CAML AddEqual(Guid fieldId, int value)
    {
      return AddEqual(fieldId, SPFieldType.Integer, value);
    }

    [SharePointPermissionAttribute(SecurityAction.InheritanceDemand, ObjectModel = true)]
    [SharePointPermissionAttribute(SecurityAction.LinkDemand, ObjectModel = true)]
    public CAML AddEqual(Guid fieldId, DateTime value)
    {
      return AddEqual(fieldId, SPFieldType.DateTime, value);
    }

    public CAML AddEqual(Guid fieldId, string value)
    {
      return AddEqual(fieldId, SPFieldType.Text, value);
    }

    public CAML AddNotEqual(Guid fieldId, SPFieldType fieldType, object value)
    {
      return AddPredicate(Predicates.Neq, fieldId, fieldType, value);
    }

    public CAML AddNotEqual(Guid fieldId, object value)
    {
      return AddPredicateWithAutoFieldTypeCasting(Predicates.Neq, fieldId, value);
    }

    public CAML AddNotEqual(Guid fieldId, int value)
    {
      return AddNotEqual(fieldId, SPFieldType.Integer, value);
    }

    [SharePointPermissionAttribute(SecurityAction.InheritanceDemand, ObjectModel = true)]
    [SharePointPermissionAttribute(SecurityAction.LinkDemand, ObjectModel = true)]
    public CAML AddNotEqual(Guid fieldId, DateTime value)
    {
      return AddNotEqual(fieldId, SPFieldType.DateTime, value);
    }

    public CAML AddNotEqual(Guid fieldId, string value)
    {
      return AddNotEqual(fieldId, SPFieldType.Text, value);
    }

    #endregion

    #region Compare

    public CAML AddGreaterThanOrEqualTo(Guid fieldId, SPFieldType fieldType, object value)
    {
      return AddPredicate(Predicates.Geq, fieldId, fieldType, value);
    }

    public CAML AddGreaterThanOrEqualTo(Guid fieldId, object value)
    {
      return AddPredicateWithAutoFieldTypeCasting(Predicates.Geq, fieldId, value);
    }

    public CAML AddGreaterThanOrEqualTo(Guid fieldId, int value)
    {
      return AddPredicate(Predicates.Geq, fieldId, SPFieldType.Integer, value);
    }

    public CAML AddGreaterThanOrEqualTo(Guid fieldId, DateTime value)
    {
      return AddPredicate(Predicates.Geq, fieldId, SPFieldType.DateTime, value);
    }

    public CAML AddGreaterThanOrEqualTo(Guid fieldId, DateTimeVariables variable)
    {
      return AddDateTimeVarPredicate(Predicates.Geq, fieldId, variable);
    }

    public CAML AddGreaterThan(Guid fieldId, SPFieldType fieldType, object value)
    {
      return AddPredicate(Predicates.Gt, fieldId, fieldType, value);
    }

    public CAML AddGreaterThan(Guid fieldId, object value)
    {
      return AddPredicateWithAutoFieldTypeCasting(Predicates.Gt, fieldId, value);
    }

    public CAML AddGreaterThan(Guid fieldId, int value)
    {
      return AddPredicate(Predicates.Gt, fieldId, SPFieldType.Integer, value);
    }

    public CAML AddGreaterThan(Guid fieldId, DateTime value)
    {
      return AddPredicate(Predicates.Gt, fieldId, SPFieldType.DateTime, value);
    }

    public CAML AddGreaterThan(Guid fieldId, DateTimeVariables variable)
    {
      return AddDateTimeVarPredicate(Predicates.Gt, fieldId, variable);
    }

    public CAML AddLessThanOrEqualTo(Guid fieldId, SPFieldType fieldType, object value)
    {
      return AddPredicate(Predicates.Leq, fieldId, fieldType, value);
    }

    public CAML AddLessThanOrEqualTo(Guid fieldId, object value)
    {
      return AddPredicateWithAutoFieldTypeCasting(Predicates.Leq, fieldId, value);
    }

    public CAML AddLessThanOrEqualTo(Guid fieldId, int value)
    {
      return AddPredicate(Predicates.Leq, fieldId, SPFieldType.Integer, value);
    }

    public CAML AddLessThanOrEqualTo(Guid fieldId, DateTime value)
    {
      return AddPredicate(Predicates.Leq, fieldId, SPFieldType.DateTime, value);
    }

    public CAML AddLessThanOrEqualTo(Guid fieldId, DateTimeVariables variable)
    {
      return AddDateTimeVarPredicate(Predicates.Leq, fieldId, variable);
    }

    public CAML AddLessThan(Guid fieldId, SPFieldType fieldType, object value)
    {
      return AddPredicate(Predicates.Lt, fieldId, fieldType, value);
    }

    public CAML AddLessThan(Guid fieldId, object value)
    {
      return AddPredicateWithAutoFieldTypeCasting(Predicates.Lt, fieldId, value);
    }

    public CAML AddLessThan(Guid fieldId, int value)
    {
      return AddPredicate(Predicates.Lt, fieldId, SPFieldType.Integer, value);
    }

    public CAML AddLessThan(Guid fieldId, DateTime value)
    {
      return AddPredicate(Predicates.Lt, fieldId, SPFieldType.DateTime, value);
    }

    public CAML AddLessThan(Guid fieldId, DateTimeVariables variable)
    {
      return AddDateTimeVarPredicate(Predicates.Lt, fieldId, variable);
    }

    #endregion

    #region Membership

    public CAML AddCurrentUserGroups(Guid fieldId)
    {
      string filterExpression = string.Format(CultureInfo.InvariantCulture,
                                             "<Membership Type=\"CurrentUserGroups\"><FieldRef ID=\"{0}\"/></Membership>", fieldId);

      AddFilter(new CAMLFilter { FilterExpression = filterExpression });
      return this;
    }

    #endregion

    #region DateRangesOverlap

    public CAML AddDateRangesOverlap(Guid eventDateFieldId, Guid endDateFieldId, Guid recurrenceIdFieldId, DateTimeVariables variable)
    {
      string filterExpression = string.Format(CultureInfo.InvariantCulture,
                                             "<DateRangesOverlap><FieldRef ID=\"{0}\"></FieldRef><FieldRef ID=\"{1}\"></FieldRef><FieldRef ID=\"{2}\"></FieldRef><Value Type=\"DateTime\"><{3}/></Value></DateRangesOverlap>",
                                           eventDateFieldId, endDateFieldId, recurrenceIdFieldId, variable);

      AddFilter(new CAMLFilter { FilterExpression = filterExpression });
      return this;
    }

    #endregion
  }
}