import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeMaterialesComponent } from './home-materiales.component';

describe('HomeMaterialesComponent', () => {
  let component: HomeMaterialesComponent;
  let fixture: ComponentFixture<HomeMaterialesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeMaterialesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeMaterialesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
