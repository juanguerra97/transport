import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminVehiculosConductorComponent } from './admin-vehiculos-conductor.component';

describe('AdminVehiculosConductorComponent', () => {
  let component: AdminVehiculosConductorComponent;
  let fixture: ComponentFixture<AdminVehiculosConductorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminVehiculosConductorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminVehiculosConductorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
