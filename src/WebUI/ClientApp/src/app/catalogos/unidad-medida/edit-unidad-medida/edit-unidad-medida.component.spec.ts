import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditUnidadMedidaComponent } from './edit-unidad-medida.component';

describe('EditUnidadMedidaComponent', () => {
  let component: EditUnidadMedidaComponent;
  let fixture: ComponentFixture<EditUnidadMedidaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditUnidadMedidaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditUnidadMedidaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
