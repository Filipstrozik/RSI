import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToDoItemAddComponent } from './to-do-item-add.component';

describe('ToDoItemAddComponent', () => {
  let component: ToDoItemAddComponent;
  let fixture: ComponentFixture<ToDoItemAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ToDoItemAddComponent]
    });
    fixture = TestBed.createComponent(ToDoItemAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
