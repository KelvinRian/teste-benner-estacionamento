import { useEffect, useState } from 'react';
import PriceRegister from '../components/PriceRegister';
import PricesTable from '../components/PricesTable';
import { get } from '../services/priceApi';
import EntryForm from '../components/EntryForm';
import ParkingSessionsTable from '../components/ParkingSessionsTable';
import SearchParkingSession from '../components/SearchParkingSession';
import { get as getParkingSessionsApi } from '../services/parkingSessionApi';

function Home() {
    const [prices, setPrices] = useState([]);
    const [parkingSessions, setParkingSessions] = useState([]);
    const [error, setError] = useState('');

    async function getPrices() {
        try {
            const data = await get();
            setPrices(data);
        } catch (err) {
            setError('Erro ao carregar preços');
        }
    };
    
    async function getParkingSessions() {
        try {
            const data = await getParkingSessionsApi();
            setParkingSessions(data);
        } catch (err) {
            setError('Erro ao carregar estacionamento');
        }
    };

    useEffect(() => {
        getPrices();
        getParkingSessions();
    }, []);

    return (
        <div>
            <div>
                <EntryForm onSuccess={getParkingSessions}/>
            </div>
            <div>
                <SearchParkingSession setParkingSessions={setParkingSessions}/>
            </div>
            <div>
                <ParkingSessionsTable parkingSessions={parkingSessions} onExitSuccess={getParkingSessions} />
            </div>
            <div>
                <PriceRegister onSuccess={getPrices} />
            </div>
            <div>
                <PricesTable prices={prices} onDeleteSuccess={getPrices} />
                {error && <p style={{ color: 'red' }}>{error}</p>}
            </div>
        </div>
    );
};

export default Home;