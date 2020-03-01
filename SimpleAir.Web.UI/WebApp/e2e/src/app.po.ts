import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/');
  }

  fillRequiredElements() {
    element(by.css('[formControlName="departure"]')).sendKeys('a');

    element.all(by.css('mat-option')).first().click();

    element(by.css('[formControlName="destination"]')).sendKeys('fr');

    element.all(by.css('mat-option')).first().click();

    element(by.id('startDateToggle')).element(by.css('mat-datepicker-toggle')).element(by.css('button')).click();
    browser.driver.sleep(500);
    element.all(by.className('mat-calendar-body-cell')).get(0).click();
  }
}
