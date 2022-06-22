import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeConductoresComponent } from './home-conductores.component';

describe('HomeConductoresComponent', () => {
  let component: HomeConductoresComponent;
  let fixture: ComponentFixture<HomeConductoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeConductoresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeConductoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
