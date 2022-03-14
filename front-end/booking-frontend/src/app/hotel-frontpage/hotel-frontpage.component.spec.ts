import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelFrontpageComponent } from './hotel-frontpage.component';

describe('HotelFrontpageComponent', () => {
  let component: HotelFrontpageComponent;
  let fixture: ComponentFixture<HotelFrontpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HotelFrontpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HotelFrontpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
