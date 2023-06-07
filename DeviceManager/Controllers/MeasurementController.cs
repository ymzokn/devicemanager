using Microsoft.AspNetCore.Mvc;
using DeviceManager.Models;
using DeviceManager.Services;

namespace DeviceManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService measurementService;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public MeasurementController(IMeasurementService _measurementService, RabbitMQProducer rabbitMQProducer)
        {
            measurementService = _measurementService;
            _rabbitMQProducer = rabbitMQProducer;
        }
        [HttpGet]
        public IEnumerable<Measurement> Get()
        {
            var measurementList = measurementService.GetMeasurements();
            return measurementList;
        }
        [HttpPost]
        public Measurement Post(Measurement measurement)
        {
            var newMeasurement = measurementService.AddMeasurement(measurement);
            _rabbitMQProducer.Publish(measurement.Id.ToString());
            return newMeasurement;
        }
    }
}
