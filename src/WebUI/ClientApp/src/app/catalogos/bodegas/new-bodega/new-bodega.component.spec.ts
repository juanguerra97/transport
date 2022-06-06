import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewBodegaComponent } from './new-bodega.component';

describe('NewBodegaComponent', () => {
  let component: NewBodegaComponent;
  let fixture: ComponentFixture<NewBodegaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewBodegaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewBodegaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
