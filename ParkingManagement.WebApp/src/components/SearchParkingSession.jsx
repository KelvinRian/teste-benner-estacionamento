import { useState } from 'react';
import { getByLicensePlate } from '../services/parkingSessionApi';

function SearchParkingSession({ setParkingSessions }) {
    const [licensePlate, setLicensePlate] = useState('');
    const [result, setResult] = useState(null);
    const [error, setError] = useState('');

    async function handleSearch() {
        setError('');

        try {
            const data = await getByLicensePlate(licensePlate);
            setParkingSessions([data]);
        } catch (error) {
            setError('Nenhuma sessão encontrada');
            setParkingSessions([]);
        }
    }

    function formatLicensePlate(value) {
        let cleaned = value.toUpperCase().replace(/[^A-Z0-9]/g, "");
        if (cleaned.length > 7) cleaned = cleaned.slice(0, 7);
        if (cleaned.length > 3) return cleaned.slice(0, 3) + "-" + cleaned.slice(3);
        return cleaned;
    }

    return (
        <div style={{ padding: '20px' }}>
            <h2>Buscar por Placa</h2>
            <input
                    type="text"
                    value={licensePlate}
                    onChange={(e) => setLicensePlate(formatLicensePlate(e.target.value))}
                    maxLength={8}
                    required
                    placeholder="ABC-1234"
            />
            <button onClick={handleSearch}>Buscar</button>

            {error && <p style={{ color: 'red' }}>{error}</p>}
            {result && (
                <pre style={{ background: '#f0f0f0', marginTop: '10px', padding: '10px' }}>
                    {JSON.stringify(result, null, 2)}
                </pre>
            )}
        </div>
    );
}

export default SearchParkingSession;
