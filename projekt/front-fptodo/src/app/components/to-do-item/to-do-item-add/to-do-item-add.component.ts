import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import Board from 'src/app/models/board';
import ToDoItemDTO from 'src/app/models/todoItemDTO';
import ToDoItem from 'src/app/models/todoitem';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';

@Component({
  selector: 'app-to-do-item-add',
  templateUrl: 'to-do-item-add.component.html',
  styleUrls: ['to-do-item-add.component.css'],
})
export class ToDoItemAddComponent implements OnInit {
  editForm: FormGroup;
  public time = '';
  public date = '';

  users: User[] = [];

  boards: Board[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ToDoItemAddComponent>,
    @Inject(MAT_DIALOG_DATA) data: Board,
    private todoApiService: TodoapiService,
    private snackBar: MatSnackBar
  ) {
    const datetime = new Date();
    this.date = datetime.toLocaleDateString(); // 9/17/2016
    this.time = datetime.toLocaleTimeString(); // 11:18:48 AM
    // cut seconds
    this.time = this.time.slice(0, -3);
    this.editForm = this.formBuilder.group({
      id: [],
      name: [, Validators.required],
      isComplete: [false],
      dueDate: [datetime, Validators.required],
      dueTime: [this.time, Validators.required],
      priority: [3, Validators.required],
      boardId: [data.id, Validators.required],
      userId: [],
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
      const editedItem: ToDoItemDTO = {
        name: this.editForm.value.name,
        isComplete: this.editForm.value.isComplete,
        dueTime: new Date(this.editForm.value.dueDate.setHours(hours, minutes)),
        priority: this.editForm.value.priority,
        boardId: this.editForm.value.boardId,
        userId: this.editForm.value.userId,
      };
      this.todoApiService.createToDoItem(editedItem).subscribe(
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
