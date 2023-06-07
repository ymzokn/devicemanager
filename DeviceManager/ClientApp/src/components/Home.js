import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Devices } from './Devices';

export const Home = () => {
  const [count, setCount] = useState(0);
  const [connection, setConnection] = useState(null);
  const [measurements, setMeasurements] = useState([]);

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("/count")
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    setConnection(connect);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log('Connected!');
          connection.on("ReceiveCount", (message) => {
            setCount(message);
          });
          connection.on("ReceiveMeasurement", (message) => {
            setMeasurements(message);
          })
        })
        .catch((error) => console.log(error));
    } else {
      console.log("Can't establish connection");
    }
  }, [connection]);

  const sendMeasurement = (device) => {
    fetch("/api/measurement", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        "value": (Math.random() + 1).toString(),
        "deviceId": device.id
      })
    })
  }

  return (
    <div>
      <h1>Count: {count}</h1>
      <Devices sendMeasurement={sendMeasurement} />
    </div>
  )
}