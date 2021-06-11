import { Component } from '@angular/core';
import { WeatherForecast, WeatherForecastClient } from '../apis/api';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(
    weatherClient: WeatherForecastClient
  ) {
    weatherClient.get().subscribe(forecasts => {
      this.forecasts = forecasts;
    });
  }
}
