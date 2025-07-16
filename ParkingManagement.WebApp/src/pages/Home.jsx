import { useEffect, useState } from 'react';
import PriceRegister from '../components/PriceRegister';
import PricesTable from '../components/PricesTable';
import { get } from '../services/priceApi';

function Home() {
    const [prices, setPrices] = useState([]);
    const [error, setError] = useState('');

    async function getPrices() {
        try {
            const data = await get();
            setPrices(data);
        } catch (err) {
            setError('Erro ao carregar preços');
        }
    };

    useEffect(() => {
        getPrices();
    }, []);

    return (
        <div>
            <div>
                <PriceRegister onSuccess={getPrices} />
            </div>
            <dvi>
                <PricesTable prices={prices} onDeleteSuccess={getPrices} />
                {error && <p style={{ color: 'red' }}>{error}</p>}
            </dvi>
        </div>
    );
};

export default Home;