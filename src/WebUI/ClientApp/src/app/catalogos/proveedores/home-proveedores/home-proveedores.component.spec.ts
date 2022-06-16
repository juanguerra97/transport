import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeProveedoresComponent } from './home-proveedores.component';

describe('HomeProveedoresComponent', () => {
  let component: HomeProveedoresComponent;
  let fixture: ComponentFixture<HomeProveedoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeProveedoresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeProveedoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
