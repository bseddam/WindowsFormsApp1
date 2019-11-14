using System;

using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    class MethotAll
    {
        public MethotAll()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        

        Class2 klas = new Class2();
        Hesablanmalar hesabla = new Hesablanmalar();







       





        public float GetEmlakTorpaq(string verginov, string TaxpayerID)
        {

            DataRow buvaxt = klas.GetDataRow(@"select year(getdate()) buil,Getdate() vaxt");


            if (verginov == "1")
            {
                hesabla.hesab08_11emlaktorpaq("1", TaxpayerID, "08", buvaxt["buil"].ToString());
                hesabla.hesab08_11emlaktorpaq("1", TaxpayerID, "11", buvaxt["buil"].ToString());
                hesabla.CalcToday("1", TaxpayerID);
                float umumiborc = 0;
                DataRow drinsertsora = klas.GetDataRow(@"select mt.TaxpayerId,  case when 
            (select top 1 RemainingDebt from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) is null then 0 
            else (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) 
       end  qaliq,       
       case when 
            (select top 1 morepayment from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) is null then 0 
         
            else (select top 1 morepayment from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) 
       end  artiqodeme,
       case when (case when 
            (select top 1 RemainingDebt from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) is null then 0 
            else (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) 
       end=0) then 0
       else
       DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID = 1 order by PaymentID desc),GETDATE() )
       end as yenigday,
       DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc),GETDATE())*
       (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc)/1000 as yenifaiz,
       (DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc),GETDATE())*
        (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc)/1000) 
        +
        (select top 1 PercentDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) as yenifaizqaliq,
       (select top 1 sanction from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 1 order by PaymentID desc) as sanction
 from CalcTaxes mt 
  where mt.TaxesType=1 and mt.TaxpayerID=" + TaxpayerID + " and CalcYear=YEAR(GETDATE()) group by mt.TaxpayerId");

                if (drinsertsora != null)
                {

                    float tq = float.Parse(drinsertsora["qaliq"].ToString());
                    float tart = float.Parse(drinsertsora["artiqodeme"].ToString());
                    float tfaiz = 0;
                    if (drinsertsora["yenifaizqaliq"] != null && drinsertsora["yenifaizqaliq"].ToString() != "")
                    {
                        tfaiz = float.Parse(drinsertsora["yenifaizqaliq"].ToString());
                    }

                    float tsanksia = 0;
                    if (drinsertsora["sanction"] != null && drinsertsora["sanction"].ToString() != "")
                    {
                        tsanksia = float.Parse(drinsertsora["sanction"].ToString());
                    }

                    DataRow dremlak = klas.GetDataRow(@"select Series,SanctionTax,CalcID,TaxpayerID,CalcYear,AmountTax,cast(AmountTax/2 as numeric(15,2)) as yarisi
from CalcTaxes where TaxpayerID=" + TaxpayerID + " and CalcYear<=Year(getdate()) and TaxesType=1 order by CalcYear desc");


                    float emlakv = 0;
                    if (dremlak != null)
                    {
                        emlakv = float.Parse(dremlak["AmountTax"].ToString());
                    }

                    float eo08 = emlakv / 2;
                    float eo11 = emlakv / 2;


                    //string datelastinsert = klas.getdatacell("Select Getdate()");


                    if (Convert.ToDateTime(buvaxt["vaxt"].ToString()) >= Convert.ToDateTime("15.08." + buvaxt["buil"].ToString()) && Convert.ToDateTime(buvaxt["vaxt"].ToString()) <= Convert.ToDateTime("15.11." + buvaxt["buil"].ToString()))
                    {

                        umumiborc = eo11 + tq + tfaiz + tsanksia - tart;
                    }
                    else if (Convert.ToDateTime(buvaxt["vaxt"].ToString()) < Convert.ToDateTime("15.08." + buvaxt["buil"].ToString()))
                    {
                        umumiborc = eo11 + eo08 + tq + tfaiz + tsanksia - tart;
                    }
                    else if (Convert.ToDateTime(buvaxt["vaxt"].ToString()) >= Convert.ToDateTime("15.11." + buvaxt["buil"].ToString()))
                    {

                        umumiborc = tq + tfaiz + tsanksia - tart;
                    }
                }
                return -float.Parse(System.Math.Round(umumiborc, 2, MidpointRounding.AwayFromZero).ToString());

            }
            // torpaq vergisi

            else if (verginov == "2")
            {
                hesabla.hesab08_11emlaktorpaq("2", TaxpayerID, "08", buvaxt["buil"].ToString());
                hesabla.hesab08_11emlaktorpaq("2", TaxpayerID, "11", buvaxt["buil"].ToString());

                hesabla.CalcToday("2", TaxpayerID);


                float umumiborc = 0;
                DataRow drinsertsora = klas.GetDataRow(@"select mt.TaxpayerId,  case when 
            (select top 1 RemainingDebt from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) is null then 0 
            else (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (2) order by PaymentID desc) 
       end  qaliq,
       
       case when 
            (select top 1 morepayment from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) is null then 0 
         
            else (select top 1 morepayment from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) 
       end  artiqodeme,
       case when (case when 
            (select top 1 RemainingDebt from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) is null then 0 
            else (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) 
       end=0) then 0
       else
       DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID = 2 order by PaymentID desc),GETDATE() )
       end as yenigday,
       DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc),GETDATE())*
       (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc)/1000 as yenifaiz,
       (DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  =2 order by PaymentID desc),GETDATE())*
        (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc)/1000) 
        +
        (select top 1 PercentDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) as yenifaizqaliq,
       (select top 1 sanction from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  = 2 order by PaymentID desc) as sanction
 from CalcTaxes mt 
  where mt.TaxesType=2 and mt.TaxpayerID=" + TaxpayerID + " and CalcYear=YEAR(GETDATE()) group by mt.TaxpayerId");
                if (drinsertsora != null)
                {

                    float tq = float.Parse(drinsertsora["qaliq"].ToString());
                    float tart = float.Parse(drinsertsora["artiqodeme"].ToString());
                    float tfaiz = 0;
                    if (drinsertsora["yenifaizqaliq"] != null && drinsertsora["yenifaizqaliq"].ToString() != "")
                    {
                        tfaiz = float.Parse(drinsertsora["yenifaizqaliq"].ToString());
                    }

                    float tsanksia = 0;
                    if (drinsertsora["sanction"] != null && drinsertsora["sanction"].ToString() != "")
                    {
                        tsanksia = float.Parse(drinsertsora["sanction"].ToString());
                    }

                    DataRow drtorp = klas.GetDataRow(@"select Series,SanctionTax,CalcID,TaxpayerID,CalcYear,AmountTax,cast(AmountTax/2 as numeric(15,2)) as yarisi
from CalcTaxes where TaxpayerID=" + TaxpayerID + " and CalcYear=Year(getdate()) and TaxesType=2 order by CalcYear desc");


                    float torpaqv = 0;
                    if (drtorp != null)
                    {
                        torpaqv = float.Parse(drtorp["AmountTax"].ToString());
                    }

                    float eo08 = torpaqv / 2;
                    float eo11 = torpaqv / 2;

                    //string buvaxt["vaxt"].ToString() = klas.getdatacell("Select Getdate()");

                    if (Convert.ToDateTime(buvaxt["vaxt"].ToString()) >= Convert.ToDateTime("15.08." + buvaxt["buil"].ToString()) && Convert.ToDateTime(buvaxt["vaxt"].ToString()) <= Convert.ToDateTime("15.11." + buvaxt["buil"].ToString()))
                    {

                        umumiborc = eo11 + tq + tfaiz + tsanksia - tart;
                    }
                    else if (Convert.ToDateTime(buvaxt["vaxt"].ToString()) < Convert.ToDateTime("15.08." + buvaxt["buil"].ToString()))
                    {
                        umumiborc = eo11 + eo08 + tq + tfaiz + tsanksia - tart;
                    }
                    else if (Convert.ToDateTime(buvaxt["vaxt"].ToString()) >= Convert.ToDateTime("15.11." + buvaxt["buil"].ToString()))
                    {
                        umumiborc = tq + tfaiz + tsanksia - tart;
                    }
                    return -float.Parse(System.Math.Round(umumiborc, 2, MidpointRounding.AwayFromZero).ToString());
                }

            }


            return 0;

        }

        public float GetOdenishAmount(string verginow, string TaxpayerID)
        {
            float qaliqborc = 0;
            if (verginow == "7")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when Amount is null then 0 else Amount end )-
