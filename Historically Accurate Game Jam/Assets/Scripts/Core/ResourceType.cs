﻿using System;
using System.Linq;

namespace Core
{
  public enum ResourceType
  {
    NONE,

    COAL,
    GOLD,
    DIAMOND
  }

  public static class ResourceTypeHelper
  {
    public static ResourceType[] allValues => Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>().Except( new []{ResourceType.NONE}).ToArray();
  }
}