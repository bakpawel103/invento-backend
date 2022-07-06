using System.Xml.Linq;
using warehouseapi.Models;

namespace warehouseapi.Tools
{
    public class ItemDatabaseController
    {
        private string ItemsDbPath;

        public ItemDatabaseController()
        {
            string localLowPath = ReplaceInternalName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            ItemsDbPath = Path.Combine(localLowPath, "Invento", "Databases", "items.xml");
        }

        public ItemDatabaseController(string itemsDbPath)
        {
            ItemsDbPath = itemsDbPath;
        }

        public void InitializeDatabaseFile()
        {
            string itemDatabaseDictionaryPath = Path.GetDirectoryName(ItemsDbPath);
            if (!Directory.Exists(itemDatabaseDictionaryPath))
            {
                Directory.CreateDirectory(itemDatabaseDictionaryPath);
            }

            XDocument root = ItemXDocumentHelper.GetEmpty();

            SaveDatabase(root);
        }

        public void SaveItems(List<Item> items)
        {
            XDocument root = ItemXDocumentHelper.GetFromItemsList(items);

            SaveDatabase(root);
        }

        public List<Item> LoadItemsDatabase()
        {
            if (!File.Exists(ItemsDbPath) || new FileInfo(ItemsDbPath).Length == 0)
            {
                InitializeDatabaseFile();
            }

            XDocument xDocument = XDocument.Load(ItemsDbPath);

            return ItemXDocumentHelper.GetItemsListFromXDocument(xDocument);
        }

        private void SaveDatabase(XDocument root)
        {            
            using (FileStream fs = new FileStream(ItemsDbPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(root.ToString());
                }
            }
        }

        #region Helpers
        private string ReplaceInternalName(string path)
        {
            return path.Replace("Roaming", "LocalLow");
}
        #endregion
    }
}