case when (select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=7) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID and p.TaxesPaymentID=7) end  borc
  from MineTax mt inner join CalcMine ct on mt.MineId=ct.MineID where mt.TaxpayerID=" + TaxpayerID + " group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "14")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when Amount is null then 0 else Amount end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=14) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=14) end  borc
  from ProfitsTax mt inner join CalcProfits ct on mt.IncomeTaxID=ct.ProfitsID where mt.TaxpayerID=" + TaxpayerID +
            " group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "9")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when mebleg is null then 0 else mebleg end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=9) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=9) end  borc
  from ViewAdvertisement mt where mt.TaxpayerID=" + TaxpayerID + " group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "10")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when Amount is null then 0 else Amount end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=10) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=10) end  borc
  from CarStop mt inner join CalcCarStop ct on mt.CarID=ct.CarStopID where mt.TaxpayerID=" + TaxpayerID + " group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "11")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when Amount is null then 0 else Amount end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=11) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=11) end  borc
  from Hotel mt inner join CalcHotel ct on mt.HotelID=ct.HotelID where mt.TaxpayerID=" + TaxpayerID + "  group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "12")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when mebleg is null then 0 else mebleg end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=12) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=12) end  borc
  from viewTradeService mt  where mt.TaxpayerID=" + TaxpayerID + " group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "13")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when AmountOnContract is null then 0 else AmountOnContract end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=13) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=13) end  borc
  from Alienation mt  where mt.TaxpayerID=" + TaxpayerID + "  group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            else if (verginow == "8")
            {
                DataRow drborc = klas.GetDataRow(@"select mt.TaxpayerId,sum(case when mebleg is null then 0 else mebleg end )-
case when 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=8) is null then 0 else 
(select sum(case when Amount is null then 0 else Amount end) from Payments p where p.TaxpayerID=mt.TaxpayerID
and p.TaxesPaymentID=8) end  borc
  from ViewLivingAreaLisee mt  where mt.TaxpayerID=" + TaxpayerID + "  group by mt.TaxpayerId");
                if (drborc != null)
                {
                    qaliqborc = float.Parse(drborc["borc"].ToString());
                }
            }
            return qaliqborc;
        }
        public void InserTaxes(string TaxpayerID, string taxestype, string amount, string receiptNumber, DateTime paydate)
        {
            //try
            //{
            SqlConnection baglan = klas.baglan();
            float qaliqborc = 0;
            if (taxestype == "20000200")
            {
                taxestype = "1";
            }
            else if (taxestype == "20000100")
            {
                taxestype = "2";
            }
            else if (taxestype == "20000300")
            {
                taxestype = "7";
            }
            else if (taxestype == "20001000")
            {
                taxestype = "8";
            }
            else if (taxestype == "20000500")
            {
                taxestype = "9";
            }
            else if (taxestype == "20000800")
            {
                taxestype = "10";
            }
            else if (taxestype == "20000700")
            {
                taxestype = "11";
            }
            else if (taxestype == "20000600")
            {
                taxestype = "12";
            }
            else if (taxestype == "20000900")
            {
                taxestype = "13";
            }
            else if (taxestype == "20000400")
            {
                taxestype = "14";
            }
            else if (taxestype == "20001100")
            {
                taxestype = "15";
            }



            if (taxestype == "1" || taxestype == "2")
            {
                DataRow drqaliq1 = klas.GetDataRow(@" select mt.TaxpayerId,  case when 
(select top 1 RemainingDebt from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) is null then 0 " +
  "       else (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) " +
@"    end  qaliq, " +
     "   case when " +
 "        (select top 1 morepayment from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) is null then 0 " +
 "        else (select top 1 morepayment from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) " +
"    end  artiqodeme, " +
"     case when (case when  " +
  "       (select top 1 RemainingDebt from Payments  p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) is null then 0 " +
  "       else (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) " +
 "   end=0) then 0 " +
 "   else " +
 "   DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID in (" + taxestype + ") order by PaymentID desc),Convert(datetime,'" + paydate + "',103)) " +
 " end as yenigday," +
 "  DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc),Convert(datetime,'" + paydate + "',103))*" +
 @"  (case when 
            (select sum(t1.Amount) from (select top 2 sr.Amount from Payments sr where sr.Operation in (2,3) and  sr.TaxesPaymentID in (" + taxestype + @") and sr.TaxpayerID=mt.TaxpayerID order by sr.NowTime desc) as t1)<
            (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + @") order by PaymentID desc) then
            (select sum(t1.Amount) from (select top 2 sr.Amount from Payments sr where sr.Operation in (2,3) and  sr.TaxesPaymentID in (" + taxestype + @") and sr.TaxpayerID=mt.TaxpayerID order by sr.NowTime desc) as t1) else
            (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + @") order by PaymentID desc)
            
           end
            )/1000 as yenifaiz," +
"   (DATEDIFF(DAY,(select top 1 NowTime from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc),Convert(datetime,'" + paydate + "',103))*" +
@"    (case when 
            (select sum(t1.Amount) from (select top 2 sr.Amount from Payments sr where sr.Operation in (2,3) and  sr.TaxesPaymentID in (" + taxestype + @") and sr.TaxpayerID=mt.TaxpayerID order by sr.NowTime desc) as t1)<
            (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + @") order by PaymentID desc) then
            (select sum(t1.Amount) from (select top 2 sr.Amount from Payments sr where sr.Operation in (2,3) and  sr.TaxesPaymentID in (" + taxestype + @") and sr.TaxpayerID=mt.TaxpayerID order by sr.NowTime desc) as t1) else
            (select top 1 RemainingDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + @") order by PaymentID desc)
            
           end
            )/1000) + " +
"    (select top 1 PercentDebt from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) as yenifaizqaliq, " +
"   (select top 1 sanction from Payments p where p.TaxpayerID=mt.TaxpayerID and TaxesPaymentID  in (" + taxestype + ") order by PaymentID desc) as sanction " +
" from CalcTaxes mt " +
"  where mt.TaxpayerID=" + TaxpayerID + " and CalcYear=YEAR(Convert(datetime,'" + paydate + "',103)) group by mt.TaxpayerId");







                SqlCommand cmd = new SqlCommand(@"Insert into Payments (TaxpayerID,TaxesPaymentID,Operation,TaxesPaymentOnline,Amount,RemainingDebt,
PercentDayCount,PercentCounted,PercentDebt,PaymentDocument,NowTime,MorePayment,Sanction,TaxesPaymentName,Qebz) 
values (@TaxpayerID,@TaxesPaymentID,@Operation,@TaxesPaymentOnline,@Amount,@RemainingDebt,@PercentDayCount,@PercentCounted,@PercentDebt,@PaymentDocument,
@NowTime,@MorePayment,@Sanction,@TaxesPaymentName,@Qebz)", baglan);

                cmd.Parameters.Add(new SqlParameter("TaxpayerID", TaxpayerID));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentID", taxestype));
                cmd.Parameters.Add(new SqlParameter("Operation", 10));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentOnline", 1));
                cmd.Parameters.Add(new SqlParameter("NowTime", paydate));
                float a = 0, art = 0; ;
                float f = 0;
                float s = 0;
                float q = 0;
                float kohneqaliq = 0;
                float yenigund = 0;
                float yenifaiz = 0;
                if (drqaliq1 != null)
                {

                    art = float.Parse(drqaliq1["artiqodeme"].ToString());
                    if (drqaliq1["yenifaizqaliq"].ToString() != "")
                        f = float.Parse(drqaliq1["yenifaizqaliq"].ToString());
                    if (drqaliq1["Sanction"].ToString() != "")
                        s = float.Parse(drqaliq1["Sanction"].ToString());
                    if (drqaliq1["qaliq"].ToString() != "")
                        kohneqaliq = float.Parse(drqaliq1["qaliq"].ToString());
                    if (drqaliq1["yenigday"].ToString() != "")
                        yenigund = float.Parse(drqaliq1["yenigday"].ToString());
                    if (drqaliq1["yenifaiz"].ToString() != "")
                        yenifaiz = float.Parse(drqaliq1["yenifaiz"].ToString());
                }
                if (f < 0)
                {
                    f = 0;
                    yenifaiz = 0;
                    yenigund = 0;
                }
                a = float.Parse(amount) - kohneqaliq + art;

                if (float.Parse(amount) >= kohneqaliq)
                {

                    q = 0;
                    art = a;
                }
                else
                {
                    q = kohneqaliq - float.Parse(amount);
                    art = 0;
                }
                if (art > q)
                {
                    if (art > f)
                    {
                        art = art - f;
                        f = 0;
                    }
                    else
                    {
                        f = f - art;
                        art = 0;
                    }

                    if (art > s)
                    {
                        art = art - s;
                        s = 0;
                    }
                    else
                    {
                        s = s - art;
                        art = 0;
                    }
                }

                if (float.Parse(amount) >= kohneqaliq)
                {

                    cmd.Parameters.Add(new SqlParameter("RemainingDebt", int.Parse("0")));
                    cmd.Parameters.Add(new SqlParameter("MorePayment", art));
                }
                else
                {
                    a = kohneqaliq - float.Parse(amount);
                    cmd.Parameters.Add(new SqlParameter("RemainingDebt", a));
                    cmd.Parameters.Add(new SqlParameter("MorePayment", int.Parse("0")));
                }
                cmd.Parameters.Add(new SqlParameter("Sanction", s));
                cmd.Parameters.Add(new SqlParameter("Amount", amount));
                cmd.Parameters.Add(new SqlParameter("PercentDayCount", yenigund));
                cmd.Parameters.Add(new SqlParameter("PercentCounted", yenifaiz));
                cmd.Parameters.Add(new SqlParameter("PercentDebt", f));
                cmd.Parameters.Add(new SqlParameter("PaymentDocument", "Online"));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentName", "hopodenis"));
                cmd.Parameters.Add(new SqlParameter("Qebz", receiptNumber));

                cmd.ExecuteNonQuery();
            }
            else
            {
                float b = 0; float art = 0; float artveodenis = 0; float kohneart = 0;
                string ko = "";
                ko = klas.getdatacell(@"select top 1 MorePayment from Payments p where p.TaxpayerID=" +
                    TaxpayerID + " and TaxesPaymentID=" + taxestype + " order by paymentid desc ");
                if (ko != "" && ko != null)
                {
                    kohneart = float.Parse(ko);
                }
                else { kohneart = 0; }

                artveodenis = kohneart + float.Parse(amount);
                if (artveodenis > qaliqborc)
                {
                    art = artveodenis - qaliqborc;
                }
                else
                {
                    b = qaliqborc - float.Parse(amount);
                }

                SqlCommand cmd = new SqlCommand(@"Insert into Payments 
(TaxpayerID,TaxesPaymentID,Amount,PaymentDocument,NowTime,Operation,TaxesPaymentOnline,RemainingDebt,
MorePayment,TaxesPaymentName,Qebz) 
values (@TaxpayerID,@TaxesPaymentID,@Amount,@PaymentDocument,@NowTime,@Operation,
@TaxesPaymentOnline,@RemainingDebt,@MorePayment,@TaxesPaymentName,@Qebz)", baglan);

                cmd.Parameters.Add(new SqlParameter("TaxpayerID", TaxpayerID));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentID", taxestype));
                cmd.Parameters.Add(new SqlParameter("Operation", 10));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentOnline", 1));
                cmd.Parameters.Add(new SqlParameter("RemainingDebt", b));
                cmd.Parameters.Add(new SqlParameter("Amount", amount));
                cmd.Parameters.Add(new SqlParameter("MorePayment", art));
                cmd.Parameters.Add(new SqlParameter("PaymentDocument", "Online"));
                cmd.Parameters.Add(new SqlParameter("NowTime", paydate));
                cmd.Parameters.Add(new SqlParameter("TaxesPaymentName", "hopodenis"));
                cmd.Parameters.Add(new SqlParameter("Qebz", receiptNumber));
                cmd.ExecuteNonQuery();
                //20001100
            }
            baglan.Close();
            //}
            //catch (Exception ex)
            //{
            //   throw;
            //}
        }


        public DataTable GetTaxesObjects(string TaxpayerID, int[] Servicecodelist1)
        {
            DataRow drtediyye0 = klas.GetDataRow(@"Select t.Pincode,t.TaxpayerID,t.SName+' '+t.Name+' '+t.FName fullname,lcm.MunicipalName,lcm.MunicipalID,'92'+lcm.Municipal_code Municipal_code,lcm.AccountNumber,lcm.Bank, 
                 lcr.Name + ' ' +case when lcr.CityID = 1 then N'şəhəri' else 'rayonu' end regionname1, RegionsID
                 from List_classification_Municipal lcm  inner join Taxpayer t on lcm.MunicipalID = t.MunicipalID
                 inner join List_classification_Regions lcr on lcm.RegionID = lcr.RegionsID
                 where t.TaxpayerID = " + TaxpayerID);
            DataRow drtediyye1 = klas.GetDataRow(@"select LivingType from LivingArea la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
  where (LivingType=1 or LivingType=3 or LivingType=6 ) and tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye2 = klas.GetDataRow(@"select LivingType from LivingArea la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
  where (LivingType=2 or LivingType=4  or LivingType=5 ) and tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye3 = klas.GetDataRow(@"select TaxType from WaterAirTransport la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
  where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye4 = klas.GetDataRow(@"select TaxType from MineTax la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye4x = klas.GetDataRow(@"select tx.TaxpayerID from ViewLivingAreaLisee la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye5 = klas.GetDataRow(@"select TaxType from ProfitsTax la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye6 = klas.GetDataRow(@"select TaxType from Advertisement la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye7 = klas.GetDataRow(@"select TaxType from CarStop la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye8 = klas.GetDataRow(@"select TaxType from Hotel la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye9 = klas.GetDataRow(@"select TaxType from TradeService la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataRow drtediyye10 = klas.GetDataRow(@"select TaxType from Alienation la inner join Taxpayer tx on tx.TaxpayerID=la.TaxpayerID
          where  tx.TaxpayerID=" + TaxpayerID);
            DataTable dt = new DataTable();

            dt.Columns.Add("RayonID", typeof(String));
            dt.Columns.Add("BelediyyeID", typeof(String));
            dt.Columns.Add("RayonSheher", typeof(String));
            dt.Columns.Add("Belediyye", typeof(String));
            dt.Columns.Add("OdeyiciID", typeof(String));
            dt.Columns.Add("OdeyiciAdi", typeof(String));
            dt.Columns.Add("VergiTipi", typeof(String));
            dt.Columns.Add("VergiAdi", typeof(String));
            dt.Columns.Add("Mebleg", typeof(String));
            dt.Columns.Add("HesabNomresi", typeof(String));
            dt.Columns.Add("BankAdi", typeof(String));

            dt.Columns.Add("AmountDue", typeof(decimal));
            dt.Columns.Add("CurrentDebt", typeof(decimal));
            dt.Columns.Add("PaymentReceiver", typeof(int));
            dt.Columns.Add("ServiceCode", typeof(int));
            dt.TableName = "Taxes";
            //string taxpayername = klas.getdatacell("Select SName+' '+Name+' '+FName as name from Taxpayer where TaxpayerID=" + TaxpayerID);
            //DataRow AccountNumber = klas.GetDataRow("Select AccountNumber,Bank from List_classification_Municipal where MunicipalID = (select MunicipalID from Taxpayer where TaxpayerID=" + TaxpayerID + ")");

            //DataRow RegionName = klas.GetDataRow("Select Name+' '+case when CityID=1 then N'şəhəri' else 'rayonu' end regionname1,RegionsID from List_classification_Regions where RegionsID in (Select RegionID from List_classification_Municipal where MunicipalID = (select MunicipalID from Taxpayer where TaxpayerID=" + TaxpayerID + "))");

            foreach (int i in Servicecodelist1)
            {

                if (i == 20000200 && (drtediyye1 != null || drtediyye3 != null))
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetEmlakTorpaq("1", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "1";
                        dr["VergiAdi"] = "Əmlak vergisi";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();

                        dr["CurrentDebt"] = -mebleg2;
                        dr["AmountDue"] = -mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000200;

                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                       
                        throw ;
                    }
                }
                else if (i == 20000100 && drtediyye2 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();

                        object mebleg = GetEmlakTorpaq("2", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "2";
                        dr["VergiAdi"] = "Torpaq vergisi";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = -mebleg2;
                        dr["AmountDue"] = -mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000100;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else if (i == 20000300 && drtediyye4 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("7", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);
                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "7";
                        dr["VergiAdi"] = "Mədən vergisi";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000300;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        throw ;
                    }
                }
                else if (i == 20001000 && drtediyye4x != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("8", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "8";
                        dr["VergiAdi"] = "Bələdiyyə mülkiyyətinin icarəsinə görə ödəniş";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20001000;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        throw ;
                    }
                }

                else if (i == 20000500 && drtediyye6 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("9", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);
                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "9";
                        dr["VergiAdi"] = "Küçə (divar) reklamının yayımı üçün ödəniş";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000500;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        throw ;
                    }
                }
                else if (i == 20000800 && drtediyye7 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("10", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "10";
                        dr["VergiAdi"] = "Nəqliyyat vasitələrinin daimi və ya müvəqqəti dayanacaqları üçün ödəniş";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000800;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                         throw ;
                    }
                }
                else if (i == 20000700 && drtediyye8 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("11", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "11";
                        dr["VergiAdi"] = "Mehmanxana, sanatoriya-kurort və turizm xidmətləri göstərən şəxslərdən alınan ödəniş";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000700;
                        dt.Rows.Add(dr);
                    }

                    catch (Exception ex)
                    {
                          throw ;
                    }
                }
                else if (i == 20000600 && drtediyye9 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("12", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "12";
                        dr["VergiAdi"] = "Stasionar və ya səyyar ticarət, ictimai iaşə və digər xidmətlərə görə ödəniş";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000600;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                       
                        throw ;
                    }
                }
                else if (i == 20000900 && drtediyye10 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("13", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);

                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "13";
                        dr["VergiAdi"] = "Bələdiyyə mülkiyyətinin özgəninkiləşdirilməsinə görə ödəniş";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000900;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        throw ;
                    }
                }
                else if (i == 20000400 && drtediyye5 != null)
                {
                    try
                    {
                        DataRow dr = dt.NewRow();
                        object mebleg = GetOdenishAmount("14", TaxpayerID);
                        double mebleg2 = Math.Round(float.Parse(mebleg.ToString()), 2, MidpointRounding.AwayFromZero);


                        dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                        dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                        dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                        dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                        dr["OdeyiciID"] = TaxpayerID;
                        dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                        dr["VergiTipi"] = "14";
                        dr["VergiAdi"] = "Mənfəət vergisi";
                        dr["Mebleg"] = mebleg2;
                        dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                        dr["BankAdi"] = drtediyye0["Bank"].ToString();
                        dr["CurrentDebt"] = mebleg2;
                        dr["AmountDue"] = mebleg2;
                        dr["PaymentReceiver"] = int.Parse(drtediyye0["Municipal_code"].ToString());
                        dr["ServiceCode"] = 20000400;
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                //else if (dt.Rows.Count == 0)
                //{
                //    DataRow dr = dt.NewRow();
                //    //double odeniw = Math.Round(float.Parse(GetOdenishAmount("13", TaxpayerID).ToString()), 2, MidpointRounding.AwayFromZero);
                //    dr["RayonID"] = drtediyye0["RegionsID"].ToString();
                //    dr["BelediyyeID"] = drtediyye0["MunicipalID"].ToString();
                //    dr["RayonSheher"] = drtediyye0["regionname1"].ToString();
                //    dr["Belediyye"] = drtediyye0["MunicipalName"].ToString();
                //    dr["OdeyiciID"] = TaxpayerID;
                //    dr["OdeyiciAdi"] = drtediyye0["fullname"].ToString();
                //    dr["VergiTipi"] = "0";
                //    dr["VergiAdi"] = "";
                //    dr["Mebleg"] = 0;
                //    dr["HesabNomresi"] = drtediyye0["AccountNumber"].ToString();
                //    dr["BankAdi"] = drtediyye0["Bank"].ToString();
                //    dr["InvoiceType"] = invoiceType.AVANS;

                //    dr["CurrentDebt"] = 0;
                //    dr["PaymentReceiver"] = 0;
                //    dr["ServiceCode"] = 0;
                //    dt.Rows.Add(dr);

                //}
            }
            return dt;
        }
    }
}
