import { useState } from "react";
import { entry } from "../services/parkingSessionApi";
import InputMask from 'react-input-mask';

function EntryForm({ onSuccess }) {
    const [licensePlate, setLicensePlate] = useState('');
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');

    function formatLicensePlate(value) {
        let cleaned = value.toUpperCase().replace(/[^A-Z0-9]/g, "");
        if (cleaned.length > 7) cleaned = cleaned.slice(0, 7);
        if (cleaned.length > 3) return cleaned.slice(0, 3) + "-" + cleaned.slice(3);
        return cleaned;
    }

    async function handlerEntry(e) {
        e.preventDefault();
        setMessage('');
        setError('');

        try {
            const body = {
                licensePlate: licensePlate
            };

            await entry(body);
            setError('');
            setMessage('Entrada registrada com sucesso!');
            setLicensePlate('');
            if (onSuccess) onSuccess();
        } catch (error) {
            setMessage('');
            setError('Erro ao registrar entrada.');
        };
    };

    return (
        <div style={{ padding: '20px' }}>
            <form onSubmit={handlerEntry}>
                <h2>Registrar entrada</h2>
                <div style={{ marginTop: '10px' }}>
                <label>Placa:</label><br />
                <input
                    type="text"
                    value={licensePlate}
                    onChange={(e) => setLicensePlate(formatLicensePlate(e.target.value))}
                    maxLength={8}
                    required
                    placeholder="ABC-1234"
                />
                </div>
                <button type="submit" style={{ marginTop: '15px' }}>
                    Entrar
                </button>
            </form>

            {message && <p style={{ color: 'green' }}>{message}</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
    )
};

export default EntryForm;