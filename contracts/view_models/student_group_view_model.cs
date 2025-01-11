namespace contracts.view_moedels {
    public class student_group_view_model {
        public int id { get; set; }
        public int group_num { get; set; } 
        public int student_count { get; set; }
        public string direction_name { get; set; } = string.Empty;
        public List<string> students_fio { get; set; } = new();
    }
}
