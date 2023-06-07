namespace DeviceManager.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public int? DeviceId { get; set; }
        public Device? Device { get; set; }
    }
}
