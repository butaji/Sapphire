using System;
using System.CodeDom;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity.InterceptionExtension;
using Sapphire.Extensions;

namespace Sapphire.Web.UI
{
  public class SapphireControlBuilder : ControlBuilder
  {
    public override void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit,
                                              CodeTypeDeclaration baseType,
                                              CodeTypeDeclaration derivedType,
                                              CodeMemberMethod buildMethod,
                                              CodeMemberMethod dataBindingMethod)
    {
      if (ControlType.MarkedWith(typeof(ResolveInsteadOfCallConstructorAttribute)))
      {
        codeCompileUnit.Namespaces[0].Imports.Add(new CodeNamespaceImport("Sapphire.Web.UI"));
        ReplaceConstructorWithContainerResolveMethod(buildMethod);
      }
      base.ProcessGeneratedCode(codeCompileUnit, baseType, derivedType, buildMethod, dataBindingMethod);
    }

    public static T Build<T>()
    {
      return Build<T>(typeof(T)); ;
    }

    public static T Build<T>(Type type)
    {
      return (T)((Application)HttpContext.Current.ApplicationInstance)
        .Container
        .AddNewExtension<Interception>()
        .Configure<Interception>()
        .SetInterceptorFor(type, new VirtualMethodInterceptor())
        .Container
        .Resolve(type);
    }

    private void ReplaceConstructorWithContainerResolveMethod(CodeMemberMethod buildMethod)
    {
      foreach (CodeStatement statement in buildMethod.Statements)
      {
        var assign = statement as CodeAssignStatement;

        if (null != assign)
        {
          var constructor = assign.Right as CodeObjectCreateExpression;

          if (null != constructor)
          {
            assign.Right =
              new CodeSnippetExpression(
                string.Format("SapphireControlBuilder.Build<{0}>()",
                              ControlType.FullName));
            break;
          }
        }
      }
    }

  }
}