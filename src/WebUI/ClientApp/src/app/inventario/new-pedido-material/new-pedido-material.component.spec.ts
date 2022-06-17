import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPedidoMaterialComponent } from './new-pedido-material.component';

describe('NewPedidoMaterialComponent', () => {
  let component: NewPedidoMaterialComponent;
  let fixture: ComponentFixture<NewPedidoMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewPedidoMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPedidoMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
