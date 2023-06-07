using DeviceManager.Models;

namespace DeviceManager.Services
{
    public interface IDeviceService
    {
        public IEnumerable<Device> GetDevices();
        public Device AddDevice(Device device);
        public Device RemoveDevice(Device device);
    }
}
