using Microsoft.Data.SqlClient;
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

        public override void Add(Caliper caliper)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO CALIPER (Manufacturer, Approval, ModelNumber, RegistrationDate) " +
                "VALUES(@Manufacturer,@Approval,@ModelNumber,@RegistrationDate)" +
                "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@Manufacturer", SqlDbType.NVarChar).Value = caliper.Manufacturer;
                    cmd.Parameters.Add("@Approval", SqlDbType.Bit).Value = caliper.Approval;
                    cmd.Parameters.Add("@ModelNumber", SqlDbType.NVarChar).Value = caliper.ModelNumber;
                    cmd.Parameters.Add("@RegistrationDate", SqlDbType.Date).Value = caliper.RegistrationDate;
                    var result = cmd.ExecuteScalar();
                }
            }
        }
    }
}
