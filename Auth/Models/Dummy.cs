namespace TechnoBit.Models;

public class Dummy : BaseEntity
{
    public int? _int1 { get; set; }
    public string? _str1 { get; set; }
    public IEnumerable<int>? _int2 { get; set; }
    public string? _str2 { get; set; }
    public IEnumerable<string>? _str3 { get; set; }
    public List<int>? _int4 { get; set; }
    
}