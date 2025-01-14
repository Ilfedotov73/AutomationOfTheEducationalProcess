using contracts.view_moedels;
using contracts.worker_contracts.helper_models;

namespace worker.office_package.helper_models.info_models {
    public class itp_temp_info : Idata_info {
        public string title => "Индивидуальный план преподавателя (на 1 год)";
        public DateOnly date => DateOnly.FromDateTime(DateTime.Now);
        public DateOnly date_from = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly date_to { get; set; } = DateOnly.FromDateTime(DateTime.Now).AddYears(1);

        public user_view_model? user_info { get; set; }

        public List<string> disciplines_A { get; set; } = new();
        public List<string> disciplines_B { get; set; } = new();

        public List<string> workTypes_21 { get; set; } = new();
        public List<string> workTypes_22 { get; set; } = new();
        public List<string> workTypes_23 { get; set; } = new();
        public List<string> workTypes_24 { get; set; } = new();

        public List<string> workTypes_31 { get; set; } = new();
        public List<string> workTypes_32 { get; set; } = new();

        public List<string> workTypes_41 { get; set; } = new();
        public List<string> workTypes_42 { get; set; } = new();

        public int workListRowCount_43 { get; set; } = 5;
        public int workListRowCount_44 { get; set; } = 5;

        public int nirListRowCount { get; set; } = 5;

        public int pdpPlanRowCount { get; set; } = 5;
        public int commentRowCount { get; set; } = 5;
    }
}
