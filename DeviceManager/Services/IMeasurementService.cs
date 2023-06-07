using DeviceManager.Models;

namespace DeviceManager.Services;
public interface IMeasurementService
{
    public IEnumerable<Measurement> GetMeasurements();
    public Measurement AddMeasurement(Measurement measurement);
    public int GetMeasurementCount();
}
