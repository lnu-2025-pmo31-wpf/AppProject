namespace DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsExpense { get; set; }
        public override string ToString() => Name;
    }
}
