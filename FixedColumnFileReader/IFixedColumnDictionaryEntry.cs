namespace FixedColumnFileCollection
{
    public interface IFixedColumnDictionaryEntry
    {
        string Name { get; }
        int ColumnStart { get; }
        int Length { get; }
    }
}
