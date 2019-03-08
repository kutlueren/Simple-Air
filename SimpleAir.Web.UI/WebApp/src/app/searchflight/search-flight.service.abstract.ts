import { Observable } from 'rxjs';

export abstract class SearchFlightServiceAbstract {

  abstract getAirPorts<T>(model: any): Observable<T> ;
  abstract getFlights<T>(model: any): Observable<T> ;
}
