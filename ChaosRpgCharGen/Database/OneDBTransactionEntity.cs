using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.Database
{
    /// <summary>
    /// HELPER CLASS TO MANAGE EASIER THE TRANSACTIONS
    /// </summary>
    public class OneDBTransactionEntity
    {
        public string theSqlText { get; }
        public KeyValuePair<string, object>[] theManyQueryDatas { get; }
        public KeyValuePair<string, object> theOnlyQueryData { get; }
        public byte theDataAmount { get; }
        /// <summary>
        /// CONSTRUCTOR OF A TRANSACTION CONTAINER - FOR MANY PARAMETER
        /// </summary>
        /// <param name="query">that must execute</param>
        /// <param name="datas">prepared data collection</param>
        /// <param name="amount">datas in prepared statement</param>
        public OneDBTransactionEntity(string query, KeyValuePair<string, object>[] datas, byte amount)
        {
            theSqlText = query;
            theManyQueryDatas = datas;
            theDataAmount = amount;
        }
        /// <summary>
        /// CONSTRUCTOR OF A TRANSACTION CONTAINER - FOR ONE PARAMETER
        /// </summary>
        /// <param name="query">that must execute</param>
        /// <param name="data">prepared data</param>
        /// <param name="amount"></param>
        public OneDBTransactionEntity(string query, KeyValuePair<string, object> data)
        {
            theSqlText = query;
            theOnlyQueryData = data;
        }
    }
}
