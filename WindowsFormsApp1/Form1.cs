using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            int j = 0;
            //for (int i = DateTime.Now.Year; i >= DateTime.Now.Year-5; i--)
            //{
            //    cmbiller.Items.Insert(j, i);
            //    j++;
            //}
            
           
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
                string qebznom = "";
                string aa = dr["ReceiptNumber"].ToString();
                qebznom = klas.getdatacell(@"select Qebz from HOPodenis where Qebz='" + dr["ReceiptNumber"] + "'");
                if (qebznom == null || qebznom == "")
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
                    dt = new DataTable();
                }
            }

            label1.Text = "ugurla gonderildi";


        }


     

      

     


        private void btnhesabla_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 725000;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork_1(object sender, DoWorkEventArgs e)
        {
           
            //SqlConnection baglan = klas.baglan();
            DataTable dtodeyiciler = klas.getdatatable(
                @"select distinct t.TaxpayerID from Taxpayer t inner join 
(
   select TaxpayerID,RegistrDate from viewLivingProperty where ExitDate is null 
   union select TaxpayerID,RegistrDate  from viewQLivingProperty  where ExitDate is null 
   union  select TaxpayerID,RegistrDate  from viewWaterAirTransport where ExitDate is null
   union select TaxpayerID,RegistrDate  from ViewLivingLand where ExitDate is null 
   union select TaxpayerID,RegistrDate  from ViewQLivingLand where ExitDate is null 
   union select TaxpayerID,RegistrDate from ViewVillageLand where ExitDate is null
)
 la on la.TaxpayerID=t.TaxpayerID left join Payments ps on t.TaxpayerID=ps.TaxpayerID
where fordelete=1 and Individual_Legal=1 and year(la.RegistrDate)<=2019
  and t.TaxpayerID not in (
 select plk.TaxpayerID from 
 (select distinct pfg.TaxpayerID,pfg.TaxesPaymentID  from Payments pfg inner join Taxpayer oo on oo.TaxpayerID=pfg.TaxpayerID 
 where pfg.Operation=3 and pfg.NowTime=CONVERT(nvarchar(20),2019)+'-08-15 00:00:00.000' and t.municipalID=oo.municipalID) plk 
 inner join 
 (select distinct pfg.TaxpayerID,pfg.TaxesPaymentID  from Payments pfg inner join Taxpayer oo on oo.TaxpayerID=pfg.TaxpayerID 
 where pfg.Operation=2 and pfg.NowTime=CONVERT(nvarchar(20),2019)+'-11-15 00:00:00.000' and t.municipalID=oo.municipalID ) kls 
 on plk.TaxpayerID=kls.TaxpayerID and plk.TaxesPaymentID=kls.TaxesPaymentID) order by  t.TaxpayerID ");
            Hesablanmalar hesabla = new Hesablanmalar();

            for (int i = 0; i < dtodeyiciler.Rows.Count; i++)
            {
                Thread.Sleep(100);
                backgroundWorker.ReportProgress(i);
            
                string buil = klas.getdatacell(@"select year(getdate())");
                hesabla.hesab08_11emlaktorpaq("1", dtodeyiciler.Rows[i]["TaxpayerID"].ToString(), "08", buil);
                hesabla.hesab08_11emlaktorpaq("1", dtodeyiciler.Rows[i]["TaxpayerID"].ToString(), "11", buil);

          

                hesabla.hesab08_11emlaktorpaq("2", dtodeyiciler.Rows[i]["TaxpayerID"].ToString(), "08", buil);
                hesabla.hesab08_11emlaktorpaq("2", dtodeyiciler.Rows[i]["TaxpayerID"].ToString(), "11", buil);
                //               SqlCommand cmd = new SqlCommand(@"
                //exec hesab08_11emlaktorpaq 1,@TaxpayerID,'08',@hesabatili 
                //exec hesab08_11emlaktorpaq 1,@TaxpayerID,'11',@hesabatili  
                //exec hesab08_11emlaktorpaq 2,@TaxpayerID,'08' ,@hesabatili 
                //exec hesab08_11emlaktorpaq 2,@TaxpayerID,'11' ,@hesabatili ", baglan);

                //               cmd.Parameters.Add(new SqlParameter("TaxpayerID", dtodeyiciler.Rows[i]["TaxpayerID"]));
                //               cmd.Parameters.Add(new SqlParameter("hesabatili",  2019));
                //               cmd.CommandTimeout = 10000000;
                //               cmd.ExecuteNonQuery();
            }
            e.Result = dtodeyiciler.Rows.Count;
            label1.Text = "basa catdi hesablama";
        }

        private void backgroundWorker_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundWorker_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO: do something with final calculation.
            if(e.Error!=null)
            {
                label1.Text = e.Error.Message;
            }
        }
    }
}
