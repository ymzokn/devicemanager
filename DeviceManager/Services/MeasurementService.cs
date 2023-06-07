using DeviceManager.Models;
using DeviceManager.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly DbContextClass _dbContext;

        public MeasurementService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Measurement> GetMeasurements()
        {
            return _dbContext.Measurements.Include("Device").ToList();
        }
        public Measurement AddMeasurement(Measurement measurement)
        {
            var result = _dbContext.Measurements.Add(measurement);
            if (measurement.Device != null)
            {
                var Device = _dbContext.Devices.Where(device => device.Name == measurement.Device.Name).FirstOrDefault();
                measurement.Device = Device;
            }
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public int GetMeasurementCount()
        {
            var result = _dbContext.Measurements.Count();
            return result;
        }
    }
}
