import { AppPage } from './app.po';
import { browser, by, element } from 'protractor';

describe('App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should search flight and find some', () => {
    page.navigateTo();
    page.fillRequiredElements();

    element(by.css('.btn-primary')).click();
    browser.driver.sleep(200);

    var tabledata = element(by.css(".table-striped")).element(by.tagName("tbody"));

    var rows = tabledata.all(by.tagName("tr"));

    expect(rows.count()).toBeGreaterThan(0);
  });
});
