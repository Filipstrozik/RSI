import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserDialogComponent } from './add-user-dialog.component';

describe('AddUserDialogComponent', () => {
  let component: AddUserDialogComponent;
  let fixture: ComponentFixture<AddUserDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddUserDialogComponent]
    });
    fixture = TestBed.createComponent(AddUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
