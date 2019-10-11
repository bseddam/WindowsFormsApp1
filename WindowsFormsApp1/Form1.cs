using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        MethotAll municipal = new MethotAll();
        Hesablanmalar hesabla = new Hesablanmalar();
        Class2 klas = new Class2();
        public Form1()
        {
            InitializeComponent();
        }
        static DataTable dt = new DataTable();
        private void btngonder_Click(object sender, EventArgs e)
        {
            DataTable dthop = klas.getdatatable(@"select v.* from vhop v left join 
(select * from Payments p where p.TaxesPaymentOnline=1 and p.Operation=10 ) p on v.Qebz=p.Qebz 
 where  p.Qebz is null  ");

//            DataTable dthop = klas.getdatatable(@"select v.* from (select 
//    h.TaxesPaymentID, h.YVOK, h.Amount, h.NowTime
//  FROM
//vhop as h
//  inner join Taxpayer t on h.YVOK = t.YVOK
//  inner join Payments p on t.TaxpayerID = p.TaxpayerID and p.Amount = h.Amount 
//and CONVERT(date, p.RealTime) = CONVERT(date, h.NowTime) and h.TaxesPaymentID = p.TaxesPaymentID
//  where p.RealTime >= '2019-09-17 00:00:00.000' and p.RealTime < '2019-09-30 00:00:00.000'  and p.TaxesPaymentOnline = 1 and p.Operation = 10) as k
//  right join vhop v on v.TaxesPaymentID = k.TaxesPaymentID and v.YVOK = k.YVOK and v.Amount = k.Amount and v.NowTime = k.NowTime
// where k.NowTime is null and v.NowTime >= '2019-09-17 00:00:00.000' and v.NowTime < '2019-09-30 00:00:00.000'");


            for (int i = 0; i < dthop.Rows.Count; i++)
            {
                NotifyAboutPayment(dthop.Rows[i]["YVOK"].ToString(), dthop.Rows[i]["TaxesPaymentID1"].ToString(),
                    dthop.Rows[i]["Amount"].ToString(), dthop.Rows[i]["Qebz"].ToString(), Convert.ToDateTime(dthop.Rows[i]["Nowtime"]));
            }

            label1.Text = "gonderildi";

        }

        public void NotifyAboutPayment(string yvok,string vergitipi,string mebleg,string receiptNumber,DateTime paydate)
        {
            //var soapRequest = TraceExtension.XmlRequest.OuterXml;
            //System.IO.File.AppendAllText(Settings.Default.LogFileDirectory + "log" + DateTime.Now.ToString() + ".txt", "Request : " + DateTime.Now + soapRequest + Environment.NewLine);

            //SqlConnection sqlcon = new SqlConnection();
            string TaxpayerID;
            //try
            //{
                //sqlcon.ConnectionString = SQLConnectionstring;
                //sqlcon.Open();
                TaxpayerID = klas.getdatacell(@"select TaxpayerID from Taxpayer where YVOK=N'" + yvok + "'");
            if (TaxpayerID != null && TaxpayerID != "")
            {
                municipal.InserTaxes(TaxpayerID, vergitipi, mebleg, receiptNumber, paydate);
            }
            //}
            //catch (Exception ex)
            //{
            //    throw ;
            //}


            //sqlcon.Close();

            //var soapResponse = TraceExtension.XmlResponse.OuterXml;
            //System.IO.File.AppendAllText(Settings.Default.LogFileDirectory + "log" + DateTime.Now.ToString() + ".txt",
            //    "INFO : " + DateTime.Now + soapResponse + Environment.NewLine);

            return;
        }

        private void btngoster_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ("XML|*.xml");
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                XmlDocument docx = new XmlDocument();
                docx.Load(ofd.FileName);
                if (dt.Rows.Count == 0)
                {
                    dt.Columns.Add("SN", typeof(int));
                    dt.Columns.Add("ServiceCode", typeof(int));
                    dt.Columns.Add("AbonCode", typeof(String));
                    dt.Columns.Add("Amount", typeof(decimal));
                    dt.Columns.Add("PaymentDate", typeof(DateTime));
                    dt.Columns.Add("ReceiptNumber", typeof(String));
                    dt.TableName = "Payments";
                }
                int i = 0;
                foreach (XmlNode node in docx.SelectNodes("Data/Msg/Rows/Row"))
                {
                    i++;
                    //MessageBox.Show(node.SelectSingleNode("Amount").InnerText);
                    DataRow dr = dt.NewRow();
                    dr["SN"] = i;
                    dr["ServiceCode"] = node.SelectSingleNode("ServiceCode").InnerText;
                    dr["AbonCode"] = node.SelectSingleNode("AbonCode").InnerText;
                    dr["Amount"] = node.SelectSingleNode("Amount").InnerText;
                    dr["PaymentDate"] = node.SelectSingleNode("PaymentDate").InnerText;
                    dr["ReceiptNumber"] = node.SelectSingleNode("ReceiptNumber").InnerText;
                    dt.Rows.Add(dr);
                }

                dataGridView1.DataSource = dt;


            }
            //label1.Text = name;

        }

        private void btnbazayagonder_Click(object sender, EventArgs e)
        {
            SqlConnection baglan = klas.baglan();
            foreach (DataRow dr in dt.Rows)
            {
                SqlCommand cmd = new SqlCommand(@"Insert into HOPodenis 
(Qebz,YVOK,NowTime,Amount,TaxesPaymentID,ImportDate) 
values (@Qebz,@YVOK,@NowTime,@Amount,@TaxesPaymentID,getdate())", baglan);
                
                cmd.Parameters.Add(new SqlParameter("Qebz", dr["ReceiptNumber"]));
                cmd.Parameters.Add(new SqlParameter("YVOK", dr["AbonCode"]));
                cmd.Parameters.Add(new SqlParameter("NowTime", dr["PaymentDate"]));
                cmd.Parameters.Add(new SqlParameter("Amount", dr["Amount"]));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentID", dr["ServiceCode"]));
                cmd.ExecuteNonQuery();
            }
           
            label1.Text =   "ugurla gonderildi";
        }
    }
}
