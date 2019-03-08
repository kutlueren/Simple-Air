namespace SimpleAir.Domain.Model
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static Airport Create(string name, string code)
        {
            //an event might be raised for event sourcing

            return new Airport() { Name = name, Code = code };
        }
    }
}
