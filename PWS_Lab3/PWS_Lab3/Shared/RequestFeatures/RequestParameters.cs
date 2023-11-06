namespace PWS_Lab3.Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        public int Limit { get; set; } = 100;
        
        public string Sort { get; set; } = "Id";
        
        public int Offset { get; set; }

        public string ContentType { get; set; } = "application/json";
    }    
}