using System;
using System.Data;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    class Class2
    {
        // public static SqlConnection SqlConn { get { return new SqlConnection(Config.ConvertString(System.Configuration.ConfigurationManager.ConnectionStrings["DBBusinessTrip"])); } }

        public SqlConnection baglan()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=10.10.192.1;Initial Catalog=Municipal_db;User ID=sa; Password=Bld123456;Connect Timeout=180;Max Pool Size=1024;Pooling=true;");
            //SqlConnection baglan = new SqlConnection(@"Data Source=(local);Initial Catalog=Municipal_db_Test;User ID=sa; Password=Bld123456;Connect Timeout=180;Max Pool Size=1024;Pooling=true;");

            //SqlConnection baglan = new SqlConnection(@"Data Source=(local);Initial Catalog=Municipal_db;Integrated Security=true;Connect Timeout=180;Max Pool Size=1024;Pooling=true;");
            baglan.Open();
            return baglan;
        }
        //public static void MsgBox(string mstx, Page P)
        //{
        //    P.ClientScript.RegisterClientScriptBlock(P.GetType(), "PopupScript", "window.focus(); alert('" + mstx + "');", true);
        //}
        public string ConvertString(object s)
        {
            if (s == null) s = "";
            return Convert.ToString(s);
        }
        public int ConvertInt(object s)
        {
            if (s == null) s = 0;
            return Convert.ToInt32(s);
        }
        public float ConvertFloat(string s)
        {
            if (s == null || s == "") s = "0";

            return float.Parse(ConvertString(s));
        }


        public int cmd(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand cmd = new SqlCommand(sqlcumle, baglan);
            cmd.CommandTimeout = 180;
            cmd.CommandType = CommandType.Text;
            int sonuc = 0;
            try
            {
                sonuc = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message + "(" + sqlcumle + ")");
            }
            cmd.Dispose();
            cmd.Connection.Close();
            baglan.Close();
            baglan.Dispose();
            return (sonuc);
        }
        public DataTable tekrarlamax2(string sutunadi, string sutunadi1, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                string birinci1 = dt1.Rows[m1][sutunadi1].ToString();


                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                }
                else
                {
                    m1 = i + 1;
                }
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                {
                    dt1.Rows[i + 1][sutunadi] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }
        public DataSet getdatagridview(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlDataAdapter dap = new SqlDataAdapter(sqlcumle, baglan);
            dap.SelectCommand.CommandTimeout = 180;
            DataSet ds = new DataSet();
            try
            {
                dap.Fill(ds, "Person_Details");
                SqlCommandBuilder cmbld = new SqlCommandBuilder(dap);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "(" + sqlcumle + ")");
            }
            dap.Dispose();
            baglan.Close();
            baglan.Dispose();
            return ds;
        }
        public DataTable getdatatable(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlDataAdapter dap = new SqlDataAdapter(sqlcumle, baglan);
            dap.SelectCommand.CommandTimeout = 120;
            DataTable dt = new DataTable();
            try
            {
                dap.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "(" + sqlcumle + ")");
            }
            dap.Dispose();
            baglan.Close();
            baglan.Dispose();
            return dt;
        }
        public DataRow GetDataRow(string sqlcumle)
        {
            DataTable dt = getdatatable(sqlcumle);
            if (dt.Rows.Count == 0)
                return null;
            else
                return dt.Rows[0];
        }
        public string getdatacell(string sqlcumle)
        {
            DataTable dt = getdatatable(sqlcumle);
            if (dt.Rows.Count == 0)
                return null;
            else
                return dt.Rows[0][0].ToString();
        }
        public static string vitrin(string vitrin)
        {
            string deger = "";
            if (vitrin == "1")
            {
                deger = "evet";
            }
            else
            {
                deger = "hayir";
            }
            return deger;
        }
        public static string vezife(string uygun)
        {
            string qiymet = "";
            if (uygun == "1")
            {
                qiymet = "Hə";
            }
            else
            {
                qiymet = "Yox";
            }
            return qiymet;
        }
        public static string vitrin2(string vitrin2)
        {
            string deger = "";
            if (vitrin2 == "1")
            {
                deger = "yok.png";
            }
            else
            {
                deger = "onay.png";
            }
            return deger;
        }
        public static string muracietebax(string muracietebax2)
        {
            string deger = "";
            if (muracietebax2 == "130")
            {
                deger = "true";
            }
            else
            {
                deger = "false";
            }
            return deger;
        }
        public static string sozukes(string kes)
        {
            string subString = "";
            if (kes.Length >= 10)
            {
                subString = kes.Substring(0, 10);

            }
            return subString;
        }
        public static string sozukes1(string kes)
        {
            string subString = "";
            if (kes.Length >= 10)
            {
                subString = kes.Substring(0, 5);

            }
            return subString;
        }
        public static string reng(string rengideyiw)
        {
            string deger = "";
            if (rengideyiw != "")
            {
                if (double.Parse(rengideyiw) >= 10)
                {
                    deger = "Blue";
                }
                else
                {
                    deger = "Red";
                }
            }
            return deger;
        }

        public string tarixkesdatetime(string kes)
        {
            string subString = "";
            if (kes.Length >= 10)
            {
                subString = kes.Substring(0, 10);

            }
            return subString;
        }

        public static string notekrar(string notekrar1)
        {
            string deger = "";
            if (notekrar1 == "130")
            {
                deger = "true";
            }
            else
            {
                deger = "false";
            }
            return deger;
        }
        public static string planagirme(string planagirme2)
        {
            string deger = "";
            if (planagirme2 == "1" || planagirme2 == "2" || planagirme2 == "4")
            {
                deger = "false";
            }
            else
            {
                deger = "true";
            }
            return deger;
        }



        public DataTable vergul(string sutunadi, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);

            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                if (i == dt1.Rows.Count - 1)
                {
                    dt1.Rows[i][sn] = ".";
                }
                else
                {
                    dt1.Rows[i][sn] = ", ";
                }
            }
            return dt1;
        }
        public DataTable tekrarlama(string sutunadi, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                {
                    dt1.Rows[i + 1][sutunadi] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }
        public DataTable tekrarlamar1x(string sutunadi, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sutunadi5, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                string birinci1 = dt1.Rows[m1][sutunadi1].ToString();
                string birinci2 = dt1.Rows[m1][sutunadi2].ToString();
                string birinci3 = dt1.Rows[m1][sutunadi3].ToString();

                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString() && birinci2 == dt1.Rows[i + 1][sutunadi2].ToString() && birinci3 == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi2] = "";
                    dt1.Rows[i + 1][sutunadi4] = "";
                    dt1.Rows[i + 1][sutunadi5] = "";
                }
                else
                {
                    m1 = i + 1;
                }
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                {
                    dt1.Rows[i + 1][sutunadi] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }
        public DataTable tekrarlamax1(string sutunadi, string sutunadi1, string sutunadi2, string sutunadi3, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                string birinci1 = dt1.Rows[m1][sutunadi1].ToString();
                string birinci2 = dt1.Rows[m1][sutunadi2].ToString();
                string birinci3 = dt1.Rows[m1][sutunadi3].ToString();

                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString() && birinci2 == dt1.Rows[i + 1][sutunadi2].ToString() && birinci3 == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi2] = "";
                }
                else
                {
                    m1 = i + 1;
                }
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                {
                    dt1.Rows[i + 1][sutunadi] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }

        public DataTable tekrarlama01(string sutunadi0, string sutunadi, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dt1.Rows.Count - i; j++)
                {
                    if (dt1.Rows[i][sutunadi4].ToString() == dt1.Rows[i + j][sutunadi4].ToString() && dt1.Rows[i][sutunadi3].ToString() == dt1.Rows[i + j][sutunadi3].ToString() && dt1.Rows[i][sutunadi2].ToString() == dt1.Rows[i + j][sutunadi2].ToString())
                    {
                        dt1.Rows[i + j][sutunadi2] = "";
                    }
                }


                string birinci = dt1.Rows[m][sutunadi].ToString();
                string sifirinci = dt1.Rows[m1][sutunadi4].ToString();
                string ikinci = dt1.Rows[m1][sutunadi2].ToString();
                string ucuncu = dt1.Rows[m1][sutunadi3].ToString();

                //if (sifirinci == dt1.Rows[i + 1][sutunadi4].ToString() && ucuncu == dt1.Rows[i + 1][sutunadi3].ToString() && ikinci == dt1.Rows[i + 1][sutunadi2].ToString())
                //{
                //    dt1.Rows[i + 1][sutunadi0] = "";
                //    dt1.Rows[i + 1][sutunadi3] = "";
                //}
                //else
                //{
                //    m1 = i + 1;
                //}



                if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                {
                    dt1.Rows[i + 1][sutunadi] = "";
                    dt1.Rows[i + 1][sutunadi1] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }
        public DataTable tekrarlama011(string sutunadi0, string sutunadi, string sutunadi1, string sutunadi2, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dt1.Rows.Count - i; j++)
                {
                    if (dt1.Rows[i][sutunadi].ToString() == dt1.Rows[i + j][sutunadi].ToString() && dt1.Rows[i][sutunadi1].ToString() == dt1.Rows[i + j][sutunadi1].ToString() && dt1.Rows[i][sutunadi2].ToString() == dt1.Rows[i + j][sutunadi2].ToString())
                    {
                        dt1.Rows[i + j][sutunadi] = "";
                    }
                }


                //string birinci = dt1.Rows[m][sutunadi].ToString();

                //string ikinci = dt1.Rows[m1][sutunadi2].ToString();


                //if (sifirinci == dt1.Rows[i + 1][sutunadi4].ToString() && ucuncu == dt1.Rows[i + 1][sutunadi3].ToString() && ikinci == dt1.Rows[i + 1][sutunadi2].ToString())
                //{
                //    dt1.Rows[i + 1][sutunadi0] = "";
                //    dt1.Rows[i + 1][sutunadi3] = "";
                //}
                //else
                //{
                //    m1 = i + 1;
                //}



                //if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                //{
                //    dt1.Rows[i + 1][sutunadi] = "";
                //    dt1.Rows[i + 1][sutunadi1] = "";
                //}
                //else
                //{
                //    index++;
                //    m = i + 1;
                //    dt1.Rows[m][sn] = index + ".";
                //}
            }
            return dt1;
        }

        public DataTable tekrarlama1(string sutunadi0, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sutunadi5, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dt1.Rows.Count - i; j++)
                {
                    if (dt1.Rows[i][sutunadi5].ToString() == dt1.Rows[i + j][sutunadi5].ToString())
                    {
                        dt1.Rows[i + j][sutunadi4] = "";
                    }
                }

                string birinci = dt1.Rows[m][sutunadi0].ToString();
                string birinci1 = dt1.Rows[m1][sutunadi1].ToString();
                string birinci2 = dt1.Rows[m1][sutunadi2].ToString();
                string birinci3 = dt1.Rows[m1][sutunadi3].ToString();
                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString() && birinci2 == dt1.Rows[i + 1][sutunadi2].ToString() && birinci3 == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi2] = "";
                }
                else
                {
                    m1 = i + 1;
                }
                if (birinci3 == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi0] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }
        public DataTable tekrarlama2(string sutunadi0, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sutunadi5, string sutunadi6, string sutunadi7, string sutunadi8, string sutunadi9, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";


            int m = 0; int m1 = 0; int m2 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dt1.Rows.Count - i; j++)
                {
                    if (dt1.Rows[i][sutunadi4].ToString() == dt1.Rows[i + j][sutunadi4].ToString())
                    {
                        dt1.Rows[i + j][sutunadi6] = "";
                    }
                    if (dt1.Rows[i][sutunadi7].ToString() == dt1.Rows[i + j][sutunadi7].ToString())
                    {
                        dt1.Rows[i + j][sutunadi8] = "";
                    }
                }





                string birinci = dt1.Rows[m][sutunadi0].ToString();
                string birinci1 = dt1.Rows[m1][sutunadi1].ToString();
                string birinci2 = dt1.Rows[m1][sutunadi2].ToString();
                string birinci3 = dt1.Rows[m1][sutunadi3].ToString();
                string birinci4 = dt1.Rows[m1][sutunadi4].ToString();
                string birinci5 = dt1.Rows[m1][sutunadi5].ToString();
                string birinci6 = dt1.Rows[m1][sutunadi6].ToString();
                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString() && birinci2 == dt1.Rows[i + 1][sutunadi2].ToString() && birinci3 == dt1.Rows[i + 1][sutunadi3].ToString() && birinci4 == dt1.Rows[i + 1][sutunadi4].ToString() && birinci5 == dt1.Rows[i + 1][sutunadi5].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi2] = "";
                    dt1.Rows[i + 1][sutunadi4] = "";
                    dt1.Rows[i + 1][sutunadi5] = "";
                    dt1.Rows[i + 1][sutunadi6] = "";
                }
                else
                {
                    m1 = i + 1;
                }
                if (birinci3 == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi0] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }


                string b0 = dt1.Rows[m2][sutunadi3].ToString();
                string b2 = dt1.Rows[m2][sutunadi9].ToString();
                if (b0 == dt1.Rows[i + 1][sutunadi3].ToString() && b2 == dt1.Rows[i + 1][sutunadi9].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi2] = "";
                }
                else
                {
                    m2 = i + 1;
                }
            }
            return dt1;


        }
        public DataTable tekrarlama20(string sutunadi0, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sutunadi5, string sutunadi6, string sutunadi7, string sutunadi8, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";


            int m3 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dt1.Rows.Count - i; j++)
                {
                    if (dt1.Rows[i][sutunadi3].ToString() == dt1.Rows[i + j][sutunadi3].ToString() && dt1.Rows[i][sutunadi2].ToString() == dt1.Rows[i + j][sutunadi2].ToString() && dt1.Rows[i][sutunadi1].ToString() == dt1.Rows[i + j][sutunadi1].ToString())
                    {
                        dt1.Rows[i + j][sutunadi1] = "";
                    }
                    if (dt1.Rows[i][sutunadi4].ToString() == dt1.Rows[i + j][sutunadi4].ToString())
                    {
                        dt1.Rows[i + j][sutunadi6] = "";
                    }
                    if (dt1.Rows[i][sutunadi7].ToString() == dt1.Rows[i + j][sutunadi7].ToString())
                    {
                        dt1.Rows[i + j][sutunadi8] = "";
                    }

                }
                string a0 = dt1.Rows[m3][sutunadi4].ToString();

                if (a0 == dt1.Rows[i + 1][sutunadi4].ToString())
                {
                    dt1.Rows[i + 1][sutunadi4] = "";
                }
                else
                {
                    m3 = i + 1;
                }






            }
            return dt1;
        }
        public DataTable tekrarlama21(string sutunadi0, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sutunadi5, string sutunadi6, string sutunadi7, string sutunadi8, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";


            int m3 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dt1.Rows.Count - i; j++)
                {
                    if (dt1.Rows[i][sutunadi3].ToString() == dt1.Rows[i + j][sutunadi3].ToString() && dt1.Rows[i][sutunadi2].ToString() == dt1.Rows[i + j][sutunadi2].ToString() && dt1.Rows[i][sutunadi1].ToString() == dt1.Rows[i + j][sutunadi1].ToString())
                    {
                        dt1.Rows[i + j][sutunadi1] = "";
                    }
                    if (dt1.Rows[i][sutunadi4].ToString() == dt1.Rows[i + j][sutunadi4].ToString())
                    {
                        dt1.Rows[i + j][sutunadi6] = "";
                    }
                    if (dt1.Rows[i][sutunadi7].ToString() == dt1.Rows[i + j][sutunadi7].ToString())
                    {
                        dt1.Rows[i + j][sutunadi8] = "";
                    }

                }
                string a0 = dt1.Rows[m3][sutunadi7].ToString();

                if (a0 == dt1.Rows[i + 1][sutunadi7].ToString())
                {
                    dt1.Rows[i + 1][sutunadi7] = "";
                }
                else
                {
                    m3 = i + 1;
                }

            }
            return dt1;
        }
        public DataTable tekrarlama3(string sutunadi, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0; int m2 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                string birinci1 = dt1.Rows[m1][sutunadi1].ToString();
                string birinci2 = dt1.Rows[m1][sutunadi2].ToString();
                string birinci3 = dt1.Rows[m2][sutunadi3].ToString();
                string birinci4 = dt1.Rows[m2][sutunadi4].ToString();




                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString() && birinci2 == dt1.Rows[i + 1][sutunadi2].ToString() && birinci3 == dt1.Rows[i + 1][sutunadi3].ToString() && birinci4 == dt1.Rows[i + 1][sutunadi4].ToString())
                {
                    dt1.Rows[i + 1][sutunadi4] = "";
                }
                else
                {
                    m2 = i + 1;
                }


                if (birinci1 == dt1.Rows[i + 1][sutunadi1].ToString() && birinci2 == dt1.Rows[i + 1][sutunadi2].ToString() && birinci3 == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi2] = "";
                }
                else
                {
                    m1 = i + 1;
                }
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString())
                {
                    dt1.Rows[i + 1][sutunadi] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }
            }
            return dt1;
        }

        public DataTable tekrarlamaxerc(string sutunadi, string sutunadi1, string sutunadi2, string sutunadi3, string sutunadi4, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                string ikinci = dt1.Rows[m][sutunadi1].ToString();
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString() && ikinci == dt1.Rows[i + 1][sutunadi1].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    dt1.Rows[i + 1][sutunadi4] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }

                string ucuncu = dt1.Rows[m1][sutunadi2].ToString();
                string dorduncu = dt1.Rows[m1][sutunadi3].ToString();

                if (ucuncu == dt1.Rows[i + 1][sutunadi2].ToString() && dorduncu == dt1.Rows[i + 1][sutunadi3].ToString())
                {
                    dt1.Rows[i + 1][sutunadi3] = "";
                }
                else
                {
                    index++;
                    m1 = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }


            }
            return dt1;
        }

        public DataTable tekrarlamaxerc(string sutunadi, string sutunadi1, string sutunadi2, string sn, DataTable dt1)
        {
            dt1.Columns.Add(sn);
            int index = 1;
            if (dt1.Rows.Count > 0)
                dt1.Rows[0][sn] = index + ".";

            int m = 0; int m1 = 0;
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                string birinci = dt1.Rows[m][sutunadi].ToString();
                string ikinci = dt1.Rows[m][sutunadi1].ToString();
                if (birinci == dt1.Rows[i + 1][sutunadi].ToString() && ikinci == dt1.Rows[i + 1][sutunadi1].ToString())
                {
                    dt1.Rows[i + 1][sutunadi1] = "";
                    // dt1.Rows[i + 1][sutunadi4] = "";
                }
                else
                {
                    index++;
                    m = i + 1;
                    dt1.Rows[m][sn] = index + ".";
                }

                string ucuncu = dt1.Rows[m1][sutunadi2].ToString();
                // string dorduncu = dt1.Rows[m1][sutunadi3].ToString();

                //if (ucuncu == dt1.Rows[i + 1][sutunadi2].ToString())
                //{
                //    dt1.Rows[i + 1][sutunadi3] = "";
                //}
                //else
                //{
                //    index++;
                //    m1 = i + 1;
                //    dt1.Rows[m][sn] = index + ".";
                //}


            }
            return dt1;
        }
    }
}
