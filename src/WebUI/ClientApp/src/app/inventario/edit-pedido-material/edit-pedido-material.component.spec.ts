import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPedidoMaterialComponent } from './edit-pedido-material.component';

describe('EditPedidoMaterialComponent', () => {
  let component: EditPedidoMaterialComponent;
  let fixture: ComponentFixture<EditPedidoMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPedidoMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPedidoMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
