using FirebirdSql.Data.FirebirdClient;
using PackagingMachine.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PackagingMachine
{
    class FirebirdAccess
    {
        public FbConnection CreatConnect()
        {
            FbConnection connection = new FbConnection(Settings.Default.FireBirdConnectionString);
            return connection;
        }

        public void SavePrescription(List<Prescription> dsPrescription,List<PrescriptionDetail> dsPrescriptionDetail)
        {
            FbConnection conn = CreatConnect();
            conn.Open();
            FbTransaction transaction = conn.BeginTransaction();
            try
            {
                FbCommand command = conn.CreateCommand();
                command.Transaction = transaction;

                string path = Application.StartupPath + @"\\PackMed.xml";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                XmlDocument packMedConfig = new XmlDocument();
                //根节点
                XmlNode rootnode = packMedConfig.CreateElement("PackMed");
                packMedConfig.AppendChild(rootnode);
                int i = 0;
                foreach (Prescription prescription in dsPrescription)
                {
                    command.CommandText = "insert into DATA_PRESCRIPTION " +
                        " (ID,REGISTER_ID,NAME,SEX,AGE,TELE,EMAIL,DEPARTMENT_NAME,DOCTOR_NAME,PRESCRIPTION_NAME,PRESCRIBE_TIME,VALUE_SN,VALUER_NAME,VALUATION_TIME,PRICE,QUANTITY,QUANTITY_DAY,PRICE_TOTAL,PAYMENT_TYPE,PAYMENT_STATUS,DATA_SOURCE,PROCESS_STATUS,DESCRIPTION) " +
                        " values (" +
                        "'" + prescription.Id + "'," +
                        "'" + prescription.Registerid + "'," +
                        "'" + prescription.Name + "'," +
                        "'" + prescription.Sex + "'," +
                        "" + prescription.Age + "," +
                        "'" + prescription.Tele + "'," +
                        "'" + prescription.Email + "'," +
                        "'" + prescription.DepartmentName + "'," +
                        "'" + prescription.DoctorName + "'," +
                        "'" + prescription.PrescriptionName + "'," +
                        "'" + prescription.PrescribeTime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + prescription.Valuesn + "'," +
                        "'" + prescription.Valuername + "'," +
                        "'" + prescription.Valuationtime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "" + prescription.Price + "," +
                         "" + prescription.Quantity + "," +
                         "" + prescription.Quantityday + "," +
                         "" + prescription.Pricetotal + "," +
                         "'" + prescription.Paymenttype + "'," +
                         "'" + prescription.Paymentstatus + "'," +
                         "'" + prescription.Datasource + "'," +
                         "'" + prescription.Processstatus + "'," +
                         "'" + prescription.Description + "'" +
                        ")";

                    XmlElement xmlmachineNo = packMedConfig.CreateElement("sql" + i);
                    xmlmachineNo.SetAttribute("sql" + i, command.CommandText);
                    rootnode.AppendChild(xmlmachineNo);
                    packMedConfig.Save(path);
                    i++;

                    command.ExecuteNonQuery();

                
                }

                string path2 = Application.StartupPath + @"\\PackMedDetail.xml";
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }

                XmlDocument packMedConfig2 = new XmlDocument();
                //根节点
                XmlNode rootnode2 = packMedConfig2.CreateElement("PackMedDetail");
                packMedConfig2.AppendChild(rootnode2);
                int j = 0;
                foreach (PrescriptionDetail prescriptionDetail in dsPrescriptionDetail)
                {
                    command.CommandText = "insert into DATA_PRESCRIPTION_DETAIL " +
                        " (ID,\"NO\",GRANULE_ID,GRANULE_NAME,DOSE_HERB,EQUIVALENT,DOSE,PRICE) " +
                        " values (" +
                        "'" + prescriptionDetail.ID + "'," +
                        "" + prescriptionDetail.No + "," +
                        "'" + prescriptionDetail.Granuleid + "'," +
                        "'" + prescriptionDetail.Granulename + "'," +
                        "" + prescriptionDetail.Doseherb + "," +
                        "" + prescriptionDetail.Equivalent + "," +
                        "" + prescriptionDetail.Dose + "," +
                        "" + prescriptionDetail.Price + "" +
                        ")";

                    XmlElement xmlmachineNo = packMedConfig2.CreateElement("sql" + j);
                    xmlmachineNo.SetAttribute("sql" + j, command.CommandText);
                    rootnode2.AppendChild(xmlmachineNo);
                    packMedConfig2.Save(path2);
                    j++;

                    command.ExecuteNonQuery();

                    
                }

                transaction.Commit();
            }
            catch (Exception err)
            {
                transaction.Rollback();
                throw new Exception(err.Message);
            }
            finally
            {
                conn.Close();
            }          
        }
    }
}
