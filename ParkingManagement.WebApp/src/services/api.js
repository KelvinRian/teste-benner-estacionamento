const BASE_URL = 'https://localhost:44353';

export async function get() {
  const resposta = await fetch(`${BASE_URL}/WeatherForecast`);
  if (!resposta.ok) {
    throw new Error('Erro na requisição');
  }
  const dados = await resposta.json();
  return dados;
}