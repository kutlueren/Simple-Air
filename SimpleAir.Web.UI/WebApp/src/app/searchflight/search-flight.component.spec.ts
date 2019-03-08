import { async, ComponentFixture, TestBed, tick, fakeAsync, flush } from '@angular/core/testing';
import { SearchFlightComponent } from './search-flight.component';
import { Airport, Flight } from '../Model';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAutocompleteModule, MatInputModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SearchFlightServiceAbstract } from './search-flight.service.abstract';
import { Observable, Subscriber } from 'rxjs';

describe('SearchFlightComponent', () => {
  let component: SearchFlightComponent;
  let fixture: ComponentFixture<SearchFlightComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SearchFlightComponent],
      imports: [
        ReactiveFormsModule,
        MatInputModule,
        MatAutocompleteModule,
        BrowserAnimationsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        HttpClientModule,
        FormsModule,
      ],
      providers: [{
        provide: SearchFlightServiceAbstract,
        useClass: MockFlightService
      }],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchFlightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should return departure airport name accordingly', async(() => {

    let airport = new MockAirport();
    airport.name = 'Amsterdam';
    airport.code = 'SCHPL';
    let displayValue = component.displayDeparture(airport);

    //const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(displayValue).toEqual(airport.name + " (" + airport.code + ")");
  }));


  it('should return destination airport name accordingly', async(() => {

    let airport = new MockAirport();
    airport.name = 'Amsterdam';
    airport.code = 'SCHPL';
    let displayValue = component.displayDestination(airport);

    //const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(displayValue).toEqual(airport.name + " (" + airport.code + ")");
  }));


  it('should set departure value to selected value on departure selected', async(() => {

    let airport = new MockAirport();
    airport.id = 1;
    component.onDepartureSelected(airport);

    //const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(component.departureValue).toEqual(airport.id);
  }));

  it('should set destination value to selected value on departure selected', async(() => {

    let airport = new MockAirport();
    airport.id = 1;
    component.onDestinationSelected(airport);

    //const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(component.destinationValue).toEqual(airport.id);
  }));

  it('should set the selected date to startDateValue', fakeAsync(() => {

    const startDateSelector = fixture.nativeElement.querySelector('#startDateToggle');
    const toggleButton = startDateSelector.querySelector('mat-datepicker-toggle button')

    toggleButton.click()
    fixture.detectChanges();

    let cells: NodeListOf<HTMLElement> = document.querySelectorAll('.mat-calendar-body-cell') as NodeListOf<HTMLElement>;

    cells[1].click();

    fixture.detectChanges();


    flush();

    const input = startDateSelector.querySelector('input')

    expect(component.startDateValue.toLocaleDateString()).toEqual(new Date(input.value).toLocaleDateString());
  }));

  it('should set the selected date to endDateValue', fakeAsync(() => {

    const endDateSelector = fixture.nativeElement.querySelector('#endDateToggle');
    const toggleButton = endDateSelector.querySelector('mat-datepicker-toggle button')

    toggleButton.click()
    fixture.detectChanges();

    let cells: NodeListOf<HTMLElement> = document.querySelectorAll('.mat-calendar-body-cell') as NodeListOf<HTMLElement>;

    cells[5].click();

    fixture.detectChanges();

    const input = endDateSelector.querySelector('input')

    flush();

    expect(component.endDateValue.toLocaleDateString()).toEqual(new Date(input.value).toLocaleDateString());
  }));

  it('should set the selected airport id to departureValue', async(() => {
    fixture.detectChanges();
    const departureAirportSelector = fixture.nativeElement.querySelector('#departureAirport');
    const input = departureAirportSelector.querySelector('input');

    input.focus();

    input.value = 'a';
    input.dispatchEvent(new Event('input'));


    fixture.detectChanges();


    fixture.whenStable().then(() => {

      fixture.detectChanges();
      fixture.whenStable().then(() => {

        fixture.detectChanges();

        let options: NodeListOf<HTMLElement> = document.querySelectorAll('mat-option') as NodeListOf<HTMLElement>;

        options[0].click();

        fixture.detectChanges();

        let values = new Values();

        expect(component.departureValue).toEqual(values.airports[0].id);
      });

    });

  }));

  it('should set the selected airport id to destinationValue', async(() => {
    fixture.detectChanges();
    const departureAirportSelector = fixture.nativeElement.querySelector('#destinationAirport');
    const input = departureAirportSelector.querySelector('input');

    input.focus();

    input.value = 'a';
    input.dispatchEvent(new Event('input'));


    fixture.detectChanges();


    fixture.whenStable().then(() => {

      fixture.detectChanges();
      fixture.whenStable().then(() => {

        fixture.detectChanges();

        let options: NodeListOf<HTMLElement> = document.querySelectorAll('mat-option') as NodeListOf<HTMLElement>;

        options[1].click();

        fixture.detectChanges();

        let values = new Values();

        expect(component.destinationValue).toEqual(values.airports[1].id);
      });

    });

  }));


});

export class Values {
  public airports: MockAirport[] = [
    { "id": 1, "name": "test airport", "code": "tst1" },
    { "id": 2, "name": "test2 airport", "code": "tst2" }
  ];
}

export class MockFlightService extends SearchFlightServiceAbstract {

  values: Values = new Values();

  getAirPorts<T>(model: any): Observable<T> {

    let result = Observable.create((observer: Subscriber<MockAirport[]>) => {
      observer.next(this.values.airports);
      observer.complete();
    });

    return result;
  }

  getFlights<T>(model: any): Observable<T> {
    throw new Error("Method not implemented.");
  }

}

class MockAirport implements Airport {
  id: number;
  name: string;
  code: string;
}

class MockFlight implements Flight {
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
