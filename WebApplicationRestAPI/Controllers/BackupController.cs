using Microsoft.AspNetCore.Mvc;
using Mybackup_service;
using Newtonsoft.Json;

namespace WebApplicationRestAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BackupController : Controller {

        private readonly backup_logic _backupLogic;
        private readonly ILogger _logger;
        public BackupController(ILogger<BackupController> logger, ILogger<backup_logic> logger1) {
            _backupLogic = new(logger1);
            _logger = logger;
        }

        [HttpGet]
        public List<string> get_backup() {
            _backupLogic.create_back_up();
            var data = _backupLogic.export_backup_file();
            return new List<string> { JsonConvert.SerializeObject(data.Item1), JsonConvert.SerializeObject(data.Item2) };
        }
    }
}
