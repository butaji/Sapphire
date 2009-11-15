using System;

namespace Sapphire.Extensions
{
  public static class CodeDomExtensions
  {
    public static bool MarkedWith(this Type classType, Type customAttributeType)
    {
      return classType
        .GetCustomAttributes(customAttributeType, true) != null;
    }
  }
}