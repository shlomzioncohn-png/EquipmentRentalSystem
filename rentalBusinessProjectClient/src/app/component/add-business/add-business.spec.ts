import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBusiness } from './add-business';

describe('AddBusiness', () => {
  let component: AddBusiness;
  let fixture: ComponentFixture<AddBusiness>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddBusiness],
    }).compileComponents();

    fixture = TestBed.createComponent(AddBusiness);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
