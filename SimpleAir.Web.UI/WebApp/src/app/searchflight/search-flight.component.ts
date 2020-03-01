import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { AirportRequestDto, FlightRequestDto, Airport, Flight } from '../Model';
import { SearchFlightServiceAbstract } from './search-flight.service.abstract';

@Component({
    selector: 'app-search-flight',
    templateUrl: './search-flight.component.html'
})
export class SearchFlightComponent {
    fligths: Flight[];
    searchDeparture: FormControl = new FormControl();
    usersForm: FormGroup;
    departureValue = 0;

    destinationValue = 0;
    isLoading = false;
    model;
    postmodel: AirportRequestDto;

    airportsDestination: Airport[];
    airportsDeparture: Airport[];

    startDateValue: Date;

    constructor(private formBuilder: FormBuilder, private flightService: SearchFlightServiceAbstract) {
    }

    ngOnInit() {
        this.usersForm = this.formBuilder.group({
            'departure': [''],
            'destination': [''],
            'startDate': [''],
            'endDate': ['']
        })

        this.usersForm
            .get('departure')
            .valueChanges
            .pipe(
                debounceTime(300)
            )
            .subscribe(data => {
                this.postmodel = new AirportRequestDto();

                this.postmodel.searchKey = data;

                if (data != undefined && data != '') {
                    this.flightService.getAirPorts<Airport[]>(this.postmodel).subscribe(result => {
                        this.airportsDeparture = result;
                    }, error => console.error(error));
                }
            });

        this.usersForm
            .get('destination')
            .valueChanges
            .pipe(
                debounceTime(300)
            )
            .subscribe(data => {
                this.postmodel = new AirportRequestDto();

                this.postmodel.searchKey = data;

                if (data != undefined && data != '') {
                    this.flightService.getAirPorts<Airport[]>(this.postmodel).subscribe(result => {
                        this.airportsDestination = result;
                    }, error => console.error(error))
                }
            });
    }

    onDepartureSelected(airport: Airport) {
        this.departureValue = airport.id;
    }

    onDestinationSelected(airport: Airport) {
        this.destinationValue = airport.id;
    }

    displayDeparture(airport: Airport) {
        if (airport) {
            return airport.name + " (" + airport.code + ")";
        }
    }

    displayDestination(airport: Airport) {
        if (airport) {
            return airport.name + " (" + airport.code + ")";
        }
    }

    submit() {
        if (this.usersForm.valid) {
            let flightSearchDto = new FlightRequestDto();
            flightSearchDto.DepartureId = this.departureValue;
            flightSearchDto.DestinationId = this.destinationValue;
            flightSearchDto.StartDate = new Date(this.startDateValue);

            this.flightService.getFlights<Flight[]>(flightSearchDto).subscribe(result => {
                if (result.length > 0) {
                    this.fligths = result;
                }
            }, error => console.error(error));
        }
    }
}
