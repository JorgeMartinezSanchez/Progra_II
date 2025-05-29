import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SummitComponent } from './summit.component';

describe('SummitComponent', () => {
  let component: SummitComponent;
  let fixture: ComponentFixture<SummitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SummitComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SummitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
