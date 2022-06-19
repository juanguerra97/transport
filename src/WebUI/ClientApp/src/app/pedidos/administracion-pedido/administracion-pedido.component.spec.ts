import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministracionPedidoComponent } from './administracion-pedido.component';

describe('AdministracionPedidoComponent', () => {
  let component: AdministracionPedidoComponent;
  let fixture: ComponentFixture<AdministracionPedidoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministracionPedidoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministracionPedidoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
