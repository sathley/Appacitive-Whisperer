using System.Collections.Generic;

namespace Appacitive.Tools.DBImport
{
    public class CannedList
    {
        public string Name { get; set; }

        public List<ListItem> Items { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }
    }

    public class ListItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public long Position { get; set; }
    }
}