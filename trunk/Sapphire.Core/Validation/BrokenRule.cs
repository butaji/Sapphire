namespace Sapphire.Core.Validation
{
  public class BrokenRule
  {
    private readonly string _description;
    private readonly string _name;

    public BrokenRule(string name, string description)
    {
      _name = name;
      _description = description;
    }

    public string Name
    {
      get { return _name; }
    }

    public string Description
    {
      get { return _description; }
    }
  }
}