using System;
using Microsoft.SharePoint;

namespace Sapphire.Core.Fluent.Interfaces
{
  internal class BeginsWithAndContains : IBeginsWithAndContains
  {
    private readonly CAML _camlBuilder;
    private readonly Predicates _predicate;
    private readonly Guid _fieldId;
    private readonly string _value;

    internal BeginsWithAndContains(CAML camlBuilder, Predicates predicate, Guid fieldId, string value)
    {
      if (predicate != Predicates.Contains || predicate != Predicates.BeginsWith)
        throw new ArgumentException("contains");

      _camlBuilder = camlBuilder;
      _predicate = predicate;
      _fieldId = fieldId;
      _value = value;
    }

    public CAML ForNote()
    {
      return Result(SPFieldType.Note);
    }

    public CAML ForText()
    {
      return Result(SPFieldType.Text);
    }

    private CAML Result(SPFieldType fieldType)
    {
      return _camlBuilder
        .AddPredicate(_predicate, _fieldId, fieldType, _value);
    }
  }
}