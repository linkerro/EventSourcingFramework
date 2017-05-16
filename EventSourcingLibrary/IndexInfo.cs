using System.Collections.Generic;

namespace EventSourcingLibrary
{
    public class IndexInfo
    {
        public string IndexName { get; private set; }
        public IList<string> PropertyNames { get; private set; }

        public IndexInfo(string indexName,IList<string> propertyNames)
        {
            IndexName = indexName;
            PropertyNames = propertyNames;
        }
    }
}