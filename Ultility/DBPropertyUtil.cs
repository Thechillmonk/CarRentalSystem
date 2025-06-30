using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CarRentalSystem.Ultility
{
    public static class DBPropertyUtil
    {
        public static SqlCommand Command { get; private set; } // Change type from 'object' to 'SqlCommand'

        public static string GetPropertyString(string propertyFileName)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(propertyFileName);

                var configuration = builder.Build();
                return configuration.GetConnectionString("DefaultConnection");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error generating lease ID: {ex.Message}");
            }
        }

        private static int GeneratePaymentID()
        {
            try
            {
                using (var connection = DBConnUtil.GetConnection())
                {
                    Command = new SqlCommand(); // Initialize the SqlCommand object
                    Command.CommandText = "SELECT ISNULL(MAX(paymentID), 0) + 1 FROM Payment";
                    Command.Connection = connection;
                    connection.Open();
                    return (int)Command.ExecuteScalar(); // ExecuteScalar is now accessible
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error generating payment ID: {ex.Message}");
            }
        }
    }
}
