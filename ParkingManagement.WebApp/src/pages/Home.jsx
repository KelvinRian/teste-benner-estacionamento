import { useEffect, useState } from 'react';
import { get } from "../services/api";

function Home() {
    const [registros, setRegistros] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const retorno = await get();
                setRegistros(retorno);
            } catch (error) {
                console.error('Erro ao buscar dados da API:', error);
            }
        }
        fetchData();
    }, []);

    return (
        <div>
            <h>Estacionamento</h>
            {registros.map((item, index) => (
                <li key={index}>{item.summary}</li>
            ))}
        </div>
    );
}

export default Home;