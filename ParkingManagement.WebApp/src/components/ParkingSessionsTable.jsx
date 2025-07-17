import { useState } from "react";
import { exit } from "../services/parkingSessionApi";

function ParkingSessionsTable({ parkingSessions, onExitSuccess }) {
    const [error, setError] = useState('');

    async function handlerExit(id) {
        try {
            await exit(id);
            if (onExitSuccess) onExitSuccess();
        } catch (error) {
            setError('Erro ao registrar saída.');
        }
    }

    return (
        <div style={{ padding: '20px' }}>
            <h2>Estacionamento</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}

            <table border="1" cellPadding="8">
                <thead>
                    <tr>
                        <th>Placa</th>
                        <th>Horário de Chegada</th>
                        <th>Horário de Saída</th>
                        <th>Duração</th>
                        <th>Tempo Cobrado (hora)</th>
                        <th>Preço</th>
                        <th>Valor a Pagar</th>
                        <th>Registrar Saída</th>
                    </tr>
                </thead>
                <tbody>
                    {parkingSessions.map(parkingSession => (
                        <tr key={parkingSession.id}>
                            <td>{parkingSession.licensePlate}</td>
                            <td>{new Date(parkingSession.entryTime).toLocaleString()}</td>
                            <td>{new Date(parkingSession.exitTime).toLocaleString()}</td>
                            <td>{parkingSession.payment.duration}</td>
                            <td>{parkingSession.payment.numberOfHoursToPay}</td>
                            <td>{parkingSession.payment.priceBaseValue.toFixed(2)}</td>
                            <td>{parkingSession.payment.totalPayable.toFixed(2)}</td>
                            <td>
                                <button onClick={() => handlerExit(parkingSession.id)}>
                                    Sair
                                </button>
                            </td>
                        </tr>
                    ))}
                    {parkingSessions.length === 0 && (
                        <tr>
                            <td colSpan="5">Nenhum registro encontrado.</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default ParkingSessionsTable;