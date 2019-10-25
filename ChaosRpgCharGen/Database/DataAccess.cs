using ChaosRpgCharGen.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace ChaosRpgCharGen.Databese
{
    public class DataAccess
    {
        //Data Source=chaos_database.db; -> relative path to DB file
        //Version=3; -> SQLite package version
        //New=false; -> no data stored in memory
        //Compress=True; -> SQLite DB compresses itself
        static string _ConnString = @"Data Source=chaos_database.db;Version=3;New=false;Compress=True;";

        static SQLiteConnection sqlite_Connnection;
        /// <summary>
        /// INITIALIZE DB CONNECTION
        /// </summary>
        /// <returns>SQLite connection</returns>
        public static SQLiteConnection ConnectToDB()
        {
            try
            {
                if (sqlite_Connnection == null)
                {
                    sqlite_Connnection = new SQLiteConnection(_ConnString);
                    sqlite_Connnection.Open();
                    return sqlite_Connnection;
                }
                else if (sqlite_Connnection.State != ConnectionState.Open)
                {
                    sqlite_Connnection.Open();
                    return sqlite_Connnection;
                }
                else
                    return sqlite_Connnection;
            }
            catch (SQLiteException e)
            {
                Debug.WriteLine("Database management failure!");
                Debug.WriteLine(e.Message + " " + e.ResultCode);
                if (e.ErrorCode == 11)   //CORRUPTED DB
                {
                    throw new DataAccessException("Az adatbázis sérült! " + e.Message + "=" + e.ResultCode);
                }
                if (e.ErrorCode == 12)  //NOT FOUND
                {
                    throw new DataAccessException("Az adatbázis hiányzik! " + e.Message + "=" + e.ResultCode);
                }
                if (e.ErrorCode == 14)  //CANNOT OPEN
                {
                    throw new DataAccessException("Az adatbázis megnyitása sikertelen! " + e.Message + "=" + e.ResultCode);
                }
                if (e.ErrorCode == 26)  //OPEND, BUT NOT A DB
                {
                    throw new DataAccessException("Az adatok elérése meghíúsult! " + e.Message + "=" + e.ResultCode);
                }
                throw new DataAccessException("Adatbázis kezelési hiba történt! " + e.Message + "=" + e.ResultCode);
            }
            catch(Exception e)
            {
                throw new DataAccessException("Ismeretlen hiba történt az adatbázis kezelés alatt! " + e.Message);
            }
        }

        #region normal nonQuery method  - boolean return

        /// <summary>
        /// EXECUTE NORMAL NON_SQL
        /// </summary>
        /// <param name="sql">SQL query</param>
        public static bool ExecuteNonSQL_normal(string sql)
        {
            int rowAffected = 0;
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, sqlite_Connnection);
                rowAffected = cmd.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                Debug.WriteLine("Database proccess failed!");
                Debug.WriteLine(e.Message + " ResultCode: " + rowAffected.ToString());
                throw new DataAccessException("Adatbázis kezelési hiba történt! " + e.Message + "=" + e.ResultCode);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
            return (rowAffected >= 0) ? true : false;
        }

        #endregion

        #region normal query method - outCell
        /// <summary>
        /// EXECUTE NORMAL SQL
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <returns>a cell</returns>
        public static object ExecuteSQL_normal_outCell(string sql)
        {
            try
            {
                object temp = null;
                SQLiteCommand cmd = new SQLiteCommand(sql, sqlite_Connnection);
                SQLiteDataReader sldr = cmd.ExecuteReader();
                while (sldr.Read())
                {
                    temp = sldr.GetValue(0);
                }
                return temp;
            }
            catch (SQLiteException e)
            {
                Debug.WriteLine("Database proccess failed!");
                throw new DataAccessException("Adatbázis kezelési hiba történt! " + e.Message + "=" + e.ResultCode);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }
        #endregion

        #region normal query method - outTable
        /// <summary>
        /// EXECUTE NORMAL SQL
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <returns>List of rows</returns>
        public static List<object[]> ExecuteSQL_normal_outTable(string sql)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, sqlite_Connnection);
                SQLiteDataReader sldr = cmd.ExecuteReader();

                List<object[]> temp = new List<object[]>(); 
                while (sldr.Read())
                {
                    object[] row = new object[sldr.FieldCount];
                    for(int i = 0; i<row.Length; i++)
                    {
                        row[i] = sldr.GetValue(i);
                    }
                    temp.Add(row);
                }
                return temp;
            }
            catch (SQLiteException e)
            {
                Debug.WriteLine("Database proccess failed!");
                throw new DataAccessException("Adatbázis kezelési hiba történt! " + e.Message + "=" + e.ResultCode);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }
        #endregion

        #region executeNonSQL - preparedOne - boolean return
        /// <summary>
        /// EXECUTE NON_SQL WITH NO RESULT
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="data">the only paremter information</param>
        /// <param name="dataAmount">one parameter</param>
        public static bool ExecuteNonSQL_prepOneParam(string sql, KeyValuePair<string, object> data)
        {
            int rowAffected = 0;
            if (data.Key == null || data.Value == null)
                return false;
            try
            {
                using (SQLiteCommand cmd = sqlite_Connnection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue(data.Key, data.Value);
                    rowAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
            return (rowAffected > 0) ? true : false;
        }
        #endregion

        #region executeNonSQL - prepared - boolean return
        /// <summary>
        /// EXECUTE NON_SQL WITH NO RESULT
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="datas">datas to query</param>
        /// <param name="dataAmount">number of parameters</param>
        public static bool ExecuteNonSQL_prepManyParam(string sql, KeyValuePair<string, object>[] datas, byte dataAmount)
        {
            int rowAffected = 0;
            if (revisePreparedDatas_isThereProblem(datas, dataAmount))
                return false;
            try
            {
                using (SQLiteCommand cmd = sqlite_Connnection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Prepare();

                    foreach (KeyValuePair<string, object> param in datas)
                    {
                        cmd.Parameters.AddWithValue(param.Key,param.Value);
                    }
                    rowAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
            return (rowAffected > 0)?true:false;
        }

        #endregion
        
        #region executeNonSQL - prepared for lot queries - boolean return
        /// <summary>
        /// EXECUTE LOTS OF NON_SQL WITH NO RESULT - ONLY ONE PARAM
        /// </summary>
        /// <param name="sqlCollect">SQL query</param>
        /// <param name="param">datas to query</param>
        /// <param name="dataAmount">number of parameters</param>
        /// <returns>succsess of queryMethod</returns>
        public static bool ExecuteNonSQL_prepManyQueryOneParam(List<string> sqlCollect, KeyValuePair<string, object> param)
        {
            byte success = 0;
            if (param.Key == null || param.Value == null)
                return false;
            if (sqlCollect.Count == 0)
                return false;
            try
            {
                using (SQLiteCommand cmd = sqlite_Connnection.CreateCommand())
                {
                    foreach (string sql in sqlCollect)
                    {
                        cmd.CommandText = sql;
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                        int rowAfffected = cmd.ExecuteNonQuery();
                        if (rowAfffected >= 0)
                            success++;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
            return (success == sqlCollect.Count) ? true : false;
        }
        #endregion

        #region executeNonSQL - prepared with one parameters transactinal - no return
        /// <summary>
        /// EXECUTE NON_SQL - PREPARED AND TRANSACTION
        /// </summary>
        /// <param name="prepTransQueryDatas">the transactions</param>
        public static void ExecuteNonSQL_prepOneParam_Trans(List<OneDBTransactionEntity> prepTransEntities)
        {
            try
            {
                using (SQLiteTransaction tr = sqlite_Connnection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = sqlite_Connnection.CreateCommand())
                    {
                        foreach (OneDBTransactionEntity ent in prepTransEntities)
                        {
                            if (ent.theOnlyQueryData.Key == null || ent.theOnlyQueryData.Value == null)
                                throw new SQLiteException("");
                            cmd.CommandText = ent.theSqlText;
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue(ent.theOnlyQueryData.Key, ent.theOnlyQueryData.Value);
                            cmd.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }
        #endregion

        #region executeNonSQL - prepared with many parameters transactinal - no return
        /// <summary>
        /// EXECUTE NON_SQL - PREPARED AND TRANSACTION
        /// </summary>
        /// <param name="prepTransQueryDatas">the transactions</param>
        public static void ExecuteNonSQL_prepManyParam_Trans(List<OneDBTransactionEntity> prepTransQueryDatas)
        {
            try
            {
                using (SQLiteTransaction tr = sqlite_Connnection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = sqlite_Connnection.CreateCommand())
                    {
                        foreach (OneDBTransactionEntity ent in prepTransQueryDatas)
                        {
                            if (revisePreparedDatas_isThereProblem(ent.theManyQueryDatas, ent.theDataAmount))
                                throw new SQLiteException("");
                            cmd.CommandText = ent.theSqlText;
                            cmd.Prepare();
                            foreach (KeyValuePair<string, object> param in ent.theManyQueryDatas)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value);
                            }
                            cmd.ExecuteNonQuery();
                        }
                        tr.Commit();

                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }

        #endregion

        #region executeSQL - prepared - outCell return
        /// <summary>
        /// EXECUTE_SQL AND GIVES RESULT OF CELL
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="datas">datas to query</param>
        /// <param name="dataAmount">number of parameters</param>
        /// <returns>cell result</returns>
        public static object ExecuteSQL_prep_outCell(string sql, KeyValuePair<string, object>[] datas, byte dataAmount)
        {
            if (revisePreparedDatas_isThereProblem(datas, dataAmount))
                return null;
            try
            {
                object temp = null;
                SQLiteCommand cmd = sqlite_Connnection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Prepare();

                foreach (KeyValuePair<string, object> param in datas)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                SQLiteDataReader sldr = cmd.ExecuteReader();
                while (sldr.Read())
                {
                    temp = sldr.GetValue(0);
                }
                return temp;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }
        #endregion

        #region executeSQL - prepared - outTable return
        /// <summary>
        /// EXECUTE_SQL AND GIVES RESULT OF ROWS
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <param name="datas">datas to query</param>
        /// <param name="dataAmount">number of parameters</param>
        /// <returns>List of query resutl</returns>
        public static List<object[]> ExecuteSQL_prep_outTable(string sql, KeyValuePair<string, object>[] datas, byte dataAmount)
        {
            if (revisePreparedDatas_isThereProblem(datas, dataAmount))
                return null;
            try
            {
                using (SQLiteCommand cmd = sqlite_Connnection.CreateCommand())
                {
                    return fetchDatas_InAndOutSQLCmd(cmd, sql, datas);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }
        /// <summary>
        /// HELPER METHOD OF EXECUTE_SQL
        /// </summary>
        /// <param name="cmd">SQLiteCommand</param>
        /// <param name="sql">SQL Query</param>
        /// <param name="datas">datas to query</param>
        /// <returns>List of query resutl</returns>
        private static List<object[]> fetchDatas_InAndOutSQLCmd(SQLiteCommand cmd, string sql, KeyValuePair<string, object>[] datas)
        {
            cmd.CommandText = sql;
            cmd.Prepare();

            foreach (KeyValuePair<string, object> param in datas)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            SQLiteDataReader sldr = cmd.ExecuteReader();

            List<object[]> datacollection = new List<object[]>();
            while (sldr.Read())
            {
                object[] row = new object[sldr.FieldCount];
                for(int i = 0; i< row.Length; i++)
                {
                    row[i] = sldr.GetValue(i);
                }
                datacollection.Add(row);
            }
            return datacollection;
        }

        #endregion

        #region executeSQL - prepared for lot queries - outCellRow return 
        /// <summary>
        /// EXECUTE_MANY SQL AND GIVES RESULT OF ROW
        /// TO ALL SQL GIVES THE SAME PARAMETERS!!
        /// </summary>
        /// <param name="sqlCollect">list of SQL queries</param>
        /// <param name="datas">datas to query</param>
        /// <param name="dataAmount">number of parameters</param>
        /// <returns>row result</returns>
        public static object[] ExecuteSQL_prepForManyQuery_outCellRow(string[] sqlCollect, KeyValuePair<string, object> data)
        {
            if (data.Key == null || data.Value == null)
                return null;
            if (sqlCollect.Length == 0)
                return null;
            try
            {
                object[] res = new object[sqlCollect.Length];
                SQLiteCommand cmd = null;
                for (byte i = 0; i < sqlCollect.Length; i++)
                {
                    cmd = sqlite_Connnection.CreateCommand();
                    cmd.CommandText = sqlCollect[i];
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue(data.Key, data.Value);
                    SQLiteDataReader sldr = cmd.ExecuteReader();
                    while (sldr.Read())
                    {
                        res[i] = sldr.GetValue(0);
                    }
                }
                return res;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database process failed!");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.GetType().ToString());
                throw new DataAccessException("Adatbázisűveleti hiba történt! " + e.Message);
            }
            finally
            {
                sqlite_Connnection.Close();
            }
        }
        #endregion

        #region inner query revisior
        /// <summary>
        /// REVISE THE ASSEMBYL OF PREPARED QUERY'S CONTENT - SEEKS QUANTITY ANOMALIES
        /// </summary>
        /// <param name="data">list of query-datas</param>
        /// <param name="dataAmount">amount are should be</param>
        /// <returns>true=failure happend / false=structural harmony</returns>
        private static bool revisePreparedDatas_isThereProblem(KeyValuePair<string, object>[] data, byte dataAmount)
        {
            if (data.Length != dataAmount)
            {
                Debug.WriteLine("Perpared query setting fail!");
                Debug.WriteLine("No equality in expected-amuont and real amount of query datas!");
                throw new DataAccessException("Perpared query setting fail! " +
                    "No equality in expected-amuont and real amount of query datas!");
            }
            for (byte i = 0; i < data.Length; i++)
            {
                if (data[i].Key == null)
                {
                    Debug.WriteLine("Perpared query setting fail!");
                    Debug.WriteLine("A key is missing, number: " + i.ToString());
                    throw new DataAccessException("Perpared query setting fail! " +
                        "A key is missing, number: " + i.ToString());
                }
                if (data[i].Value == null)
                {
                    Debug.WriteLine("Perpared query setting fail!");
                    Debug.WriteLine("A value is missing, number: " + i.ToString());
                    throw new DataAccessException("Perpared query setting fail! " +
                        "A value is missing, number: " + i.ToString());
                }
            }
            return false;
        }
        #endregion

    }
}
