<template>
  <div class="weather-component">
    <h1>Weather forecast</h1>
    <p>This component demonstrates fetching data from the server.</p>

    <div v-if="post" class="content">
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="forecast in post" :key="forecast.date">
            <td>{{ forecast.date }}</td>
            <td>{{ forecast.temperatureC }}</td>
            <td>{{ forecast.temperatureF }}</td>
            <td>{{ forecast.summary }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

type Forecasts = {
  date: string,
  temperatureC: string,
  temperatureF: string,
  summary: string
}[];

const post = ref<Forecasts | null>(null);

await fetchData();

async function fetchData(): Promise<void> {
  post.value = null;
  const response = await fetch('weatherforecast')
  await new Promise(resolve => setTimeout(resolve, 3000));
  const json = await response.json();
  post.value = json as Forecasts;
}

</script>

<style scoped>
th {
  font-weight: bold;
}

th,
td {
  padding-left: .5rem;
  padding-right: .5rem;
}

.weather-component {
  text-align: center;
}

table {
  margin-left: auto;
  margin-right: auto;
}
</style>