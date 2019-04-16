namespace SimpleAir.Domain.Model
{
    /// <summary>
    /// Airport object
    /// </summary>
    public class Airport
    {
        /// <summary>
        /// Airport Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Airport Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Airport Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Creates a new airport object with given parameters
        /// </summary>
        /// <param name="name">Airport Name</param>
        /// <param name="code">Airport code</param>
        /// <returns>new Airport object</returns>
        public static Airport Create(string name, string code)
        {
            //an event might be raised for event sourcing

            return new Airport() { Name = name, Code = code };
        }
    }
}
