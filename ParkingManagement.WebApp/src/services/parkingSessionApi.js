const BASE_URL = 'https://localhost:44353/api/ParkingSessions';

export async function entry(body) {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });
    console.log(response);
    if (!response.ok) throw new Error('Erro ao registrar entrada.');
    if (response.status === 204) return;
    return await response.json();
};

export async function get() {
    const response = await fetch(BASE_URL);
    console.log(response);
    if (!response.ok) throw new Error('Erro ao buscar registros');
    return await response.json();
};

export async function exit(id) {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' }
    });
    console.log(response);
    if (!response.ok) throw new Error('Nenhuma sessão encontrada');
    if (response.status === 204) return;
    return await response.json();
};

export async function getByLicensePlate(LicensePlate) {
    const response = await fetch(`${BASE_URL}/${LicensePlate}`, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    });
    console.log(response);
    if (!response.ok) throw new Error('Erro ao registrar saída');
    if (response.status === 204) return;
    return await response.json();
};