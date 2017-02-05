using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp
{
    public class DBWrapper
    {
        public bool CreateDB (string name)
        {
            throw new NotImplementedException();
        }

        public bool AddTable (string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTable(string name)
        {
            throw new NotImplementedException();
        }

        public bool AddRecord(string tableName, int id, Card card)
        {
            throw new NotImplementedException();
        }

        public bool ReplaceRecord(string tableName, int id, Card card)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(string tableName, int id)
        {
            throw new NotImplementedException();
        }

    }
}
