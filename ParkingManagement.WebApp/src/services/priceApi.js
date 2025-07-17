const BASE_URL = 'https://localhost:44353/api/Prices';

export async function create(body) {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });
    console.log(response);
    if (!response.ok) throw new Error('Erro ao criar registro');
    if (response.status === 204) return;
    return await response.json();
};

export async function get() {
    const response = await fetch(BASE_URL);
    console.log(response);
    if (!response.ok) throw new Error('Erro ao buscar registros');
    return await response.json();
};

export async function deletePrice(id) {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE'
    });
    console.log(response);
    if (!response.ok) throw new Error('Erro ao excluir registro');
};