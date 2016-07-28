namespace FixedColumnFileCollection
{
    public class FixedColumnDictionaryEntry : IFixedColumnDictionaryEntry
    {
        public string Name { get; set; }
        public int ColumnStart { get; set; }
        public int Length { get; set; }
    }
}