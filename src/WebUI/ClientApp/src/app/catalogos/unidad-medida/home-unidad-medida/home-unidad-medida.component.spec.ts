import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeUnidadMedidaComponent } from './home-unidad-medida.component';

describe('HomeUnidadMedidaComponent', () => {
  let component: HomeUnidadMedidaComponent;
  let fixture: ComponentFixture<HomeUnidadMedidaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeUnidadMedidaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeUnidadMedidaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
