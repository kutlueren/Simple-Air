import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SearchFlightComponent } from './searchflight/search-flight.component';
import { SearchFlightService } from './searchflight/search-flight.service';
import { SearchFlightServiceAbstract } from './searchflight/search-flight.service.abstract';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAutocompleteModule, MatInputModule, MatDatepickerModule,  MatNativeDateModule  } from '@angular/material';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchFlightComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatAutocompleteModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    RouterModule.forRoot([
      { path: '', component: SearchFlightComponent }
    ])
  ],
  providers: [{
    provide: SearchFlightServiceAbstract,
    useClass: SearchFlightService 
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
