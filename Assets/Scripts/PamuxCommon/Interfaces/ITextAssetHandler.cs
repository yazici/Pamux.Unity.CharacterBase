using System.Collections.Generic;

namespace Pamux.Interfaces
{
    public interface ITextAssetHandler
    {
        void SetVariable(string name, string value);
        void AddItems(IDictionary<string, int> headerNameToColMap, string[] fields);

        void OnItemsReady();
    }
}
