import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeBodegasComponent } from './home-bodegas.component';

describe('HomeBodegasComponent', () => {
  let component: HomeBodegasComponent;
  let fixture: ComponentFixture<HomeBodegasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeBodegasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeBodegasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
