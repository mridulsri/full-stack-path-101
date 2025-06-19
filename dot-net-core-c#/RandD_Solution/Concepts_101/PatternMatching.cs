namespace Concepts_101;

public class PatternMatching
{
    public void CheckPatternMatching()
    {
        var apSettings = new ApSettings()
        {
            Tenant = "Mridul"
        };
        try
        {
            var someFeature = apSettings.SomeFeature;
            if (someFeature.Enable)
            {
                Console.WriteLine("feature is enabled");
            }
            Console.WriteLine("feature is not enabled");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        

    }

    class ApSettings
    {
        public bool Enable { get; set; }
        public string Tenant { get; set; }

        public SomeFeature SomeFeature { get; set; }
    }

    class SomeFeature
    {
        public bool Enable { get; set; }
        public IList<string> Code { get; set; }
    }
    
    
}