import { useState  } from "react";
import { create } from "../services/priceApi";

function PriceRegister({ onSuccess }) {
    const [baseValue, setBaseValue] = useState('');
    const [extraTimeValue, setExtraTimeValue] = useState('');
    const [effectivePeriodStart, setEffectivePeriodStart] = useState('');
    const [effectivePeriodEnd, setEffectivePeriodEnd] = useState('');
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');

    async function handlerCreate(e) {
        e.preventDefault();
        setMessage('');
        setError('');

        try {
            const body = {
                baseValue: parseFloat(baseValue.replace(',', '.')),
                extraTimeValue: parseFloat(extraTimeValue.replace(',', '.')),
                effectivePeriodStart: new Date(effectivePeriodStart).toISOString(),
                effectivePeriodEnd: new Date(effectivePeriodEnd).toISOString(),
            };

            await create(body);
            setError('');
            setMessage('Registro criado com sucesso!');
            setBaseValue('');
            setExtraTimeValue('');
            setEffectivePeriodStart('');
            setEffectivePeriodEnd('');
            if (onSuccess) onSuccess();
        } catch (error) {
            setMessage('');
            setError('Erro ao salvar');
        };
    }

    return (
        <div style={{ padding: '20px' }}>
            <form onSubmit={handlerCreate}>
                <div style={{ marginTop: '10px' }}>
                <label>Valor base (R$):</label><br />
                <input
                    type="text"
                    value={baseValue}
                    onChange={(e) => setBaseValue(e.target.value)}
                    placeholder="00,00"
                    required
                />
                </div>

                <div style={{ marginTop: '10px' }}>
                <label>Valor horas adicionais (R$):</label><br />
                <input
                    type="text"
                    value={extraTimeValue}
                    onChange={(e) => setExtraTimeValue(e.target.value)}
                    placeholder="00,00"
                    required
                />
                </div>

                <div style={{ marginTop: '10px' }}>
                <label>Ínicio vigência:</label><br />
                <input
                    type="datetime-local"
                    value={effectivePeriodStart}
                    onChange={(e) => setEffectivePeriodStart(e.target.value)}
                    required
                />
                </div>

                <div style={{ marginTop: '10px' }}>
                    <label>Fim vigência:</label><br />
                    <input
                        type="datetime-local"
                        value={effectivePeriodEnd}
                        onChange={(e) => setEffectivePeriodEnd(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" style={{ marginTop: '15px' }}>
                    Salvar
                </button>
            </form>

            
            {message && <p style={{ color: 'green' }}>{message}</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
    );
}

export default PriceRegister;