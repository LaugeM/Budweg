using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Budweg.Models
{
    public class CaliperRepository : BaseRepo<Caliper>
    {
        public CaliperRepository() : base()
        {
        }

        //public override void Add(Caliper caliper)
        //{
        //    using (SqlConnection con = CreateConnection())
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new SqlCommand("INSERT INTO CALIPER (Manufacturer, Approval, ModelNumber, RegistrationDate) " +
        //        "VALUES(@Manufacturer,@Approval,@ModelNumber,@RegistrationDate)" +
        //        "SELECT @@IDENTITY", con))
        //        {
        //            cmd.Parameters.Add("@Manufacturer", SqlDbType.NVarChar).Value = caliper.Manufacturer;
        //            cmd.Parameters.Add("@Approval", SqlDbType.Bit).Value = caliper.Approval;
        //            cmd.Parameters.Add("@ModelNumber", SqlDbType.NVarChar).Value = caliper.ModelNumber;
        //            cmd.Parameters.Add("@RegistrationDate", SqlDbType.Date).Value = caliper.RegistrationDate;
        //            var result = cmd.ExecuteScalar();
        //        }
        //    }
        //}

        public override void Add(Caliper caliper)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                // If no existing stampnumber, use database to create one
                if (string.IsNullOrEmpty(caliper.StampNumber))
                {
                    using SqlCommand cmd = new SqlCommand("dbo.GenerateStampNumber", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    caliper.StampNumber = cmd.ExecuteScalar().ToString();
                }
                else
                {
                    //Currently not being used, this is preparing for batch creation
                    using SqlCommand cmd = new SqlCommand("dbo.GetTimesRenovated", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StampNumber", caliper.StampNumber);

                    int timesRenovated = (int)cmd.ExecuteScalar();
                }

                //StampNumber is guaranteed to be set now
                using SqlCommand insert = new SqlCommand(@"
                INSERT INTO dbo.CALIPER 
                    (StampNumber, BatchNumber, Manufacturer, Approval, ModelNumber, RegistrationDate)
                VALUES 
                    (@StampNumber, @BatchNumber, @Manufacturer, @Approval, @ModelNumber, @RegistrationDate)", con);

                insert.Parameters.AddWithValue("@StampNumber", caliper.StampNumber);
                insert.Parameters.AddWithValue("@BatchNumber", (object?)caliper.BatchNumber ?? DBNull.Value);
                insert.Parameters.AddWithValue("@Manufacturer", caliper.Manufacturer);
                insert.Parameters.AddWithValue("@Approval", caliper.Approval);
                insert.Parameters.AddWithValue("@ModelNumber", caliper.ModelNumber);
                insert.Parameters.AddWithValue("@RegistrationDate", caliper.RegistrationDate);

                insert.ExecuteNonQuery();

            }
        }
    }
}
