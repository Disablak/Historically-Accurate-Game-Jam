using System;
using System.Linq;

namespace Core
{
  public enum ResourceType
  {
    NONE,

    COAL,
    IRON,
    GOLD,
    DIAMOND
  }

  public static class ResourceTypeHelper
  {
    public static ResourceType[] allValues => Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>().Except( new []{ResourceType.NONE}).ToArray();

    public static string toString(this ResourceType[] resource_types)
    {
      string result = string.Empty;
      foreach (ResourceType resource_type in resource_types)
        result += resource_type.ToString() + " ";

      return result.Trim();
    }
  }
}