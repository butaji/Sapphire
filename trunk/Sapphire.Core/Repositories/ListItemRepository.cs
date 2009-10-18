using Microsoft.SharePoint;
using Sapphire.Core.DomainBase;
using Sapphire.Core.Mapping;

namespace Sapphire.Core.Repositories
{
  /// <summary>
  /// The ListItemRepository class provides functinality to interact with SharePoint Lists
  /// </summary>
  internal class ListItemRepository<T> : IListItemRepository<T> where T : EntityBase, new()
  {
    #region Methods

    /// <summary>
    /// The Add method will add a new SPListItem to the specified list with a set of values.
    /// </summary>
    /// <param name="web">The SPWeb of the SPList</param>
    /// <param name="listUrl">The name of the SPList</param>
    /// <param name="mapper"></param>
    /// <param name="entity"></param>
    /// <returns>The new SPListItem added to the SPList</returns>
    public SPListItem Add(SPWeb web, string listUrl, ListItemFieldMapper<T> mapper, T entity)
    {
      SPListItem newItem = null;

      newItem = GetList(web, listUrl).Items.Add();

      mapper.FillSPListItemFromEntity(newItem, entity);

      newItem.Update();

      return newItem;
    }

    /// <summary>
    /// The Get method will get an SPListItem from the specified list based on a specified query.
    /// </summary>
    /// <param name="web">The SPWeb of the SPList</param>
    /// <param name="listUrl">The name of the SPList</param>
    /// <param name="query">The SPQuery to find the item</param>
    /// <returns>The SPListItem in the SPList</returns>
    public SPListItem Get(SPWeb web, string listUrl, SPQuery query)
    {
      SPListItem item = null;

      SPListItemCollection collection = GetList(web, listUrl).GetItems(query);

      if (collection != null && collection.Count > 0)
      {
        item = collection[0];
      }

      return item;
    }

    /// <summary>
    /// The GetAll method will get an SPListItemCollection from the specified list based on a specified query.
    /// </summary>
    /// <param name="web"></param>
    /// <param name="listUrl"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public SPListItemCollection GetAll(SPWeb web, string listUrl, SPQuery query)
    {
      return GetList(web, listUrl).GetItems(query);
    }

    /// <summary>
    /// The Update method will update a SPListItem in the specified list with a set of values.
    /// </summary>
    /// <param name="web">The SPWeb of the SPList</param>
    /// <param name="listUrl">The name of the SPList</param>
    /// <param name="listItemId">The Id of the list item to update</param>
    /// <param name="fields">Dictionarty containing unique SPField ID's and values</param>
    public void Update(SPWeb web, string listUrl, int listItemId, ListItemFieldMapper<T> mapper, T entity)
    {
      SPListItemCollection collection = null;

      collection = GetList(web, listUrl).GetItems(BuildQuery(listItemId));

      if (collection != null && collection.Count > 0)
      {
        SPListItem item = collection[0];

        mapper.FillSPListItemFromEntity(item, entity);

        bool webSetStatus = SetAllowUnsafeUpdates(web);
        item.Update();
        UnSetAllowUnsafeUpdates(web, webSetStatus);
      }
    }

    /// <summary>
    /// The Delete method will delete a SPListItem in the specified list.
    /// </summary>
    /// <param name="web">The SPWeb of the SPList</param>
    /// <param name="listUrl">The name of the SPList</param>
    /// <param name="listItemId">The Id of the list item to update</param>
    public void Delete(SPWeb web, string listUrl, int listItemId)
    {
      SPListItem item = null;
      SPListItemCollection collection = null;

      collection = GetList(web, listUrl).GetItems(BuildQuery(listItemId));

      if (collection != null && collection.Count > 0)
      {
        item = collection[0];

        bool webSetStatus = SetAllowUnsafeUpdates(web);
        item.Delete();
        UnSetAllowUnsafeUpdates(web, webSetStatus);
      }
    }

    private static SPQuery BuildQuery(int listItemId)
    {
      SPQuery query = new SPQuery();
      query.Query = string.Format("<Where><Eq><FieldRef Name='ID'/><Value Type='Integer'>{0}</Value></Eq></Where>", listItemId);

      return query;
    }

    private static bool SetAllowUnsafeUpdates(SPWeb web)
    {
      bool webSetStatus = false;

      if (!web.AllowUnsafeUpdates)
      {
        web.AllowUnsafeUpdates = true;
        webSetStatus = true;
      }

      return webSetStatus;
    }

    private static void UnSetAllowUnsafeUpdates(SPWeb web, bool webSetStatus)
    {
      if (webSetStatus)
      {
        web.AllowUnsafeUpdates = false;
      }
    }

    #endregion

    #region Privates

    /// <summary>
    /// Incapsulate getting list logic
    /// possible place for caching listId's
    /// </summary>
    /// <param name="web"></param>
    /// <param name="listUrl"></param>
    /// <returns></returns>
    static SPList GetList(SPWeb web, string listUrl)
    {
      return web.GetList(web.ServerRelativeUrl.TrimEnd('/') + listUrl);
    }

    #endregion
  }
}