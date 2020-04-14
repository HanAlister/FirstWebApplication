import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: string[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<string[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  dt_txt : string
  temp: DecimalPipe
  pressure: DecimalPipe
  humidity: DecimalPipe
  clouds: DecimalPipe
  rain: DecimalPipe
  snow: DecimalPipe
  description: string
}
