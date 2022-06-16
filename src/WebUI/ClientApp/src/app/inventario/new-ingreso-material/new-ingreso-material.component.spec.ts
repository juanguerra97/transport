import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewIngresoMaterialComponent } from './new-ingreso-material.component';

describe('NewIngresoMaterialComponent', () => {
  let component: NewIngresoMaterialComponent;
  let fixture: ComponentFixture<NewIngresoMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewIngresoMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewIngresoMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
