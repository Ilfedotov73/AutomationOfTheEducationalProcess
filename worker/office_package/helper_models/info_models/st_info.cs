using contracts.view_moedels;
using contracts.worker_contracts.helper_models;

namespace worker.office_package.helper_models.info_models {
    public class st_info : Idata_info {
        public string title => "ЭКЗАМЕНАЦИОННАЯ ВЕДОМОСТЬ";
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public student_group_view_model? group_info { get; set; }
        public string subject { get; set; } = string.Empty;
        public string examiner { get; set; } = string.Empty;
        public test_type test { get; set; }
        public int totalHoursNum { get; set; }
    }
    public enum test_type {
        Зачет,
        Экзамен
    }
}
