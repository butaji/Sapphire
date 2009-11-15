using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Sapphire.Web.UI;

namespace Sapphire
{
  public class Application : Microsoft.SharePoint.ApplicationRuntime.SPHttpApplication
  {
    private static readonly object LockObject = new object();
    private static IUnityContainer _container;

    /// <summary>
    /// Gets and sets the Unity Container
    /// </summary>
    public virtual IUnityContainer Container
    {
      get { return _container; }
      protected set { _container = value; }
    }

    /// <summary>
    /// Handles the <see cref="Start"/> event and defines the application lifecycle.
    /// </summary>
    /// <param name="sender">The object firing the event.</param>
    /// <param name="e">The event associated data.</param>
    protected virtual void Application_Start(object sender, EventArgs e)
    {
      CreateContainer();
      AddRequiredServices();
      Start();
    }

    /// <summary>
    /// Adds the required application services to the container.
    /// </summary>
    /// <remarks>Override this method to add or change the services available in the container.</remarks>
    protected virtual void AddRequiredServices()
    {
    }

    /// <summary>
    /// Creates the application root container.
    /// </summary>
    /// <remarks>Override this method to change the container to be used by the application.</remarks>
    protected virtual void CreateContainer()
    {
      if (Container == null)
      {
        lock (LockObject)
        {
          if (Container == null)
          {
            IUnityContainer container = new UnityContainer();
            Container = container;
            Container.RegisterInstance(Container);
          }
        }
      }
    }

    /// <summary>
    /// Override this method to add behavior to be executed once the application has started.
    /// </summary>
    protected virtual void Start()
    {
    }
  }
}