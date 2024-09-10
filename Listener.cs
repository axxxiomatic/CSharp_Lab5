using System;
using System.Collections.Generic;

namespace Lab05_CSharp
{
    class Listener
    {
        private List<ListEntry> changes_list = new List<ListEntry>();

        public void Add_changes(object source, EventArgs args)
        {
            var even = args as MagazinesChangedEventArgs<string>;
            changes_list.Add(new ListEntry(even.collectionName, even.why, even.propertyName, even.elementKey));
        }

        public override string ToString()
        {
            string result = "";
            foreach (var elem in changes_list)
            {
                result += "\nChange in collection: " + elem.collectionName + " |Change type: " + elem.why + " |Change in property: " + elem.propertyName + " |Element key: " + elem.textElementKey;
            }
            return result;
        }
    }
}
