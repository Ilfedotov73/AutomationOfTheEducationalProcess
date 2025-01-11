namespace contracts.view_moedels {
    public class document_view_model {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public string author_name { get; set; } = string.Empty;
        public string file_format_type { get; set; } = string.Empty;
        public string document_type { get; set; } = string.Empty;
        public string template_name { get; set; } = string.Empty;
    }
}
