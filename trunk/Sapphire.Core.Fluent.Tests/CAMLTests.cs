using System;
using MbUnit.Framework;

namespace Sapphire.Core.Fluent.Tests
{
  public class CAMLTests
  {
    [Test]
    void should_have_static_creator()
    {
      Assert.IsNotNull(CAML.Create());
    }

    [Test]
    void should_have_buld_method()
    {
      Assert.IsNotNull(CAML.Create().Build());
    }

    [Test]
    void should_have_equal_predicate()
    {
      Assert.IsNotNull(CAML.Create().AddEqual(Guid.NewGuid(), 123).Build());
    }

    [Test]
    void should_have_beginswith_predicate()
    {
      Assert.IsNotNull(CAML.Create().AddBeginsWith(Guid.NewGuid(), "123").ForText().Build());
    }
  }
}
