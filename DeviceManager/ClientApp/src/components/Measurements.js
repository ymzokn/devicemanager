import React, { useEffect, useState } from "react";

export const Measurements = (props) => {
    const [measurements, setMeasurements] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        populateDeviceData();
    }, []);

    const populateDeviceData = async () => {
        const response = await fetch('api/measurement');
        const data = await response.json();
        setMeasurements([...data]);
        setLoading(false);
    }

    let contents = loading ? <p><em>Loading...</em></p> : <table className='table' aria-labelledby="tabelLabel">
        <thead>
            <tr>
                <th>#</th>
                <th>Device Name</th>
            </tr>
        </thead>
        <tbody>
            {measurements.map((measurement, index) =>
                <tr key={measurement.id}>
                    <td>{index+1}</td>
                    <td className="font-weight-bold">{measurement.device.name}</td>
                </tr>
            )}
        </tbody>
    </table>

    return contents;
}