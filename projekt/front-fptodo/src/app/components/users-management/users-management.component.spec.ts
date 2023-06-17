import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersManagementComponent } from './users-management.component';

describe('UsersManagementComponent', () => {
  let component: UsersManagementComponent;
  let fixture: ComponentFixture<UsersManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UsersManagementComponent]
    });
    fixture = TestBed.createComponent(UsersManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
