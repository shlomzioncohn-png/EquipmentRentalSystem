import { ComponentFixture, TestBed } from '@angular/core/testing';
import { mapBusiness } from './mapBusiness';


describe('MapBusiness', () => {
  let component: mapBusiness;
  let fixture: ComponentFixture<mapBusiness>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [mapBusiness],
    }).compileComponents();

    fixture = TestBed.createComponent(mapBusiness);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
