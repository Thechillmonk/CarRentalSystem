using System.Data.SqlClient;

namespace CarRentalSystem.Ultility
{
    public static class DBConnUtil
    {
        private static SqlConnection connection;

        public static SqlConnection GetConnection(string connectionString)
        {
            try
            {
                if (connection == null || connection.State == System.Data.ConnectionState.Closed)
                {
                    connection = new SqlConnection(connectionString);
                }
                return connection;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error establishing database connection: {ex.Message}");
            }
        }

        public static SqlConnection GetConnection()
        {
            string connectionString = DBPropertyUtil.GetPropertyString("settings.json");
            return GetConnection(connectionString);
        }
    }
}




