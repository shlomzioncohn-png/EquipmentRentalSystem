import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBusinesses } from './my-businesses';

describe('MyBusinesses', () => {
  let component: MyBusinesses;
  let fixture: ComponentFixture<MyBusinesses>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyBusinesses],
    }).compileComponents();

    fixture = TestBed.createComponent(MyBusinesses);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
