using FirebirdSql.Data.FirebirdClient;
using PackagingMachine.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackagingMachine
{
    public partial class Form1 : Form
    {
        private DataSet dsPrescription;
        private DataSet dsPrescriptionDetail;
        private DataView dvDetail;
        public Form1()
        {
            InitializeComponent();

            dsPrescription = new DataSet();
            dsPrescriptionDetail = new DataSet();
            dvDetail = new DataView();
        }

        private void btnQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QueryData();
        }

        private void btnSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //string id = DateTime.Now.ToString("yyyyMMddHHmmss");
            //List<Prescription> listPrescription = new List<Prescription>();
            //Prescription prescription = new Prescription();
            //prescription.Id = id;
            //prescription.Registerid = "123456";
            //prescription.Name = "lxh";
            //prescription.Sex = "男";
            //prescription.Age = 30;
            //prescription.Tele = "";
            //prescription.Email = "";
            //prescription.DepartmentName = "";
            //prescription.DoctorName = "lxh";
            //prescription.PrescriptionName = "";
            //prescription.PrescribeTime = DateTime.Now;
            //prescription.Valuesn = "";
            //prescription.Valuername = "lxh";
            //prescription.Valuationtime = DateTime.Now;
            //prescription.Price = 100;
            //prescription.Quantity = 10;
            //prescription.Quantityday = 10;
            //prescription.Pricetotal = 100;
            //prescription.Paymenttype = "自费";
            //prescription.Paymentstatus = "PAYED";
            //prescription.Datasource = "HIS";
            //prescription.Processstatus = "NEW";
            //prescription.Description = "";
            //listPrescription.Add(prescription);

            //List<PrescriptionDetail> listPrescriptionDetail = new List<PrescriptionDetail>();
            //PrescriptionDetail prescriptionDetail = new PrescriptionDetail();
            //prescriptionDetail.ID = id;
            //prescriptionDetail.No = 1;
            //prescriptionDetail.Granuleid = "123456";
            //prescriptionDetail.Granulename = "阿胶";
            //prescriptionDetail.Doseherb = 20;
            //prescriptionDetail.Equivalent = 0;
            //prescriptionDetail.Dose = 0;
            //prescriptionDetail.Price = 10;
            //listPrescriptionDetail.Add(prescriptionDetail);

            if (dsPrescription.Tables.Count <= 0 || dsPrescription.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("请输入正确的处方号！");
                return;
            }

            DataRow[] drArray = dsPrescription.Tables[0].Select("ISSELECT = true");
            if (drArray.Length <= 0)
            {
                MessageBox.Show("请选择需要发送的处方！");
                return;
            }

            List<Prescription> listPrescription = new List<Prescription>();
            List<PrescriptionDetail> listPrescriptionDetail = new List<PrescriptionDetail>();


            foreach (DataRow dr in drArray)
            {
                Prescription prescription = new Prescription();
                prescription.Id = dr["ID"].ToString();
                prescription.Registerid = dr["REGISTER_ID"].ToString();
                prescription.Name = dr["NAME"].ToString();
                prescription.Sex = dr["SEX"].ToString();

                try
                {
                    if (dr["AGE"].ToString().Trim() == string.Empty)
                    {
                        prescription.Age = 1;
                    }
                    else
                    {
                        prescription.Age = Convert.ToDecimal(Regex.Replace(dr["AGE"].ToString().Trim(), @"\D", ""));//Convert.ToDecimal(dr["AGE"]);
                    }
                }
                catch
                {
                    MessageBox.Show("AGE 年龄数据错误！");
                    return;
                }

                prescription.Tele = dr["TELE"].ToString();
                prescription.Email = dr["EMAIL"].ToString();
                prescription.DepartmentName = dr["DEPARTMENT_NAME"].ToString();
                prescription.DoctorName = dr["DOCTOR_NAME"].ToString();
                prescription.PrescriptionName = "";// dr["PRESCRIPTION_NAME"].ToString();
                prescription.PrescribeTime = DateTime.Now;//Convert.ToDateTime(dr["PRESCRIBE_TIME"]);
                prescription.Valuesn = "";// dr["VALUE_SN"].ToString();
                prescription.Valuername = "";// dr["VALUER_NAME"].ToString();
                prescription.Valuationtime = DateTime.Now; //Convert.ToDateTime(dr["VALUATION_TIME"]);

                try
                {
                    if (dr["PRICE"].ToString().Trim() == string.Empty)
                    {
                        prescription.Price = 1;
                    }
                    else
                    {
                        prescription.Price = Convert.ToDecimal(dr["PRICE"]);
                    }
                }
                catch
                {
                    MessageBox.Show("PRICE 处方单剂价格数据错误！");
                    return;
                }

                try
                {
                    if (dr["QUANTITY"].ToString().Trim() == string.Empty)
                    {
                        prescription.Quantity = 1;
                    }
                    else
                    {
                        prescription.Quantity = Convert.ToDecimal(dr["QUANTITY"]);
                    }
                }
                catch
                {
                    MessageBox.Show("QUANTITY 剂数（袋数）数据错误！");
                    return;
                }

                try
                {
                    if (dr["QUANTITY_DAY"].ToString().Trim() == string.Empty)
                    {
                        prescription.Quantityday = 1;
                    }
                    else
                    {
                        prescription.Quantityday = Convert.ToDecimal(dr["QUANTITY_DAY"]);
                    }
                }
                catch
                {
                    MessageBox.Show("QUANTITY_DAY 剂数（付数）数据错误！");
                    return;
                }

                try
                {
                    if (dr["PRICE_TOTAL"].ToString().Trim() == string.Empty)
                    {
                        prescription.Pricetotal = 1;
                    }
                    else
                    {
                        prescription.Pricetotal = Convert.ToDecimal(dr["PRICE_TOTAL"]);
                    }
                }
                catch
                {
                    MessageBox.Show("PRICE_TOTAL 处方总价数据错误！");
                    return;
                }

                prescription.Paymenttype = dr["PAYMENT_TYPE"].ToString();
                prescription.Paymentstatus = "PAYED";
                prescription.Datasource = "HIS";
                prescription.Processstatus = "NEW";
                prescription.Description = "";//dr["DESCRIPTION"].ToString();
                listPrescription.Add(prescription);

                foreach (DataRow drDetail in dsPrescriptionDetail.Tables[0].Select("ID = '" + dr["ID"].ToString() + "'"))
                {

                    PrescriptionDetail prescriptionDetail = new PrescriptionDetail();
                    prescriptionDetail.ID = drDetail["ID"].ToString();
                    prescriptionDetail.No = Convert.ToInt32(drDetail["NO"]);
                    prescriptionDetail.Granuleid = drDetail["GRANULE_ID"].ToString();
                    prescriptionDetail.Granulename = drDetail["GRANULE_NAME"].ToString();

                    try
                    {
                        if (drDetail["DOSE_HERB"].ToString().Trim() == string.Empty)
                        {
                            prescriptionDetail.Doseherb = 1;
                        }
                        else
                        {
                            prescriptionDetail.Doseherb = Convert.ToDecimal(drDetail["DOSE_HERB"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("DOSE_HERB 饮片剂量数据错误！");
                        return;
                    }

                    try
                    {
                        if (drDetail["EQUIVALENT"].ToString().Trim() == string.Empty)
                        {
                            prescriptionDetail.Equivalent = 0;
                        }
                        else
                        {
                            prescriptionDetail.Equivalent = Convert.ToDecimal(drDetail["EQUIVALENT"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("EQUIVALENT 当量数据错误！");
                        return;
                    }

                    try
                    {
                        if (drDetail["DOSE"].ToString().Trim() == string.Empty)
                        {
                            prescriptionDetail.Dose = 0;
                        }
                        else
                        {
                            prescriptionDetail.Dose = Convert.ToDecimal(drDetail["DOSE"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("DOSE 颗粒剂量数据错误！");
                        return;
                    }

                    try
                    {
                        if (drDetail["PRICE"].ToString().Trim() == string.Empty)
                        {
                            prescriptionDetail.Price = 1;
                        }
                        else
                        {
                            prescriptionDetail.Price = Convert.ToDecimal(drDetail["PRICE"]);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("PRICE 颗粒价格数据错误！");
                        return;
                    }

                    listPrescriptionDetail.Add(prescriptionDetail);
                }
            }

            try
            {
                FirebirdAccess firebirdAccess = new FirebirdAccess();
                firebirdAccess.SavePrescription(listPrescription, listPrescriptionDetail);
                MessageBox.Show("发送成功!");
                timer1.Start();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtCfh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                QueryData();
            }
        }

        private void QueryData()
        {
            if (txtCfh.Text.Trim() == string.Empty)
            {
                try
                {
                    SqlAccess sqlAccess = new SqlAccess();
                    dsPrescription = sqlAccess.QueryPrescriptionAll();

                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "ISSELECT";
                    dc.DataType = typeof(System.Boolean);
                    dc.DefaultValue = false;
                    dsPrescription.Tables[0].Columns.Add(dc);

                    gridControl2.DataSource = dsPrescription.Tables[0];

                    if (dsPrescription.Tables[0].Rows.Count > 0)
                    {
                        dsPrescriptionDetail = sqlAccess.QueryPrescriptionDetailAll();
                        dvDetail = dsPrescriptionDetail.Tables[0].DefaultView;
                        gridControl1.DataSource = dvDetail;
                    }

                    gridView2_Click(null, null);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                txtCfh.Text = txtCfh.Text.Trim().PadLeft(10, '0');
                try
                {
                    SqlAccess sqlAccess = new SqlAccess();
                    dsPrescription = sqlAccess.QueryPrescriptionByCfh(txtCfh.Text);

                    DataColumn dc = new DataColumn();
                    dc.ColumnName = "ISSELECT";
                    dc.DataType = typeof(System.Boolean);
                    dc.DefaultValue = false;
                    dsPrescription.Tables[0].Columns.Add(dc);

                    gridControl2.DataSource = dsPrescription.Tables[0];

                    if (dsPrescription.Tables[0].Rows.Count > 0)
                    {
                        dsPrescriptionDetail = sqlAccess.QueryPrescriptionDetailByCfh(txtCfh.Text);
                        dvDetail = dsPrescriptionDetail.Tables[0].DefaultView;
                        gridControl1.DataSource = dvDetail;
                    }

                    gridView2_Click(null, null);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dsPrescription = new DataSet();
            dsPrescriptionDetail = new DataSet();

            timer1.Interval = Convert.ToInt32(Settings.Default.RefreshInterval) * 1000;
            timer1.Start();
        }

        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                DataRow drSelect = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                if (Convert.ToBoolean(drSelect["ISSELECT"]) == true)
                {
                    drSelect["ISSELECT"] = false;
                }
                else
                {
                    drSelect["ISSELECT"] = true;
                }

                timer1.Stop();
            }
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                DataRow drSelect = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                dvDetail.RowFilter = "ID = '" + drSelect["ID"].ToString() + "'";
            }
        
       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            QueryData();
        }
    }
}
