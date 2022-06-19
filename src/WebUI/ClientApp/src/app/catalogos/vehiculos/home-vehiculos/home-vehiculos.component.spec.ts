import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeVehiculosComponent } from './home-vehiculos.component';

describe('HomeVehiculosComponent', () => {
  let component: HomeVehiculosComponent;
  let fixture: ComponentFixture<HomeVehiculosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeVehiculosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeVehiculosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
