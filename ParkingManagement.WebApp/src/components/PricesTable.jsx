import { useState } from "react";
import { deletePrice } from "../services/priceApi";

function PricesTable({ prices, onDeleteSuccess }) {
    const [error, setError] = useState('');


    async function handleDelete(id) {
        try {
            await deletePrice(id);
            if (onDeleteSuccess) onDeleteSuccess();
        } catch (error) {
            setError('Erro ao excluir.');
        }
    }

    return (
        <div div style={{ padding: '20px' }}>
            <h2>Tabela de Preços</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}

            <table border="1" cellPadding="8">
                <thead>
                    <tr>
                        <th>Valor Base (R$)</th>
                        <th>Valor horas adicionais (R$)</th>
                        <th>Início vigência</th>
                        <th>Fim vigência</th>
                        <th>Exluir</th>
                    </tr>
                </thead>
                <tbody>
                    {prices.map(price => (
                        <tr key={price.id}>
                            <td>{price.baseValue.toFixed(2)}</td>
                            <td>{price.extraTimeValue.toFixed(2)}</td>
                            <td>{new Date(price.effectivePeriodStart).toLocaleString()}</td>
                            <td>{new Date(price.effectivePeriodEnd).toLocaleString()}</td>
                            <td>
                                <button onClick={() => handleDelete(price.id)}>
                                    Excluir
                                </button>
                            </td>
                        </tr>
                    ))}
                    {prices.length === 0 && (
                        <tr>
                            <td colSpan="5">Nenhum registro encontrado.</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default PricesTable;