async function getData() {
    const apiUrl = 'https://localhost:7008/WeatherForecast/medicao-tempo';

    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error('Erro: ' + response.statusText);
        }
        const data = await response.json();

        const results = data[0].results;
        console.log(results);
        document.getElementById('dia').textContent = 'Data: ' + (results.date || 'N/A');
        document.getElementById('umidade').textContent = 'Umidade: ' + (results.humidity || 'N/A');
        document.getElementById('cidade').textContent = 'Cidade: ' + (results.city_name || 'N/A');
        document.getElementById('descricao').textContent = 'Descrição: ' + (results.description || 'N/A');
        document.getElementById('temperatura').textContent = 'Temperatura: ' + (results.temp || 'N/A');

        getChart(results.forecast); // Passa os dados de forecast diretamente para getChart
    } catch (error) {
        console.error('Erro:', error);
    }
}

function getChart(forecast) {
    const labels = forecast.map(item => item.date);
    const temperatures = forecast.map(item => item.max);

    const chartData = {
        labels: labels,
        datasets: [{
            label: 'Temperatura',
            data: temperatures,
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }]
    };

    const config = {
        type: 'bar',
        data: chartData,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    };

    const ctx = document.getElementById('chart').getContext('2d');
    new Chart(ctx, config);
}
