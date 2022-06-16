import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeInventarioBodegaComponent } from './home-inventario-bodega.component';

describe('HomeInventarioBodegaComponent', () => {
  let component: HomeInventarioBodegaComponent;
  let fixture: ComponentFixture<HomeInventarioBodegaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeInventarioBodegaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeInventarioBodegaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
