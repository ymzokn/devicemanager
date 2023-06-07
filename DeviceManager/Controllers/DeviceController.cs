using DeviceManager.Models;
using DeviceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService deviceService;
        public DeviceController(IDeviceService _deviceService)
        {
            deviceService = _deviceService;
        }
        [HttpGet]
        public IEnumerable<Device> Get()
        {
            var deviceList = deviceService.GetDevices();
            return deviceList;
        }
        [HttpPost]
        public Device Post(Device device) {
            var newDevice = deviceService.AddDevice(device);
            return newDevice;
        }
        [HttpDelete]
        public Device Delete(Device device)
        {
            var oldDevice = deviceService.RemoveDevice(device);
            return oldDevice;
        }
    }
}
