import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessDetail } from './business-detail';

describe('BusinessDetail', () => {
  let component: BusinessDetail;
  let fixture: ComponentFixture<BusinessDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusinessDetail],
    }).compileComponents();

    fixture = TestBed.createComponent(BusinessDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
