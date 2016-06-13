using PackagingMachine.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingMachine
{
    class SqlAccess
    {
        public SqlConnection CreatConnect()
        {
            SqlConnection connection = new SqlConnection(Settings.Default.HisConnectionString);
            return connection;
        }

        public DataSet QueryPrescriptionByCfh(string strCfh)
        {
            SqlConnection conn = CreatConnect();
            conn.Open();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT DISTINCT DATA_PRESCRIPTION.* FROM DATA_PRESCRIPTION,DATA_PRESCRIPTION_DETAIL where  DATA_PRESCRIPTION.ID = DATA_PRESCRIPTION_DETAIL.ID AND ID = '" + strCfh + "'";
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataSet dsHis = new DataSet();
                sd.Fill(dsHis);
                return dsHis;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet QueryPrescriptionDetailByCfh(string strCfh)
        {
            SqlConnection conn = CreatConnect();
            conn.Open();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT DATA_PRESCRIPTION_DETAIL.* FROM DATA_PRESCRIPTION,DATA_PRESCRIPTION_DETAIL where  DATA_PRESCRIPTION.ID = DATA_PRESCRIPTION_DETAIL.ID AND ID = '" + strCfh + "'";
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataSet dsHis = new DataSet();
                sd.Fill(dsHis);
                return dsHis;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet QueryPrescriptionAll()
        {
            SqlConnection conn = CreatConnect();
            conn.Open();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT DISTINCT DATA_PRESCRIPTION.* FROM DATA_PRESCRIPTION,DATA_PRESCRIPTION_DETAIL where  DATA_PRESCRIPTION.ID = DATA_PRESCRIPTION_DETAIL.ID";
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataSet dsHis = new DataSet();
                sd.Fill(dsHis);
                return dsHis;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet QueryPrescriptionDetailAll( )
        {
            SqlConnection conn = CreatConnect();
            conn.Open();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT DATA_PRESCRIPTION_DETAIL.* FROM DATA_PRESCRIPTION,DATA_PRESCRIPTION_DETAIL where  DATA_PRESCRIPTION.ID = DATA_PRESCRIPTION_DETAIL.ID ";
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataSet dsHis = new DataSet();
                sd.Fill(dsHis);
                return dsHis;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
