namespace PWS_Lab3.Shared.RequestFeatures.UserParameters
{
    public class StudentParameters
    {
        public string ContentType { get; set; } = "application/json";
        
        public int Limit { get; set; } = 50;
        public string Sort { get; set; } = "Id";
        public int Offset { get; set; }
        
        public int MinId { get; set; }
        public int MaxId { get; set; } = int.MaxValue;
        public string Like { get; set; }
        
        public string Columns { get; set; }
        
        public string GlobalLike { get; set; }
    }
}