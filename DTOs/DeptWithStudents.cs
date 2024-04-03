namespace API_FinalTask.DTOs
{
    public class DeptWithStudents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MgrName { get; set; }
        public List<string> StudentsNames { get; set; } = new List<string>();
    }
}
