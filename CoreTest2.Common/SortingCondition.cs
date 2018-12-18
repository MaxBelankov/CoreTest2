using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTest2.Common
{
    public class SortingCondition
    {
        public SortingField Field { get; set; }
        public SortingDirection Direction { get; set; }

        public static SortingCondition Default = new SortingCondition { Field = SortingField.Id, Direction = SortingDirection.Asc };
    }
}
