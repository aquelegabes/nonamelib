namespace NoNameLib.Api.ViewModels;

public static class ApiTestViewModels
{
    public class BasicModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class InsertModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class IsValidModel
    {
        public string Id { get; set; }
        public bool IsValid { get; set; }
    }
}
