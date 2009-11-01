using System.IO;
using System.Web;
using System.Web.UI;
using MbUnit.Framework;
using TypeMock;
using System;

namespace Sapphire.Application.Tests
{
  public class UserControlsTests
  {
    [Test]
    private void page_lifecycle_test()
    {
      Page page = new Page();

      MockObject<HttpBrowserCapabilities> mockBrowser = MockManager.MockObject<HttpBrowserCapabilities>(Constructor.NotMocked);
      mockBrowser.ExpectGetAlways("PreferredRenderingMime", "text/html");
      mockBrowser.ExpectGetAlways("PreferredResponseEncoding", "UTF-8");
      mockBrowser.ExpectGetAlways("PreferredRequestEncoding", "UTF-8");
      mockBrowser.ExpectGetAlways("SupportsMaintainScrollPositionOnPostback", false);

      MockObject<HttpRequest> mockRequest = MockManager.MockObject<HttpRequest>(Constructor.Mocked);
      mockRequest.ExpectGetAlways("FilePath", "/default.aspx");
      mockRequest.ExpectGetAlways("HttpMethod", "GET");
      mockRequest.ExpectGetAlways("Browser", mockBrowser.Object);

      MockObject<HttpResponse> mockResponse = MockManager.MockObject<HttpResponse>(Constructor.Mocked);

      HttpContext mockContext = new HttpContext(mockRequest.Object, mockResponse.Object);

      using (StringWriter stringWriter = new StringWriter())
      using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
      {
        mockBrowser.AlwaysReturn("CreateHtmlTextWriter", htmlWriter);
        page.PreInit +=
          delegate(object sender, EventArgs e)
          {
            // Perform some action
          };
        page.Load +=
          delegate(object sender, EventArgs e)
          {
            // Check/Assert the results of your action
          };
        page.ProcessRequest(mockContext);
      }
    }
  }
}
