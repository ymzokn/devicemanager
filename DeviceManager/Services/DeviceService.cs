using DeviceManager.Data;
using DeviceManager.Models;
using System.Diagnostics.Metrics;

namespace DeviceManager.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly DbContextClass _dbContext;

        public DeviceService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Device> GetDevices()
        {
            return _dbContext.Devices.ToList();
        }
        public Device AddDevice(Device device)
        {
            var result = _dbContext.Devices.Add(device);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public Device RemoveDevice(Device device)
        {
            var result = _dbContext.Devices.Remove(device);
            var measurements = _dbContext.Measurements.Where(measurement => measurement.DeviceId == device.Id);
            foreach(var measurement in measurements)
            {
                _dbContext.Measurements.Remove(measurement);
            }
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
