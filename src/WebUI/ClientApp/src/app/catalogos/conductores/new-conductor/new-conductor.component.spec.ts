import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewConductorComponent } from './new-conductor.component';

describe('NewConductorComponent', () => {
  let component: NewConductorComponent;
  let fixture: ComponentFixture<NewConductorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewConductorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewConductorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
