import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidosBodegaComponent } from './pedidos-bodega.component';

describe('PedidosBodegaComponent', () => {
  let component: PedidosBodegaComponent;
  let fixture: ComponentFixture<PedidosBodegaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedidosBodegaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PedidosBodegaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
