const BASE_URL = 'https://localhost:44353/api/Prices';

export async function create(body) {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });
    console.log('requisitado');
    console.log(response);
    if (!response.ok) throw new Error('Erro ao criar registro');
    if (response.status === 204) return;
    return await response.json();
};