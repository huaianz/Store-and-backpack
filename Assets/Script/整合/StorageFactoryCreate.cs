
using UnityEngine;
using static StorageFactory;

public static class StorageFactoryCreate
{

    public static StorageManagerFactory CreateFactory(StorageType storageType, Transform parent, MySqlData file = null)
    {

        if (storageType == StorageType.SQLite)
        {
            return new SQLiteStorageFactory(parent);
        }
        else if (storageType == StorageType.MySQL)
        {
            return new MySQLStorageFactory(parent, file);
        }
        else
        {
            return null;
        }
    }
}