﻿using System;

namespace Core.Tools.ExtensionMethods
{
    public static class RangeEx
    {
        public static int RandomTo(this Range range)
        {
            return range.End.Value.RandomTo() + range.Start.Value;
        }
    }
}