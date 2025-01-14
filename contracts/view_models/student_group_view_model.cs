namespace contracts.view_moedels {
    public class student_group_view_model {
        public int id { get; set; }
        public string faculty_name { get; set; } = string.Empty;
        public string group { get; set; } = string.Empty;
        public string direction_name { get; set; } = string.Empty;
        public int course_num { get; set; }
        public int semester_num { get; set; }
        public List<(int grade_book_num, string fio)> students { get; set; } = new();
    }
}
