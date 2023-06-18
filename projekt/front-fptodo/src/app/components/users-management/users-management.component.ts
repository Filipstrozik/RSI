import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { AddUserDialogComponent } from './add-user-dialog/add-user-dialog.component';

@Component({
  selector: 'app-users-management',
  templateUrl: './users-management.component.html',
  styleUrls: ['./users-management.component.css'],
})
export class UsersManagementComponent {
  users: User[] = [];
  name: string = '';
  email: string = '';
  minAge: number | null = null;
  maxAge: number | null = null;

  constructor(
    private dialog: MatDialog,
    private todoApiService: TodoapiService
  ) {}

  ngOnInit(): void {
    this.updateUsers();
  }

  updateUsers() {
    this.todoApiService.getAllUsers().subscribe((users: User[]) => {
      this.users = users;
    });
  }

  onAddUser() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = null;
    this.dialog
      .open(AddUserDialogComponent, dialogConfig)
      .afterClosed()
      .subscribe((result: User) => {
        if (result) {
          this.users.push(result);
        }
      });
  }

  filterUsers() {
    this.todoApiService
      .filterUsers(this.name, this.email, this.minAge!, this.maxAge!)
      .subscribe((users: User[]) => {
        this.users = users;
      });
  }

  clearFilters(): void {
    this.name = '';
    this.email = '';
    this.minAge = null;
    this.maxAge = null;
    this.updateUsers();
  }
}
