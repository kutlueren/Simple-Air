import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Http, RequestOptions, Headers } from '@angular/http';
import { SearchFlightServiceAbstract } from './search-flight.service.abstract';


@Injectable()
export class SearchFlightService extends SearchFlightServiceAbstract{

  apiUrl ="http://localhost:61222/api"

  constructor(private http: HttpClient) {
      super();
  }

  getAirPorts<T>(model: any): Observable<T> {

    let head = new HttpHeaders({ 'Content-Type': 'application/json' });
    //let option = new RequestOptions({ headers: head });

    return this.http.post<T>(this.apiUrl + '/GetAirports', JSON.stringify(model), { headers: head });
  }

  getFlights<T>(model: any): Observable<T> {

    let head = new HttpHeaders({ 'Content-Type': 'application/json' });
    //let option = new RequestOptions({ headers: head });

    return this.http.post<T>(this.apiUrl + '/GetFlights', JSON.stringify(model), { headers: head });
  }
}
