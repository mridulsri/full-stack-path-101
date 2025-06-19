namespace Concepts_101;

class Program
{
    static void Main(string[] args)
    {

        var list = new List<string>();
        list = null;
        var result = list?.Any()?? true;
        
        var obj = new PatternMatching();
        obj.CheckPatternMatching();
    }

    class ApSettings
    {
        public bool Enable { get; set; }
        public int SeatNumber { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
    }

    class Address
    {
        public bool Enable { get; set; }
        public int Number { get; set; }
        public string AddressLine { get; set; }
        public PhoneType ContactNumber { get; set; }
    }

    private enum PhoneType
    {
        Teliphone,
        Mobile
        
        
    }
    private enum Gender
    {
        Male,
        Female,
        Others
    }
}