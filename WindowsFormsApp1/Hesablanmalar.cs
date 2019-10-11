using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    class Hesablanmalar
    {
        Class2 klas = new Class2();
        public Hesablanmalar()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void hesab08_11emlaktorpaq(string verginov, string TaxpayerID, string vaxt08ve11, string year)
        {
            SqlConnection baglan = klas.baglan();
            SqlCommand cmd = new SqlCommand(@"exec hesab08_11emlaktorpaq " + verginov + "," + TaxpayerID + ",'" + vaxt08ve11 + "' ," + year + "", baglan);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Connection.Close();
            baglan.Close();
            baglan.Dispose();
        }
        public void CalcToday(string verginov, string TaxpayerID)
        {
            SqlConnection baglan = klas.baglan();
            SqlCommand cmd = new SqlCommand(@"exec CalcToday " + verginov + "," + TaxpayerID, baglan);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Connection.Close();
            baglan.Close();
            baglan.Dispose();
        }
    }
}
