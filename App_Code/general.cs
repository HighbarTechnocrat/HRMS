using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace WebApplication1
{
    public class general
    {
        string strcon = ConfigurationManager.AppSettings["con"].ToString();
        SqlConnection con1;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        public DataSet getData(String strs)
        {
            con1 = new SqlConnection(strcon);
            da = new SqlDataAdapter(strs, con1);
            ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            con1.Dispose();
            return ds;
        }

        public int updateData(string strs)
        {
            int k;
            con1 = new SqlConnection(strcon);
            con1.Open();
            cmd = new SqlCommand(strs, con1);
            k = cmd.ExecuteNonQuery();
            con1.Close();
            return k;
        }

        public DataSet spGetData(string spName, SqlParameter[] param)
        {
            con1 = new SqlConnection(strcon);
            cmd = new SqlCommand(spName, con1);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter sqlParam in param)
            {
                if (param != null)
                {
                    cmd.Parameters.Add(sqlParam);
                }
            }
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }

        public int spUpdateData(string spName, SqlParameter[] param)
        {
            int k = 0;
            con1 = new SqlConnection(strcon);
            con1.Open();
            cmd = new SqlCommand(spName, con1);
            da = new SqlDataAdapter(cmd);
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                foreach (SqlParameter sqlParam in param)
                {
                    cmd.Parameters.Add(sqlParam);
                }
            }
            k = cmd.ExecuteNonQuery();
            return k;
        }

        public int sendMail(string r, string p)
        {
            string sendid = "shaikh.hameed2001@gmail.com";
            string sendpass = "saifsaif";
            try
            {
                SmtpClient sc = new SmtpClient { Host = "smtp.gmail.com", Port = 587, EnableSsl = true, DeliveryMethod = SmtpDeliveryMethod.Network, Credentials = new System.Net.NetworkCredential(sendid, sendpass), Timeout = 6000, };
                MailMessage msg = new MailMessage(sendid, r, "FROM Chat-And-Share : Password recovery", "Your password is : " + p);
                sc.Send(msg);
                return 1;
            }
            catch (Exception e2)
            {
                return 0;
            }

        }
    }
}