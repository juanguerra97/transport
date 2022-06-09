import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormUnidadMedidaComponent } from './form-unidad-medida.component';

describe('FormUnidadMedidaComponent', () => {
  let component: FormUnidadMedidaComponent;
  let fixture: ComponentFixture<FormUnidadMedidaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormUnidadMedidaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormUnidadMedidaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
