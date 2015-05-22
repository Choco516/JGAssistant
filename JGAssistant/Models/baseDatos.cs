using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

namespace JGAssistant.Models
{
    public class baseDatos
    {
        OracleConnection OraConn = new OracleConnection();
        OracleCommand OraCmd;

        //String de conección
        public string conString;
        public bool mantenerConexion = false;

        //constructor
        public baseDatos(string conexion)
        {
            this.conString = conexion;
        }

        //constructor2
        public baseDatos(string conexion, bool mantenerConexion)
        {
            this.conString = conexion;
            this.mantenerConexion = mantenerConexion;
        }

        //obtiene el string de conexión
        private string Get_ConnectionString()
        {
            return this.conString;
        }

        //abre la conexion
        public void Open_Connection()
        {
            if (OraConn.State == ConnectionState.Broken | OraConn.State == ConnectionState.Closed)
            {
                OraConn = new OracleConnection(Get_ConnectionString());
                OraConn.Open();
                //OraConn.ModuleName = "SNCA";
                //Begin_Transaccion()
                //OraCmd.CommandType = CommandType.Text
                //OraCmd.CommandText = "alter session set nls_date_format='dd/mm/yyyy'"
                //OraCmd.ExecuteNonQuery()
                //End_Transaccion(False)
            }
        }

        //cierra la conexion
        public void Close_Connection()
        {
            if (!mantenerConexion)
            {
                OraConn.Close();
            }
        }

        //inicia un transacción
        public void Begin_Transaccion()
        {
            OraCmd = new OracleCommand("", OraConn);
            OraConn.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        //termina una transacción iniciada anteriormente
        public void End_Transaccion(bool commit)
        {
            try
            {
                if (commit)
                {
                    OraCmd.Transaction.Commit();
                }
                else
                {
                    OraCmd.Transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Ejecuta un consulta y retorna un dataset
        public DataSet ExecuteSelect(string sql, CommandType cmdType)
        {
            List<OracleParameter> parametros = new List<OracleParameter>();
            return ExecuteSelect(sql, cmdType, parametros);
        }

        //Ejecuta un consulta con parámetros y retorna un dataset
        public DataSet ExecuteSelect(string sql, CommandType cmdType, List<OracleParameter> parametros)
        {
            OracleDataAdapter da = default(OracleDataAdapter);
            DataSet ds = new DataSet();
            OraCmd = new OracleCommand();
            OraCmd.Connection = OraConn;
            OraCmd.CommandType = cmdType;
            OraCmd.CommandText = sql;
            if (parametros.Count > 0)
            {
                foreach (OracleParameter param in parametros)
                {
                    OraCmd.Parameters.Add(param);
                }
            }
            da = new OracleDataAdapter(OraCmd);
            da.Fill(ds);
            OraCmd.Parameters.Clear();
            return ds;
        }

        //Ejecuta un select que retorna un solo dato
        public object ExecuteSelectOne(string sql)
        {
            List<OracleParameter> parametros = new List<OracleParameter>();
            return ExecuteSelectOne(sql, parametros);
        }

        //Ejecuta un select con parametros que retorna un solo dato
        public object ExecuteSelectOne(string sql, List<OracleParameter> parametros)
        {
            object result = null;
            OracleDataAdapter da = default(OracleDataAdapter);
            DataSet ds = new DataSet();
            OraCmd = new OracleCommand();
            OraCmd.Connection = OraConn;
            OraCmd.CommandType = CommandType.Text;
            OraCmd.CommandText = sql;
            if (parametros.Count > 0)
            {
                foreach (OracleParameter param in parametros)
                {
                    OraCmd.Parameters.Add(param);
                }
            }
            da = new OracleDataAdapter(OraCmd);
            da.Fill(ds);
            OraCmd.Parameters.Clear();
            try
            {
                result = ds.Tables[0].Select(null, null, DataViewRowState.CurrentRows).ElementAt(0)[0];
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        
    }
}