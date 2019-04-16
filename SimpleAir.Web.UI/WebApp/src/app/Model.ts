export class AirportRequestDto {
  searchKey: string;
}

export class FlightRequestDto {
  DepartureId: number;
  DestinationId: number;
  StartDate: Date;
}

export interface Airport {
  id: number;
  name: string;
  code: string;
}

export interface Flight {
  DepartureId: number;
  DepartureAirportName: string;
  DepartureCode: string;
  DestinationId: number;
  DestinationAirportName: string;
  DestinationCode: string;
  Date: Date;
  Fare: number;
  Currency: string;
  FlightId: number;
}
