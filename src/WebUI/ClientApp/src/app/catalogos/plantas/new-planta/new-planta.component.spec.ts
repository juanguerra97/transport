import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPlantaComponent } from './new-planta.component';

describe('NewPlantaComponent', () => {
  let component: NewPlantaComponent;
  let fixture: ComponentFixture<NewPlantaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewPlantaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPlantaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
