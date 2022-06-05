import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePlantasComponent } from './home-plantas.component';

describe('HomePlantasComponent', () => {
  let component: HomePlantasComponent;
  let fixture: ComponentFixture<HomePlantasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomePlantasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomePlantasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
