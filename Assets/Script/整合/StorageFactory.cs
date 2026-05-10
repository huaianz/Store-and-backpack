using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StorageFactory;

public class StorageFactory : MonoBehaviour
{
    public abstract class StorageManagerFactory
    {
        protected Transform parent;
        protected MySqlData file;

        public StorageManagerFactory(Transform parent, MySqlData file = null)
        {
            this.parent = parent;
            this.file = file;
        }

        public abstract StorageInterface CreateStorageManager();
    }
}

public class SQLiteStorageFactory : StorageManagerFactory
{
    public SQLiteStorageFactory(Transform parent) : base(parent) { }

    public override StorageInterface CreateStorageManager()
    {
        //创建一个新物体，命名为SQLiteStorageManager
        var sqliteObject = new GameObject("SQLiteStorageManager");

        sqliteObject.transform.SetParent(parent);
        var manager = sqliteObject.AddComponent<SQLiteStorageManager>();
        manager.Initialize();
        return manager;
    }
}

public class MySQLStorageFactory : StorageManagerFactory
{
    public MySQLStorageFactory(Transform parent, MySqlData file) : base(parent, file) { }

    public override StorageInterface CreateStorageManager()
    {
        var mysqlObject = new GameObject("MySQLStorageManager");

        mysqlObject.transform.SetParent(parent);
        var manager = mysqlObject.AddComponent<MySQLStorageManager>();
        if (file != null)
        {
            manager.ConfigureDatabase(file.Server, file.Database, file.Username, file.Password, file.Port);
        }
        manager.Initialize();
        return manager;
    }
}
