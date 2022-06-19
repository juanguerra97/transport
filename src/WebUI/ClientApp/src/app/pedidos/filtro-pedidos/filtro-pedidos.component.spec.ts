import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FiltroPedidosComponent } from './filtro-pedidos.component';

describe('FiltroPedidosComponent', () => {
  let component: FiltroPedidosComponent;
  let fixture: ComponentFixture<FiltroPedidosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FiltroPedidosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FiltroPedidosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
