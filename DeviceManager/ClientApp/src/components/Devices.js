import React, { useEffect, useState } from "react";

export const Devices = (props) => {
  const [devices, setDevices] = useState([]);
  const [autoDevices, setAutoDevices] = useState([]);
  const [loading, setLoading] = useState(true);
  const [name, setName] = useState('');
  const { sendMeasurement } = { ...props };

  useEffect(() => {
    populateDeviceData();
  }, []);

  const populateDeviceData = async () => {
    const response = await fetch('api/device');
    const data = await response.json();
    setDevices([...data]);
    setLoading(false);
  }

  const addDevice = async () => {
    setAutoDevices([]);
    const response = await fetch('api/device', {
      method: "POST",
      body: JSON.stringify({
        Name: name
      }),
      headers: {
        "Content-Type": "application/json",
      }
    }).then(() => {
      populateDeviceData()
    });
  }

  const removeDevice = async (device) => {
    setAutoDevices([]);
    const response = await fetch('api/device', {
      method: "DELETE",
      body: JSON.stringify({
        "id": device.id,
        "Name": device.name,
      }),
      headers: {
        "Content-Type": "application/json",
      }
    }).then(() => {
      populateDeviceData()
    });
  }

  const startRandom = async (device) => {
    let devices = autoDevices;
    setAutoDevices([...devices, device]);
    let interval = setInterval(() => {
      sendMeasurement(device);
    }, ((Math.random() + 1) * 1000));
    device.intervalId = interval;
  }

  const stopRandom = async (device) => {
    window.clearInterval(device.intervalId);
    let devices = autoDevices.filter((elem) => elem != device)
    setAutoDevices([...devices]);
  }

  let contents = loading ? <p><em>Loading...</em></p> : <table className='table' aria-labelledby="tabelLabel">
    <thead>
      <tr>
        <th colSpan="4">Devices</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td><form className="form-group"><input className="form-control" onChange={(e) => setName(e.target.value)} placeholder="Device Name" /></form></td><td><button className="mx-2 btn btn-primary" onClick={() => addDevice()}>Add Device</button></td>
      </tr>
      {devices.map(device =>
        <tr key={device.id}>
          <td className="font-weight-bold">{device.name}</td>
          <td><button className="btn btn-outline-danger" onClick={() => removeDevice(device)}>Remove</button></td>
          <td><button className="btn btn-outline-success" onClick={() => sendMeasurement(device)}>Send Measurement</button></td>
          {!autoDevices.includes(device) && <td><button className="btn btn-outline-primary" onClick={() => startRandom(device)}>Auto</button></td>}
          {autoDevices.includes(device) && <td><button className="btn btn-outline-danger" onClick={() => stopRandom(device)}>Stop</button></td>}
        </tr>
      )}
    </tbody>
  </table>

  return (contents);
}