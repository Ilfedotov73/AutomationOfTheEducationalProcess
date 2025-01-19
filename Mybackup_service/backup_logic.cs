using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Mybackup_service {
    public class backup_logic {
        string dir = @"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\BACKUP\";
        private readonly ILogger _logger;

        public backup_logic(ILogger<backup_logic> logger) {
            _logger = logger;
        }

        public void create_back_up() {
            var connection = new SqlConnection(@"Data Source=FEDOTOVILIA\SQLEXPRESS;
                                            Initial Catalog=AutomationOfTheEducationalProcess;
                                            Integrated Security=True;
                                            MultipleActiveResultSets=True;
                                            ;
                                            TrustServerCertificate=True");
            var command = new SqlCommand("BackupDatabase", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            command.Parameters.AddWithValue("@location", dir);

            connection.Open();
            try {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex) {
                _logger.LogError(ex, "backup operatin created is failed");
                throw;
            }
            connection.Close();
        }

        public (byte[], string) export_backup_file() {
            List<byte[]> backups = new();
            string[] files = Directory.GetFiles(dir);
            var file_info = new FileInfo(files[files.Length - 1]);
            return (File.ReadAllBytes(files[files.Length - 1]), file_info.Name);
        }
    }
}
