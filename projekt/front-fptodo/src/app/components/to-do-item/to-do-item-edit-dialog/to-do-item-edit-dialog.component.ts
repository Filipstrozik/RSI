import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import Board from 'src/app/models/board';
import ToDoItemDTO from 'src/app/models/todoItemDTO';
import ToDoItem from 'src/app/models/todoitem';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-to-do-item-edit-dialog',
  templateUrl: './to-do-item-edit-dialog.component.html',
  styleUrls: ['./to-do-item-edit-dialog.component.css'],
})
export class ToDoItemEditDialogComponent implements OnInit {
  editForm: FormGroup;
  item: ToDoItem;
  public time = '';
  public date = '';

  users: User[] = [];

  boards: Board[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ToDoItemEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: ToDoItem,
    private todoApiService: TodoapiService,
    private snackBar: MatSnackBar
  ) {
    this.item = data;

    const datetime = new Date(this.item.dueTime);
    this.date = datetime.toLocaleDateString(); // 9/17/2016
    this.time = datetime.toLocaleTimeString(); // 11:18:48 AM
    // cut seconds
    this.time = this.time.slice(0, -3);
    this.editForm = this.formBuilder.group({
      id: [this.item.id],
      name: [this.item.name, Validators.required],
      isComplete: [this.item.isComplete],
      dueDate: [datetime, Validators.required],
      dueTime: [this.time, Validators.required],
      priority: [this.item.priority, Validators.required],
      boardId: [this.item.board.id, Validators.required],
      userId: [this.item.user?.id],
    });
  }

  ngOnInit() {
    this.todoApiService.getAllUsers().subscribe((users: User[]) => {
      this.users = users;
    });

    this.todoApiService.getAllBoards().subscribe((boards: Board[]) => {
      this.boards = boards;
    });
  }

  save() {
    if (this.editForm.valid) {
      const hours = this.editForm.value.dueTime.split(':')[0];
      const minutes = this.editForm.value.dueTime.split(':')[1];

      const userId = this.editForm.value.userId
        ? this.editForm.value.userId
        : -1;
      const editedItem: ToDoItemDTO = {
        id: this.editForm.value.id,
        name: this.editForm.value.name,
        isComplete: this.editForm.value.isComplete,
        dueTime: new Date(this.editForm.value.dueDate.setHours(hours, minutes)),
        priority: this.editForm.value.priority,
        boardId: this.editForm.value.boardId,
        userId: userId,
      };

      this.todoApiService.updateToDoItem(editedItem).subscribe(
        (item: ToDoItem) => {
          this.dialogRef.close(item);
        },
        (error) => {
          this.snackBar.open(error.error.errors.Priority, 'Close', {
            duration: 5000,
          });
        }
      );
    }
  }

  close() {
    this.dialogRef.close();
  }
}
