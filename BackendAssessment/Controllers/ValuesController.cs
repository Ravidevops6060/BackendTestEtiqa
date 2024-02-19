using BackendAssessment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackendAssessment.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpListingDB"].ConnectionString);
        FreeLancerEmplist FLE = new FreeLancerEmplist();

        // GET api/values
        public List<FreeLancerEmplist> Get()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Usp_GetAllEmpList", sqlconn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<FreeLancerEmplist> lstfreelanceremplist = new List<FreeLancerEmplist>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FreeLancerEmplist FLE = new FreeLancerEmplist();
                        FLE.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        FLE.UserName = dt.Rows[i]["UserName"].ToString();
                        FLE.Mail = dt.Rows[i]["Mail"].ToString();
                        FLE.PhoneNumber = Convert.ToInt32(dt.Rows[i]["PhoneNumber"]);
                        FLE.SkillSets = dt.Rows[i]["SkillSets"].ToString();
                        FLE.Hobby = dt.Rows[i]["Hobby"].ToString();
                        lstfreelanceremplist.Add(FLE);
                    }
                }
                if (lstfreelanceremplist.Count > 0)
                {
                    return lstfreelanceremplist;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error- " + ex.Message);
            }

        }

        // GET api/values/5
        public FreeLancerEmplist Get(int id)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Usp_GetEmpListbyID", sqlconn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ID", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                FreeLancerEmplist FLE = new FreeLancerEmplist();
                if (dt.Rows.Count > 0)
                {

                    FLE.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    FLE.UserName = dt.Rows[0]["UserName"].ToString();
                    FLE.Mail = dt.Rows[0]["Mail"].ToString();
                    FLE.PhoneNumber = Convert.ToInt32(dt.Rows[0]["PhoneNumber"]);
                    FLE.SkillSets = dt.Rows[0]["SkillSets"].ToString();
                    FLE.Hobby = dt.Rows[0]["Hobby"].ToString();


                }

                if (FLE != null)
                {
                    return FLE;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("error- " + ex.Message);
            }
        }

        // POST api/values
        public string Post(FreeLancerEmplist freeLancerEmplist)
        {
            string RetStr = "";
            try
            {
                if (freeLancerEmplist != null)
                {
                    SqlCommand cmd = new SqlCommand("Usp_AddEmployee", sqlconn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", freeLancerEmplist.UserName);
                    cmd.Parameters.AddWithValue("@Mail", freeLancerEmplist.Mail);
                    cmd.Parameters.AddWithValue("@PhoneNumber", freeLancerEmplist.PhoneNumber);
                    cmd.Parameters.AddWithValue("@SkillSets", freeLancerEmplist.SkillSets);
                    cmd.Parameters.AddWithValue("@Hobby", freeLancerEmplist.Hobby);
                    sqlconn.Open();
                    int i = cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    if (i > 0)
                    {
                        RetStr = "Data Inserted Successfully!";
                    }
                    else
                    {
                        RetStr = "error";
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("error- " + ex.Message);
            }
            return RetStr;
        }

        // PUT api/values/5
        public string Put(int Id, FreeLancerEmplist freeLancerEmplist)
        {

            string RetStr = "";
            try
            {
                if (freeLancerEmplist != null)
                {
                    SqlCommand cmd = new SqlCommand("Usp_UpdateEmpListbyID", sqlconn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.Parameters.AddWithValue("@UserName", freeLancerEmplist.UserName);
                    cmd.Parameters.AddWithValue("@Mail", freeLancerEmplist.Mail);
                    cmd.Parameters.AddWithValue("@PhoneNumber", freeLancerEmplist.PhoneNumber);
                    cmd.Parameters.AddWithValue("@SkillSets", freeLancerEmplist.SkillSets);
                    cmd.Parameters.AddWithValue("@Hobby", freeLancerEmplist.Hobby);
                    sqlconn.Open();
                    int i = cmd.ExecuteNonQuery();
                    sqlconn.Close();

                    if (i > 0)
                    {
                        RetStr = "Data Updated Successfully!";
                    }
                    else
                    {
                        RetStr = "error";
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("error- " + ex.Message);
            }
            return RetStr;

        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            string RetStr = "";
            try
            {
                if (id > 0)
                {
                    SqlCommand cmd = new SqlCommand("Usp_DeleteEmpListbyID", sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    sqlconn.Open();
                    int i = cmd.ExecuteNonQuery();
                    sqlconn.Close();

                    if (i > 0)
                    {
                        RetStr = "Data Deleted Successfully!";
                    }
                    else
                    {
                        RetStr = "error";
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("error- " + ex.Message);
            }
            return RetStr;
        }


    }
}