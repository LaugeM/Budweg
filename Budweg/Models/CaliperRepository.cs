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

                if (string.IsNullOrEmpty(caliper.StampNumber))
                {
                    // Brand new caliper, generate stamp and insert
                    using SqlCommand cmd = new SqlCommand("dbo.GenerateStampNumber", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Manufacturer", System.Data.SqlDbType.NVarChar, 50).Value = caliper.Manufacturer;
                    cmd.Parameters.Add("@Approval", System.Data.SqlDbType.Bit).Value = caliper.Approval;
                    cmd.Parameters.Add("@ModelNumber", System.Data.SqlDbType.NVarChar, 20).Value = caliper.ModelNumber;
                    caliper.StampNumber = cmd.ExecuteScalar().ToString().Trim();
                }
                else
                {
                    // Stamp number provided, check if it already exists
                    using SqlCommand checkCmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM dbo.CALIPER 
                    WHERE StampNumber = @StampNumber", con);
                    checkCmd.Parameters.Add("@StampNumber", System.Data.SqlDbType.Char, 8).Value = caliper.StampNumber;
                    bool exists = (int)checkCmd.ExecuteScalar() > 0;

                    if (exists)
                    {
                        // Returning caliper, increment counter
                        using SqlCommand renovateCmd = new SqlCommand(@"
                        UPDATE dbo.CALIPER
                        SET TimesRenovated = TimesRenovated + 1, Approval = @Approval
                        WHERE StampNumber = @StampNumber;", con);
                        renovateCmd.Parameters.Add("@StampNumber", System.Data.SqlDbType.Char, 8).Value = caliper.StampNumber;
                        renovateCmd.Parameters.Add("@Approval", System.Data.SqlDbType.Bit).Value = caliper.Approval;
                        renovateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // New caliper with pre-existing stamp, insert into CALIPER
                        using SqlCommand insertCmd = new SqlCommand(@"
                        INSERT INTO dbo.CALIPER (StampNumber, Manufacturer, Approval, ModelNumber)
                        VALUES (@StampNumber, @Manufacturer, @Approval, @ModelNumber)", con);
                        insertCmd.Parameters.Add("@StampNumber", System.Data.SqlDbType.Char, 8).Value = caliper.StampNumber;
                        insertCmd.Parameters.Add("@Manufacturer", System.Data.SqlDbType.NVarChar, 50).Value = caliper.Manufacturer;
                        insertCmd.Parameters.Add("@Approval", System.Data.SqlDbType.Bit).Value = caliper.Approval;
                        insertCmd.Parameters.Add("@ModelNumber", System.Data.SqlDbType.NVarChar, 20).Value = caliper.ModelNumber;
                        insertCmd.ExecuteNonQuery();
                    }
                }

                // Always insert a renovation record, regardless of which branch we took
                using SqlCommand renovationCmd = new SqlCommand(@"
                INSERT INTO dbo.RENOVATION (StampNumber, RegistrationDate)
                VALUES (@StampNumber, @RegistrationDate);", con);
                renovationCmd.Parameters.Add("@StampNumber", System.Data.SqlDbType.Char, 8).Value = caliper.StampNumber;
                renovationCmd.Parameters.Add("@RegistrationDate", System.Data.SqlDbType.Date).Value = DateTime.Today;
                renovationCmd.ExecuteNonQuery();

                caliper.StampNumber = null;
            }
        }
    }
}
