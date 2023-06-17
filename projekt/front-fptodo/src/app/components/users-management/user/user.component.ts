import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent {
  @Input() user!: User;
  @Output() updateUser: EventEmitter<any> = new EventEmitter();

  constructor(
    private todoApiService: TodoapiService,
    private dialog: MatDialog
  ) {}

  onEditUser() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = this.user;
    this.dialog
      .open(AddUserDialogComponent, dialogConfig)
      .afterClosed()
      .subscribe((result: User) => {
        if (result) {
          this.user = result;
        }
      });
  }

  onDeleteEdit() {
    this.todoApiService.deleteUser(this.user.id!).subscribe(() => {
      this.updateUser.emit();
    });
  }
}
