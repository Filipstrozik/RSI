import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToDoItemEditDialogComponent } from './to-do-item-edit-dialog.component';

describe('ToDoItemEditDialogComponent', () => {
  let component: ToDoItemEditDialogComponent;
  let fixture: ComponentFixture<ToDoItemEditDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ToDoItemEditDialogComponent]
    });
    fixture = TestBed.createComponent(ToDoItemEditDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
